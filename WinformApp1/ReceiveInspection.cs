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

namespace WinformApp1
{
    public partial class ReceiveInspection : Form
    {
        public ReceiveInspection()
        {
            InitializeComponent();
        }

        private void ReceiveInspection_Load(object sender, EventArgs e)
        {
            btnInit_Click(this, null);

            var t1 = new Thread(() => {
                while(true)
                {
                    if (!serialPort1.IsOpen)
                    {
                        try
                        {
                            serialPort1.Open();
                            if(serialPort1.IsOpen)
                                StatusMessageShow("바코드스캐너 연결OK");
                        }
                        catch (IOException ex)
                        {
                            StatusMessageShow("바코드스캐너 연결에 문제가 있습니다. 케이블 연결 여부 혹은 스캐너 상태를 확인하세요.");
                        }
                        catch (Exception ex)
                        {
                            StatusMessageShow(ex.Message);
                        }
                    }
                    Thread.Sleep(4500);
                }
            });

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

            t1.IsBackground = t2.IsBackground = true;
            t1.Start();
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
                    new KeyValuePair<string, string>("ok_ng", rhlResult.Result == ResultType.OK ? "OK" : "NG")
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
            lblStandWeight.Text = "0.0 g";
            lblMeanWeight.Text = "0.0 g";
            rhlResult.Result = ResultType.None;
        }

        // 바코드스캐너 수신 이벤트
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                var barcode = serialPort1.ReadExisting();

                if (txtBarcodeData.InvokeRequired)
                {
                    txtBarcodeData.Invoke((MethodInvoker)(() =>
                    {
                        txtBarcodeData.Text = barcode;
                    }));
                }
                else
                {
                    txtBarcodeData.Text = barcode;
                }

                var data = barcode.Split('#');
                if (data.Length == 7)
                {
                    var partNo = data[4];
                    if (txtPart.InvokeRequired)
                    {
                        txtPart.Invoke((MethodInvoker)(() =>
                        {
                            txtPart.Text = partNo;
                        }));
                    }
                    else
                    {
                        txtPart.Text = partNo;
                    }
                    var weight = data[5];
                    if (lblStandWeight.InvokeRequired)
                    {
                        lblStandWeight.Invoke((MethodInvoker)(() =>
                        {
                            lblStandWeight.Text = $"{weight} g";
                        }));
                    }
                    else
                    {
                        lblStandWeight.Text = $"{weight} g";
                    }
                }
                else
                {
                    StatusMessageShow("형식에 맞지 않은 바코드입니다");
                }
            }
            catch (IOException ex)
            {
                StatusMessageShow(ex.Message);
            }
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

                if (lblMeanWeight.InvokeRequired)
                {
                    lblMeanWeight.Invoke((MethodInvoker)(() =>
                    {
                        lblMeanWeight.Text = $"{weight} g";
                    }));
                }
                else
                {
                    lblMeanWeight.Text = $"{weight} g";
                }

                var sStandard = lblStandWeight.Text.Substring(0, lblStandWeight.Text.Length - 3);
                var standard = 0.0f;
                if (float.TryParse(sStandard, out standard))
                {
                    rhlResult.Result = standard + 0.5f > weight && standard - 0.5f < weight ? ResultType.OK : ResultType.NG;
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

        // 하단 상태 정보 메세지
        private void StatusMessageShow(string message)
        {
            this.BeginInvoke((Action)(() => {
                tslStatus.Text = DateTime.Now.ToString("HH:mm:ss") + ":" + message;
            }));
        }
    }
}
