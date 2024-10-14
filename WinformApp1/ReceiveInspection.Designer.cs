namespace WinformApp1
{
    partial class ReceiveInspection
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceiveInspection));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtPart = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblMeanWeight = new System.Windows.Forms.Label();
            this.lblStandWeight = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBarcodeData = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnInspect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnInit = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tslStatus = new System.Windows.Forms.ToolStripLabel();
            this.rhlResult = new WinformApp1.UserControls.ResultHighlighter();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.txtPart, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblMeanWeight, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblStandWeight, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtBarcodeData, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnInspect, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnInit, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.rhlResult, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(651, 245);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtPart
            // 
            this.txtPart.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.txtPart, 2);
            this.txtPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPart.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtPart.Location = new System.Drawing.Point(193, 41);
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(455, 41);
            this.txtPart.TabIndex = 10;
            this.txtPart.Text = "AB12";
            this.txtPart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.Location = new System.Drawing.Point(3, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(184, 41);
            this.label8.TabIndex = 9;
            this.label8.Text = "품목";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMeanWeight
            // 
            this.lblMeanWeight.AutoSize = true;
            this.lblMeanWeight.BackColor = System.Drawing.SystemColors.Control;
            this.lblMeanWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMeanWeight.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMeanWeight.ForeColor = System.Drawing.Color.Blue;
            this.lblMeanWeight.Location = new System.Drawing.Point(423, 82);
            this.lblMeanWeight.Name = "lblMeanWeight";
            this.lblMeanWeight.Size = new System.Drawing.Size(225, 41);
            this.lblMeanWeight.TabIndex = 8;
            this.lblMeanWeight.Text = "0.0 g";
            this.lblMeanWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStandWeight
            // 
            this.lblStandWeight.AutoSize = true;
            this.lblStandWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStandWeight.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblStandWeight.Location = new System.Drawing.Point(193, 82);
            this.lblStandWeight.Name = "lblStandWeight";
            this.lblStandWeight.Size = new System.Drawing.Size(224, 41);
            this.lblStandWeight.TabIndex = 7;
            this.lblStandWeight.Text = "7.5 g";
            this.lblStandWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 41);
            this.label1.TabIndex = 1;
            this.label1.Text = "바코드 데이터";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBarcodeData
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtBarcodeData, 2);
            this.txtBarcodeData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBarcodeData.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtBarcodeData.Location = new System.Drawing.Point(193, 3);
            this.txtBarcodeData.Multiline = true;
            this.txtBarcodeData.Name = "txtBarcodeData";
            this.txtBarcodeData.ReadOnly = true;
            this.txtBarcodeData.Size = new System.Drawing.Size(455, 35);
            this.txtBarcodeData.TabIndex = 3;
            this.txtBarcodeData.Text = "##HY#K3A08#AB12#7.5#0001";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(3, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(184, 41);
            this.label5.TabIndex = 6;
            this.label5.Text = "표준|측정";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnInspect
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.btnInspect, 2);
            this.btnInspect.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnInspect.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnInspect.Location = new System.Drawing.Point(3, 167);
            this.btnInspect.Name = "btnInspect";
            this.btnInspect.Size = new System.Drawing.Size(414, 54);
            this.btnInspect.TabIndex = 0;
            this.btnInspect.Text = "검사완료";
            this.btnInspect.UseVisualStyleBackColor = true;
            this.btnInspect.Click += new System.EventHandler(this.btnInspect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(3, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 41);
            this.label2.TabIndex = 2;
            this.label2.Text = "판정";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnInit
            // 
            this.btnInit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnInit.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnInit.Location = new System.Drawing.Point(427, 167);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(221, 54);
            this.btnInit.TabIndex = 11;
            this.btnInit.Text = "초기화";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // toolStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.toolStrip1, 3);
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStatus});
            this.toolStrip1.Location = new System.Drawing.Point(0, 224);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(651, 21);
            this.toolStrip1.TabIndex = 12;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tslStatus
            // 
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(52, 18);
            this.tslStatus.Text = "연결OK";
            // 
            // rhlResult
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.rhlResult, 2);
            this.rhlResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rhlResult.Location = new System.Drawing.Point(196, 129);
            this.rhlResult.Margin = new System.Windows.Forms.Padding(6);
            this.rhlResult.Name = "rhlResult";
            this.rhlResult.Size = new System.Drawing.Size(449, 29);
            this.rhlResult.TabIndex = 13;
            // 
            // serialPort2
            // 
            this.serialPort2.PortName = "COM4";
            this.serialPort2.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort2_DataReceived);
            // 
            // ReceiveInspection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 245);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReceiveInspection";
            this.Text = "입고검사";
            this.Load += new System.EventHandler(this.ReceiveInspection_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBarcodeData;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label txtPart;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblMeanWeight;
        private System.Windows.Forms.Label lblStandWeight;
        private System.IO.Ports.SerialPort serialPort2;
        private System.Windows.Forms.Button btnInspect;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel tslStatus;
        private UserControls.ResultHighlighter rhlResult;
    }
}

