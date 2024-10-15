using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformApp1.UserControls;
using WinformApp1.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace WinformApp1
{
    public partial class ReceiveInspection : Form
    {
        BarcodeScanner _scanner;
        Action<byte[]> barcodeReadCallback;
        WeightScaler _weighter;
        Action<byte[]> scaleReadCallback;
        Action<Control, string> TextSetThreadSafe;
        const string ZERO_FLOAT_VALUE = "0.0";
        const string SCANNER_PORT_NAME = "COM3";
        const string SCALE_PORT_NAME = "COM4";
        public ReceiveInspection()
        {
            InitializeComponent();
        }

        private void ReceiveInspection_Load(object sender, EventArgs e)
        {
            btnInit_Click(this, null);

            TextSetThreadSafe = (control, text) =>
            {
                if (control.InvokeRequired)
                {
                    control.Invoke(new Action(() => control.Text = text));
                }
                else
                {
                    control.Text = text;
                }
            };

            barcodeReadCallback = (buffer) => {
                var barcode = new BarcodeInfo(buffer);
                TextSetThreadSafe(txtBarcodeData, barcode.DataText);

                if (barcode.ValidBarcode)
                {
                    TextSetThreadSafe(txtPart, barcode.PartNo);
                    TextSetThreadSafe(lblStandWeight, $"{barcode.StandardWeight}");
                }
                else
                {
                    StatusMessageShow("형식에 맞지 않은 바코드입니다");
                }
            };

            var scannerPort = new SerialPort()
            {
                PortName = SCANNER_PORT_NAME,
                BaudRate = 9600,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One
            };
            var port = new XerialPort(scannerPort, barcodeReadCallback);
            _scanner = new BarcodeScanner(port);
            _scanner.ConnectStart();

            scaleReadCallback += (buffer) =>
            {
                var weight = BitConverter.ToSingle(buffer, 0);
                TextSetThreadSafe(lblMeanWeight, $"{weight}");
                var judgment = new Judgmenter(JudgmentType.MarginOfError);
                var standard = 0.0f;
                if (float.TryParse(lblStandWeight.Text, out standard))
                {
                    rhlResult.Result = judgment.Condition(standard, weight) ? ResultType.OK : ResultType.NG;
                }
                else
                {
                    rhlResult.Result = ResultType.None;
                    StatusMessageShow("표준중량 값에 이상이 있습니다");
                }
            };
            var scalePort = new SerialPort()
            {
                PortName = SCALE_PORT_NAME,
                BaudRate = 9600,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One
            };
            var port2 = new XerialPort(scalePort, scaleReadCallback);
            _weighter = new WeightScaler(port2);
            _weighter.ConnectStart();


        }

        // 입고검사 완료
        private void btnInspect_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://69af0b64-3377-43c3-a562-60729cebad2a.mock.pstmn.io");
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("barcode", txtBarcodeData.Text),
                    new KeyValuePair<string, string>("model", txtPart.Text),
                    new KeyValuePair<string, string>("weight", lblMeanWeight.Text),
                    new KeyValuePair<string, string>("ok_ng", rhlResult.Result == ResultType.OK ? ResultTypeText.OK : ResultTypeText.NG)
                });
                var result = client.PostAsync("/api/receive/inspect", content);
                StatusMessageShow("결과가 전송되었습니다");
                btnInit_Click(this, null);
            }
            catch (Exception ex)
            {
                //통신 실패시 처리로직
                StatusMessageShow(ex.Message);
            }
        }

        // 초기화
        private void btnInit_Click(object sender, EventArgs e)
        {
            txtBarcodeData.Text = txtPart.Text = string.Empty;
            lblStandWeight.Text = lblMeanWeight.Text = ZERO_FLOAT_VALUE;
            rhlResult.Result = ResultType.None;
        }

        // 하단 상태 정보 메세지
        public void StatusMessageShow(string message)
        {
            toolStrip1.BeginInvoke(new Action(async () =>
            {
                var msg = $"{DateTime.Now.ToString("HH:mm:ss")}:{message}";
                tslStatus.Text = msg;
                await Task.Delay(5000);
                if(tslStatus.Text == msg)
                    tslStatus.Text = string.Empty;
            }));
        }
    }
}
