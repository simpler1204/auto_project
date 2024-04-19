
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
            this.lblRail = new DevExpress.XtraEditors.LabelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.cmbAi = new System.Windows.Forms.ComboBox();
            this.cmbAo = new System.Windows.Forms.ComboBox();
            this.cmbDi = new System.Windows.Forms.ComboBox();
            this.cmbDo = new System.Windows.Forms.ComboBox();
            this.cmbRack = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnAi
            // 
            this.btnAi.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAi.Appearance.Options.UseFont = true;
            this.btnAi.Location = new System.Drawing.Point(600, 6);
            this.btnAi.Margin = new System.Windows.Forms.Padding(6);
            this.btnAi.Name = "btnAi";
            this.btnAi.Size = new System.Drawing.Size(139, 52);
            this.btnAi.TabIndex = 1;
            this.btnAi.Text = "AI";
            this.btnAi.Click += new System.EventHandler(this.btnAi_Click);
            // 
            // btnAo
            // 
            this.btnAo.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAo.Appearance.Options.UseFont = true;
            this.btnAo.Location = new System.Drawing.Point(852, 6);
            this.btnAo.Margin = new System.Windows.Forms.Padding(6);
            this.btnAo.Name = "btnAo";
            this.btnAo.Size = new System.Drawing.Size(139, 52);
            this.btnAo.TabIndex = 2;
            this.btnAo.Text = "AO";
            this.btnAo.Click += new System.EventHandler(this.btnAo_Click);
            // 
            // btnDi
            // 
            this.btnDi.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDi.Appearance.Options.UseFont = true;
            this.btnDi.Location = new System.Drawing.Point(1109, 6);
            this.btnDi.Margin = new System.Windows.Forms.Padding(6);
            this.btnDi.Name = "btnDi";
            this.btnDi.Size = new System.Drawing.Size(139, 52);
            this.btnDi.TabIndex = 3;
            this.btnDi.Text = "DI";
            this.btnDi.Click += new System.EventHandler(this.btnDi_Click);
            // 
            // btnDo
            // 
            this.btnDo.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDo.Appearance.Options.UseFont = true;
            this.btnDo.Location = new System.Drawing.Point(1372, 6);
            this.btnDo.Margin = new System.Windows.Forms.Padding(6);
            this.btnDo.Name = "btnDo";
            this.btnDo.Size = new System.Drawing.Size(139, 52);
            this.btnDo.TabIndex = 4;
            this.btnDo.Text = "DO";
            this.btnDo.Click += new System.EventHandler(this.btnDo_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(6, 64);
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
            this.labelControl1.Location = new System.Drawing.Point(20, 16);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
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
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(68, 29);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Rail : ";
            // 
            // lblRail
            // 
            this.lblRail.Location = new System.Drawing.Point(275, 16);
            this.lblRail.Margin = new System.Windows.Forms.Padding(4);
            this.lblRail.Name = "lblRail";
            this.lblRail.Size = new System.Drawing.Size(13, 29);
            this.lblRail.TabIndex = 9;
            this.lblRail.Text = "0";
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.Location = new System.Drawing.Point(1694, 6);
            this.btnClear.Margin = new System.Windows.Forms.Padding(6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(139, 52);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cmbAi
            // 
            this.cmbAi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAi.FormattingEnabled = true;
            this.cmbAi.Items.AddRange(new object[] {
            "4",
            "8",
            "16",
            "32"});
            this.cmbAi.Location = new System.Drawing.Point(513, 12);
            this.cmbAi.Margin = new System.Windows.Forms.Padding(6);
            this.cmbAi.Name = "cmbAi";
            this.cmbAi.Size = new System.Drawing.Size(80, 32);
            this.cmbAi.TabIndex = 12;
            // 
            // cmbAo
            // 
            this.cmbAo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAo.FormattingEnabled = true;
            this.cmbAo.Items.AddRange(new object[] {
            "4",
            "8",
            "16",
            "32"});
            this.cmbAo.Location = new System.Drawing.Point(767, 12);
            this.cmbAo.Margin = new System.Windows.Forms.Padding(6);
            this.cmbAo.Name = "cmbAo";
            this.cmbAo.Size = new System.Drawing.Size(80, 32);
            this.cmbAo.TabIndex = 13;
            // 
            // cmbDi
            // 
            this.cmbDi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDi.FormattingEnabled = true;
            this.cmbDi.Items.AddRange(new object[] {
            "4",
            "8",
            "16",
            "32"});
            this.cmbDi.Location = new System.Drawing.Point(1023, 12);
            this.cmbDi.Margin = new System.Windows.Forms.Padding(6);
            this.cmbDi.Name = "cmbDi";
            this.cmbDi.Size = new System.Drawing.Size(80, 32);
            this.cmbDi.TabIndex = 14;
            // 
            // cmbDo
            // 
            this.cmbDo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDo.FormattingEnabled = true;
            this.cmbDo.Items.AddRange(new object[] {
            "",
            "4",
            "8",
            "16",
            "32"});
            this.cmbDo.Location = new System.Drawing.Point(1285, 12);
            this.cmbDo.Margin = new System.Windows.Forms.Padding(6);
            this.cmbDo.Name = "cmbDo";
            this.cmbDo.Size = new System.Drawing.Size(80, 32);
            this.cmbDo.TabIndex = 15;
            // 
            // cmbRack
            // 
            this.cmbRack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRack.FormattingEnabled = true;
            this.cmbRack.Items.AddRange(new object[] {
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
            "20"});
            this.cmbRack.Location = new System.Drawing.Point(97, 10);
            this.cmbRack.Margin = new System.Windows.Forms.Padding(6);
            this.cmbRack.Name = "cmbRack";
            this.cmbRack.Size = new System.Drawing.Size(80, 32);
            this.cmbRack.TabIndex = 16;
            this.cmbRack.SelectedValueChanged += new System.EventHandler(this.cmbRack_SelectedValueChanged);
            // 
            // Rail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbRack);
            this.Controls.Add(this.cmbDo);
            this.Controls.Add(this.cmbDi);
            this.Controls.Add(this.cmbAo);
            this.Controls.Add(this.cmbAi);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblRail);
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
        private DevExpress.XtraEditors.LabelControl lblRail;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private System.Windows.Forms.ComboBox cmbAi;
        private System.Windows.Forms.ComboBox cmbAo;
        private System.Windows.Forms.ComboBox cmbDi;
        private System.Windows.Forms.ComboBox cmbDo;
        private System.Windows.Forms.ComboBox cmbRack;
    }
}
