﻿
namespace auto_proj.UserControls
{
    partial class UserPanel
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnPid = new DevExpress.XtraEditors.SimpleButton();
            this.gridPid = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridList = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.seq_no = new DevExpress.XtraGrid.Columns.GridColumn();
            this.part_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.description = new DevExpress.XtraGrid.Columns.GridColumn();
            this.instrument_type = new DevExpress.XtraGrid.Columns.GridColumn();
            this.location = new DevExpress.XtraGrid.Columns.GridColumn();
            this.io_type = new DevExpress.XtraGrid.Columns.GridColumn();
            this.system = new DevExpress.XtraGrid.Columns.GridColumn();
            this.external_power = new DevExpress.XtraGrid.Columns.GridColumn();
            this.plc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.signal_type = new DevExpress.XtraGrid.Columns.GridColumn();
            this.remark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnSave);
            this.groupControl1.Controls.Add(this.btnPid);
            this.groupControl1.Location = new System.Drawing.Point(2, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1915, 722);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Panel";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(169, 660);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 46);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPid
            // 
            this.btnPid.Location = new System.Drawing.Point(11, 660);
            this.btnPid.Margin = new System.Windows.Forms.Padding(4);
            this.btnPid.Name = "btnPid";
            this.btnPid.Size = new System.Drawing.Size(150, 46);
            this.btnPid.TabIndex = 1;
            this.btnPid.Text = "PID Mapping";
            this.btnPid.Click += new System.EventHandler(this.btnPid_Click);
            // 
            // gridPid
            // 
            this.gridPid.Location = new System.Drawing.Point(1924, 0);
            this.gridPid.MainView = this.gridView1;
            this.gridPid.Name = "gridPid";
            this.gridPid.Size = new System.Drawing.Size(427, 722);
            this.gridPid.TabIndex = 2;
            this.gridPid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.gridView1.GridControl = this.gridPid;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "PID";
            this.gridColumn1.FieldName = "page_name";
            this.gridColumn1.MinWidth = 40;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 150;
            // 
            // gridList
            // 
            this.gridList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridList.Location = new System.Drawing.Point(3, 729);
            this.gridList.MainView = this.gridView2;
            this.gridList.Name = "gridList";
            this.gridList.Size = new System.Drawing.Size(2592, 431);
            this.gridList.TabIndex = 7;
            this.gridList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3,
            this.seq_no,
            this.part_name,
            this.pid,
            this.tag,
            this.description,
            this.instrument_type,
            this.location,
            this.io_type,
            this.system,
            this.external_power,
            this.plc,
            this.signal_type,
            this.remark});
            this.gridView2.GridControl = this.gridList;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "ID";
            this.gridColumn2.FieldName = "id";
            this.gridColumn2.MinWidth = 40;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Width = 150;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "proj_id";
            this.gridColumn3.FieldName = "proj_id";
            this.gridColumn3.MinWidth = 40;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Width = 150;
            // 
            // seq_no
            // 
            this.seq_no.Caption = "SEQ";
            this.seq_no.FieldName = "seq_no";
            this.seq_no.MinWidth = 40;
            this.seq_no.Name = "seq_no";
            this.seq_no.OptionsColumn.FixedWidth = true;
            this.seq_no.Visible = true;
            this.seq_no.VisibleIndex = 0;
            this.seq_no.Width = 100;
            // 
            // part_name
            // 
            this.part_name.Caption = "PART";
            this.part_name.FieldName = "part_name";
            this.part_name.MinWidth = 40;
            this.part_name.Name = "part_name";
            this.part_name.OptionsColumn.FixedWidth = true;
            this.part_name.Visible = true;
            this.part_name.VisibleIndex = 1;
            this.part_name.Width = 120;
            // 
            // pid
            // 
            this.pid.Caption = "P&ID";
            this.pid.FieldName = "pid";
            this.pid.MinWidth = 40;
            this.pid.Name = "pid";
            this.pid.Visible = true;
            this.pid.VisibleIndex = 2;
            this.pid.Width = 263;
            // 
            // tag
            // 
            this.tag.Caption = "TAG";
            this.tag.FieldName = "tag";
            this.tag.MinWidth = 40;
            this.tag.Name = "tag";
            this.tag.Visible = true;
            this.tag.VisibleIndex = 3;
            this.tag.Width = 263;
            // 
            // description
            // 
            this.description.Caption = "DESCRIPTION";
            this.description.FieldName = "service_description";
            this.description.MinWidth = 40;
            this.description.Name = "description";
            this.description.Visible = true;
            this.description.VisibleIndex = 4;
            this.description.Width = 263;
            // 
            // instrument_type
            // 
            this.instrument_type.Caption = "INSTRUMENT_TYPE";
            this.instrument_type.FieldName = "instrument_type";
            this.instrument_type.MinWidth = 40;
            this.instrument_type.Name = "instrument_type";
            this.instrument_type.Visible = true;
            this.instrument_type.VisibleIndex = 5;
            this.instrument_type.Width = 263;
            // 
            // location
            // 
            this.location.Caption = "LOCATION";
            this.location.FieldName = "location";
            this.location.MinWidth = 40;
            this.location.Name = "location";
            this.location.Visible = true;
            this.location.VisibleIndex = 6;
            this.location.Width = 171;
            // 
            // io_type
            // 
            this.io_type.Caption = "IO_TYPE";
            this.io_type.FieldName = "io_type";
            this.io_type.MinWidth = 40;
            this.io_type.Name = "io_type";
            this.io_type.Visible = true;
            this.io_type.VisibleIndex = 7;
            this.io_type.Width = 263;
            // 
            // system
            // 
            this.system.Caption = "SYSTEM";
            this.system.FieldName = "system";
            this.system.MinWidth = 40;
            this.system.Name = "system";
            this.system.Visible = true;
            this.system.VisibleIndex = 8;
            this.system.Width = 171;
            // 
            // external_power
            // 
            this.external_power.Caption = "POWER";
            this.external_power.FieldName = "external_power";
            this.external_power.MinWidth = 40;
            this.external_power.Name = "external_power";
            this.external_power.OptionsColumn.FixedWidth = true;
            this.external_power.Visible = true;
            this.external_power.VisibleIndex = 9;
            this.external_power.Width = 200;
            // 
            // plc
            // 
            this.plc.Caption = "PLC";
            this.plc.FieldName = "plc";
            this.plc.MinWidth = 40;
            this.plc.Name = "plc";
            this.plc.OptionsColumn.FixedWidth = true;
            this.plc.Visible = true;
            this.plc.VisibleIndex = 10;
            this.plc.Width = 100;
            // 
            // signal_type
            // 
            this.signal_type.Caption = "SIGNAL";
            this.signal_type.FieldName = "signal_type";
            this.signal_type.MinWidth = 40;
            this.signal_type.Name = "signal_type";
            this.signal_type.OptionsColumn.FixedWidth = true;
            this.signal_type.Visible = true;
            this.signal_type.VisibleIndex = 11;
            this.signal_type.Width = 100;
            // 
            // remark
            // 
            this.remark.Caption = "REMARK";
            this.remark.FieldName = "remark";
            this.remark.MinWidth = 40;
            this.remark.Name = "remark";
            this.remark.Visible = true;
            this.remark.VisibleIndex = 12;
            this.remark.Width = 179;
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(2445, 677);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(150, 46);
            this.btnExcel.TabIndex = 8;
            this.btnExcel.Text = "Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(2379, 456);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(150, 46);
            this.simpleButton1.TabIndex = 9;
            this.simpleButton1.Text = "Excel";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // UserPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.gridList);
            this.Controls.Add(this.gridPid);
            this.Controls.Add(this.groupControl1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "UserPanel";
            this.Size = new System.Drawing.Size(2605, 1261);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnPid;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraGrid.GridControl gridPid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.GridControl gridList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn seq_no;
        private DevExpress.XtraGrid.Columns.GridColumn part_name;
        private DevExpress.XtraGrid.Columns.GridColumn pid;
        private DevExpress.XtraGrid.Columns.GridColumn tag;
        private DevExpress.XtraGrid.Columns.GridColumn description;
        private DevExpress.XtraGrid.Columns.GridColumn instrument_type;
        private DevExpress.XtraGrid.Columns.GridColumn location;
        private DevExpress.XtraGrid.Columns.GridColumn io_type;
        private DevExpress.XtraGrid.Columns.GridColumn system;
        private DevExpress.XtraGrid.Columns.GridColumn external_power;
        private DevExpress.XtraGrid.Columns.GridColumn plc;
        private DevExpress.XtraGrid.Columns.GridColumn signal_type;
        private DevExpress.XtraGrid.Columns.GridColumn remark;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}
