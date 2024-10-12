namespace WinformApp1.UserControls
{
    partial class ResultHighlighter
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblOk = new System.Windows.Forms.Label();
            this.lblNG = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblOk
            // 
            this.lblOk.AutoSize = true;
            this.lblOk.BackColor = System.Drawing.Color.Gray;
            this.lblOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOk.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblOk.ForeColor = System.Drawing.Color.White;
            this.lblOk.Location = new System.Drawing.Point(1, 0);
            this.lblOk.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lblOk.Name = "lblOk";
            this.lblOk.Size = new System.Drawing.Size(210, 89);
            this.lblOk.TabIndex = 6;
            this.lblOk.Text = "OK";
            this.lblOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNG
            // 
            this.lblNG.AutoSize = true;
            this.lblNG.BackColor = System.Drawing.Color.Gray;
            this.lblNG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNG.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblNG.ForeColor = System.Drawing.Color.White;
            this.lblNG.Location = new System.Drawing.Point(215, 0);
            this.lblNG.Name = "lblNG";
            this.lblNG.Size = new System.Drawing.Size(207, 89);
            this.lblNG.TabIndex = 7;
            this.lblNG.Text = "NG";
            this.lblNG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblOk, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblNG, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(425, 89);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // ResultHighlighter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ResultHighlighter";
            this.Size = new System.Drawing.Size(425, 89);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblOk;
        private System.Windows.Forms.Label lblNG;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
