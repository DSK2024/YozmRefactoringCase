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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace WinformApp1
{
    public partial class ReceiveInspection : Form
    {
        BarcodeScanner _scanner;
        Action<byte[]> barcodeReadCallback;
        public ReceiveInspection()
        {
            InitializeComponent();
        }

        private void ReceiveInspection_Load(object sender, EventArgs e)
        {
            btnInit_Click(this, null);

            barcodeReadCallback = (buffer) => {
                var barcode = new BarcodeInfo(buffer);
                if (txtBarcodeData.InvokeRequired)
                {
                    txtBarcodeData.Invoke((MethodInvoker)(() => {
                        txtBarcodeData.Text = barcode.DataText;
                    }));
                }
                else
                {
                    txtBarcodeData.Text = barcode.DataText;
                }

                if (barcode.ValidBarcode)
                {
                    if (txtPart.InvokeRequired)
                    {
                        txtPart.Invoke((MethodInvoker)(() => {
                            txtPart.Text = barcode.PartNo;
                        }));
                    }
                    else
                    {
                        txtPart.Text = barcode.PartNo;
                    }

                    if (lblStandWeight.InvokeRequired)
                    {
                        lblStandWeight.Invoke((MethodInvoker)(() => {
                            lblStandWeight.Text = $"{barcode.StandardWeight} g";
                        }));
                    }
                    else
                    {
                        lblStandWeight.Text = $"{barcode.StandardWeight} g";
                    }
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
                    new KeyValuePair<string, string>("ok_ng", lblOk.BackColor == Color.Blue ? "OK" : "NG")
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
            lblOk.BackColor = Color.Black;
            lblNG.BackColor = Color.Gray;
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
                    if (standard + 0.5f > weight && standard - 0.5f < weight)
                    {
                        lblOk.BackColor = System.Drawing.Color.Blue;
                        lblNG.BackColor = System.Drawing.Color.Gray;
                        return;
                    }
                }
                lblOk.BackColor = System.Drawing.Color.Gray;
                lblNG.BackColor = System.Drawing.Color.Red;
            }
            catch (IOException ex)
            {
                StatusMessageShow(ex.Message);
            }
        }

        // 하단 상태 정보 메세지
        public void StatusMessageShow(string message)
        {
            this.BeginInvoke((Action)(() => {
                tslStatus.Text = DateTime.Now.ToString("HH:mm:ss") + ":" + message;
            }));
        }
    }
}
