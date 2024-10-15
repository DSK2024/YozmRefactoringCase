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
        Action<Control, string> TextSetThreadSafe;
        const string ZERO_FLOAT_VALUE = "0.0";
        const float ALLOW_ERROR_WEIGHT_ADD = 0.5f;
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
                PortName = "COM3",
                BaudRate = 9600,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One
            };
            var port = new XerialPort(scannerPort, barcodeReadCallback);
            _scanner = new BarcodeScanner(port);
            _scanner.ConnectStart();

            var t2 = new Thread(() =>
            {
                while(true)
                {
                    if (!serialPort2.IsOpen)
                    {
                        try
                        {
                            serialPort2.Open();
                            if (serialPort2.IsOpen)
                                StatusMessageShow("중량계 연결OK");
                        }
                        catch (IOException ex)
                        {
                            StatusMessageShow("중량계 연결에 문제가 있습니다. 케이블 연결 여부 혹은 중량계 전원을 확인하세요.");
                        }
                        catch (Exception ex)
                        {
                            StatusMessageShow(ex.Message);
                        }
                    }
                    Thread.Sleep(5500);
                }
            });

            t2.IsBackground = true;
            t2.Start();
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

        // 중량계 수신 이벤트
        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            try
            {
                var bytes = serialPort2.BytesToRead;
                var buffer = new byte[bytes];
                serialPort2.Read(buffer, 0, bytes);
                var weight = BitConverter.ToSingle(buffer, 0);

                TextSetThreadSafe(lblMeanWeight, $"{weight}");

                var standard = 0.0f;
                if (float.TryParse(lblStandWeight.Text, out standard))
                {
                    rhlResult.Result = MarginOfErrorTest(standard, ALLOW_ERROR_WEIGHT_ADD, weight) ? ResultType.OK : ResultType.NG;
                }
                else
                {
                    rhlResult.Result = ResultType.None;
                    StatusMessageShow("표준중량 값에 이상이 있습니다");
                }
            }
            catch (IOException ex)
            {
                StatusMessageShow(ex.Message);
            }
        }

        /// <summary>
        /// 허용오차 범위인지 판별하여 bool을 반환한다.
        /// </summary>
        /// <param name="standardWeight">표준중량</param>
        /// <param name="allowError">허용오차</param>
        /// <param name="weight">검증할 중량값</param>
        /// <returns>허용오차 범위 안의 중량이면 true 아니면 false 반환한다</returns>
        private bool MarginOfErrorTest(float standardWeight, float allowError, float weight) => 
            standardWeight + allowError > weight && standardWeight - allowError < weight;

        // 하단 상태 정보 메세지
        public void StatusMessageShow(string message)
        {
            this.BeginInvoke((Action)(() => {
                tslStatus.Text = DateTime.Now.ToString("HH:mm:ss") + ":" + message;
            }));
        }
    }
}
