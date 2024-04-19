
namespace auto_proj.UserControls
{
    partial class Card
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
            this.lblCardName = new System.Windows.Forms.Label();
            this.lblChannel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSlot = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCardName
            // 
            this.lblCardName.AutoSize = true;
            this.lblCardName.Location = new System.Drawing.Point(6, 50);
            this.lblCardName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCardName.Name = "lblCardName";
            this.lblCardName.Size = new System.Drawing.Size(16, 12);
            this.lblCardName.TabIndex = 0;
            this.lblCardName.Text = "AI";
            // 
            // lblChannel
            // 
            this.lblChannel.AutoSize = true;
            this.lblChannel.Location = new System.Drawing.Point(8, 74);
            this.lblChannel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(11, 12);
            this.lblChannel.TabIndex = 1;
            this.lblChannel.Text = "8";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Controls.Add(this.lblSlot);
            this.groupBox1.Controls.Add(this.lblCardName);
            this.groupBox1.Controls.Add(this.lblChannel);
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(1, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(34, 98);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // lblSlot
            // 
            this.lblSlot.AutoSize = true;
            this.lblSlot.Location = new System.Drawing.Point(6, 18);
            this.lblSlot.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSlot.Name = "lblSlot";
            this.lblSlot.Size = new System.Drawing.Size(16, 12);
            this.lblSlot.TabIndex = 2;
            this.lblSlot.Text = "AI";
            // 
            // Card
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Card";
            this.Size = new System.Drawing.Size(38, 100);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblCardName;
        public System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label lblSlot;
    }
}
