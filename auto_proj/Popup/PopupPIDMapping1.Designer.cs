
namespace auto_proj.Popup
{
    partial class PopupPIDMapping1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblProject = new DevExpress.XtraEditors.LabelControl();
            this.gridSource = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridTarget = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.btnMove = new DevExpress.XtraEditors.SimpleButton();
            this.btnBack = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbCpu = new System.Windows.Forms.ComboBox();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.lblCpuName = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTarget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(108, 29);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "프로젝트 : ";
            // 
            // lblProject
            // 
            this.lblProject.Location = new System.Drawing.Point(127, 13);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(84, 29);
            this.lblProject.TabIndex = 1;
            this.lblProject.Text = "프로젝트";
            // 
            // gridSource
            // 
            this.gridSource.Location = new System.Drawing.Point(13, 141);
            this.gridSource.MainView = this.gridView1;
            this.gridSource.Name = "gridSource";
            this.gridSource.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridSource.Size = new System.Drawing.Size(773, 823);
            this.gridSource.TabIndex = 2;
            this.gridSource.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn5});
            this.gridView1.GridControl = this.gridSource;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "PART";
            this.gridColumn2.FieldName = "part_name";
            this.gridColumn2.MinWidth = 40;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 250;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "PAGE";
            this.gridColumn5.FieldName = "page_name";
            this.gridColumn5.MinWidth = 40;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 412;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit1.ValueGrayed = false;
            // 
            // gridTarget
            // 
            this.gridTarget.Location = new System.Drawing.Point(910, 141);
            this.gridTarget.MainView = this.gridView2;
            this.gridTarget.Name = "gridTarget";
            this.gridTarget.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2});
            this.gridTarget.Size = new System.Drawing.Size(773, 823);
            this.gridTarget.TabIndex = 3;
            this.gridTarget.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn6});
            this.gridView2.GridControl = this.gridTarget;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.MultiSelect = true;
            this.gridView2.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "PART";
            this.gridColumn4.FieldName = "part_name";
            this.gridColumn4.MinWidth = 40;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.FixedWidth = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 250;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "PAGE";
            this.gridColumn6.FieldName = "page_name";
            this.gridColumn6.MinWidth = 40;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 412;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // btnMove
            // 
            this.btnMove.Location = new System.Drawing.Point(795, 405);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(106, 46);
            this.btnMove.TabIndex = 4;
            this.btnMove.Text = ">>";
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(795, 493);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(106, 46);
            this.btnBack.TabIndex = 5;
            this.btnBack.Text = "<<";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(13, 89);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(67, 29);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "CPU : ";
            // 
            // cmbCpu
            // 
            this.cmbCpu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCpu.FormattingEnabled = true;
            this.cmbCpu.Location = new System.Drawing.Point(272, 10);
            this.cmbCpu.Name = "cmbCpu";
            this.cmbCpu.Size = new System.Drawing.Size(335, 37);
            this.cmbCpu.TabIndex = 8;
            this.cmbCpu.Visible = false;
            this.cmbCpu.SelectedIndexChanged += new System.EventHandler(this.cmbCpu_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1533, 89);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 46);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblCpuName
            // 
            this.lblCpuName.Location = new System.Drawing.Point(86, 89);
            this.lblCpuName.Name = "lblCpuName";
            this.lblCpuName.Size = new System.Drawing.Size(84, 29);
            this.lblCpuName.TabIndex = 10;
            this.lblCpuName.Text = "프로젝트";
            // 
            // PopupPIDMapping1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1714, 1005);
            this.Controls.Add(this.lblCpuName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbCpu);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnMove);
            this.Controls.Add(this.gridTarget);
            this.Controls.Add(this.gridSource);
            this.Controls.Add(this.lblProject);
            this.Controls.Add(this.labelControl1);
            this.Name = "PopupPIDMapping1";
            this.Text = "PopupPIDMapping1";
            ((System.ComponentModel.ISupportInitialize)(this.gridSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTarget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblProject;
        private DevExpress.XtraGrid.GridControl gridSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl gridTarget;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SimpleButton btnMove;
        private DevExpress.XtraEditors.SimpleButton btnBack;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.ComboBox cmbCpu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl lblCpuName;
    }
}