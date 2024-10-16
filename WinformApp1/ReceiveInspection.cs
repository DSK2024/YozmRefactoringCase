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

namespace WinformApp1
{
    public partial class ReceiveInspection : Form
    {
        /// <summary>
        /// 컨트롤의 Text 스레드 안전한 세팅 대리자
        /// </summary>
        Action<Control, string> TextSetThreadSafe;
        /// <summary>
        /// 바코드스캐너 객체 멤버
        /// </summary>
        BarcodeScanner _scanner;
        /// <summary>
        /// 중량계 객체 멤버
        /// </summary>
        WeightScaler _weighter;
        /// <summary>
        /// 바코드 수신 콜백
        /// </summary>
        Action<byte[]> barcodeReadCallback;
        /// <summary>
        /// 중량계 수신 콜백
        /// </summary>
        Action<byte[]> scaleReadCallback;
        const string ZERO_FLOAT_VALUE = "0.0";
        const float ALLOW_ERROR_WEIGHT_ADD = 0.5f;
        public ReceiveInspection()
        {
            InitializeComponent();

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

            scaleReadCallback += (buffer) =>
            {
                var weight = BitConverter.ToSingle(buffer, 0);
                TextSetThreadSafe(lblMeanWeight, $"{weight}");

                var standard = 0.0f;
                if (float.TryParse(lblStandWeight.Text, out standard))
                {
                    var condition = new ConditionMarginError(standard, ALLOW_ERROR_WEIGHT_ADD, weight);
                    var tester = new Judge(condition);
                    rhlResult.Result = tester.Judgment() ? ResultType.OK : ResultType.NG;
                }
                else
                {
                    rhlResult.Result = ResultType.None;
                    StatusMessageShow("표준중량 값에 이상이 있습니다");
                }
            };
        }

        private void ReceiveInspection_Load(object sender, EventArgs e)
        {
            btnInit_Click(this, null);

            _scanner = new BarcodeScanner(
                Global.GetScannerXerial(barcodeReadCallback)
            );
            _scanner.ConnectStart();

            _weighter = new WeightScaler(
                Global.GetScaleXerial(scaleReadCallback)
            );
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

        /// <summary>
        /// 하단 상태 정보 메세지 출력. 5초 후 메세지는 초기화 된다.
        /// </summary>
        /// <param name="message">메세지</param>
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
