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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

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
            
            try
            {
                serialPort1.Open();
            }
            catch(IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                serialPort2.Open();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 입고검사 완료
        private void btnInspect_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8719");
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("barcode", txtBarcodeData.Text),
                    new KeyValuePair<string, string>("model", txtPart.Text),
                    new KeyValuePair<string, string>("weight", lblMeanWeight.Text),
                    new KeyValuePair<string, string>("ok_ng", lblOk.BackColor == Color.Blue ? "OK" : "NG")
                });
                var result = client.PostAsync("/api/Membership/exists", content);
            }
            catch (Exception ex)
            {
                //통신 실패시 처리로직
                Console.WriteLine(ex.ToString());
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

                if(barcode.Length > 10)
                {
                    var data = barcode.Split('#');
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
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            try
            {
                var sWeight = serialPort2.ReadExisting();

                if (lblMeanWeight.InvokeRequired)
                {
                    lblMeanWeight.Invoke((MethodInvoker)(() =>
                    {
                        lblMeanWeight.Text = $"{sWeight} g";
                    }));
                }
                else
                {
                    lblMeanWeight.Text = $"{sWeight} g";
                }

                var weight = 0.0f;
                var standard = 0.0f;
                if (float.TryParse(sWeight, out weight))
                {
                    var sStandard = lblStandWeight.Text.Substring(0, lblStandWeight.Text.Length - 3);
                    if(float.TryParse(sStandard, out standard))
                    {
                        if(standard + 0.5f > weight && standard - 0.5f < weight)
                        {
                            lblOk.BackColor = System.Drawing.Color.Blue;
                            lblNG.BackColor = System.Drawing.Color.Gray;
                            return;
                        }
                    }
                }
                lblOk.BackColor = System.Drawing.Color.Gray;
                lblNG.BackColor = System.Drawing.Color.Red;
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
