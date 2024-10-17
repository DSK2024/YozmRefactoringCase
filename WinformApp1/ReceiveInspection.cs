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
using WinformApp1.Models;
using WinformApp1.Services;
using WinformApp1.UserControls;

namespace WinformApp1
{
    public partial class ReceiveInspection : Form
    {
        /// <summary>
        /// 바코드스캐너 객체 멤버
        /// </summary>
        Devices.BarcodeScanner _scanner;
        /// <summary>
        /// 중량계 객체 멤버
        /// </summary>
        Devices.WeightScaler _weighter;
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

            barcodeReadCallback = (buffer) => {
                var barcode = new Models.BarcodeInfo(buffer);
                Helper.ThreadSafety.TextSet(txtBarcodeData, barcode.DataText);

                if (barcode.ValidBarcode)
                {
                    Helper.ThreadSafety.TextSet(txtPart, barcode.PartNo);
                    Helper.ThreadSafety.TextSet(lblStandWeight, $"{barcode.StandardWeight}");
                }
                else
                {
                    StatusMessageShow("형식에 맞지 않은 바코드입니다");
                }
            };

            scaleReadCallback += (buffer) =>
            {
                var weight = BitConverter.ToSingle(buffer, 0);
                Helper.ThreadSafety.TextSet(lblMeanWeight, $"{weight}");

                var standard = 0.0f;
                if (float.TryParse(lblStandWeight.Text, out standard))
                {
                    var condition = new Models.ConditionMarginError(standard, ALLOW_ERROR_WEIGHT_ADD, weight);
                    var tester = new Models.Judge(condition);
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

            _scanner = new Devices.BarcodeScanner(
                ProgramGlobal.GetScannerXerial(barcodeReadCallback)
            );
            _scanner.StartRun();

            _weighter = new Devices.WeightScaler(
                ProgramGlobal.GetScaleXerial(scaleReadCallback)
            );
            _weighter.StartRun();
        }

        // 입고검사 완료
        private void btnInspect_Click(object sender, EventArgs e)
        {
            var check = rhlResult.Result == ResultType.OK ? ResultTypeText.OK : ResultTypeText.NG;
            var result = new InspectResult(txtPart.Text, lblMeanWeight.Text, check, txtBarcodeData.Text);
            var service = ProgramGlobal.GetHttpSender;
            if(service.Send("/api/receive/inspect", result))
                StatusMessageShow("전송이 완료되었습니다");
            else
                StatusMessageShow("전송되지 않았습니다. 연결을 확인하세요");
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
