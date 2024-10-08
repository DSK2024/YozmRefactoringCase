using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        }

        // 입고검사 완료
        private void btnInspect_Click(object sender, EventArgs e)
        {

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
                    var partNo = barcode.Split('#')[4];
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
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
