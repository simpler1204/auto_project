
namespace auto_proj.UserControls
{
    partial class Rail
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
            this.btnAi = new DevExpress.XtraEditors.SimpleButton();
            this.btnAo = new DevExpress.XtraEditors.SimpleButton();
            this.btnDi = new DevExpress.XtraEditors.SimpleButton();
            this.btnDo = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblRail = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAi
            // 
            this.btnAi.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAi.Appearance.Options.UseFont = true;
            this.btnAi.Location = new System.Drawing.Point(1264, 6);
            this.btnAi.Margin = new System.Windows.Forms.Padding(6);
            this.btnAi.Name = "btnAi";
            this.btnAi.Size = new System.Drawing.Size(140, 53);
            this.btnAi.TabIndex = 1;
            this.btnAi.Text = "AI";
            this.btnAi.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnAo
            // 
            this.btnAo.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAo.Appearance.Options.UseFont = true;
            this.btnAo.Location = new System.Drawing.Point(1407, 6);
            this.btnAo.Margin = new System.Windows.Forms.Padding(6);
            this.btnAo.Name = "btnAo";
            this.btnAo.Size = new System.Drawing.Size(140, 53);
            this.btnAo.TabIndex = 2;
            this.btnAo.Text = "AO";
            this.btnAo.Click += new System.EventHandler(this.btnAo_Click);
            // 
            // btnDi
            // 
            this.btnDi.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDi.Appearance.Options.UseFont = true;
            this.btnDi.Location = new System.Drawing.Point(1550, 6);
            this.btnDi.Margin = new System.Windows.Forms.Padding(6);
            this.btnDi.Name = "btnDi";
            this.btnDi.Size = new System.Drawing.Size(140, 53);
            this.btnDi.TabIndex = 3;
            this.btnDi.Text = "DI";
            this.btnDi.Click += new System.EventHandler(this.btnDi_Click);
            // 
            // btnDo
            // 
            this.btnDo.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDo.Appearance.Options.UseFont = true;
            this.btnDo.Location = new System.Drawing.Point(1694, 6);
            this.btnDo.Margin = new System.Windows.Forms.Padding(6);
            this.btnDo.Name = "btnDo";
            this.btnDo.Size = new System.Drawing.Size(140, 53);
            this.btnDo.TabIndex = 4;
            this.btnDo.Text = "DO";
            this.btnDo.Click += new System.EventHandler(this.btnDo_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(6, 65);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox1.Size = new System.Drawing.Size(1831, 200);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(21, 16);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(81, 29);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Rack : ";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(208, 16);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(68, 29);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Rail : ";
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(102, 8);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24"});
            this.comboBoxEdit1.Size = new System.Drawing.Size(79, 44);
            this.comboBoxEdit1.TabIndex = 8;
            // 
            // lblRail
            // 
            this.lblRail.Location = new System.Drawing.Point(274, 16);
            this.lblRail.Name = "lblRail";
            this.lblRail.Size = new System.Drawing.Size(13, 29);
            this.lblRail.TabIndex = 9;
            this.lblRail.Text = "0";
            // 
            // Rail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblRail);
            this.Controls.Add(this.comboBoxEdit1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnDo);
            this.Controls.Add(this.btnDi);
            this.Controls.Add(this.btnAo);
            this.Controls.Add(this.btnAi);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Rail";
            this.Size = new System.Drawing.Size(1857, 272);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnAi;
        private DevExpress.XtraEditors.SimpleButton btnAo;
        private DevExpress.XtraEditors.SimpleButton btnDi;
        private DevExpress.XtraEditors.SimpleButton btnDo;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.LabelControl lblRail;
    }
}
