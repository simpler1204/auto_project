
namespace auto_proj.Popup
{
    partial class PopupUnSortedList
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
            this.gridList = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // gridList
            // 
            this.gridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridList.Location = new System.Drawing.Point(0, 0);
            this.gridList.MainView = this.gridView2;
            this.gridList.Name = "gridList";
            this.gridList.Size = new System.Drawing.Size(2451, 1279);
            this.gridList.TabIndex = 6;
            this.gridList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn1,
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
            // gridColumn1
            // 
            this.gridColumn1.Caption = "proj_id";
            this.gridColumn1.FieldName = "proj_id";
            this.gridColumn1.MinWidth = 40;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Width = 150;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "gridColumn3";
            this.gridColumn3.FieldName = "origin_excel_list_id";
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
            this.seq_no.Visible = true;
            this.seq_no.VisibleIndex = 0;
            this.seq_no.Width = 150;
            // 
            // part_name
            // 
            this.part_name.Caption = "PART";
            this.part_name.FieldName = "part_name";
            this.part_name.MinWidth = 40;
            this.part_name.Name = "part_name";
            this.part_name.OptionsColumn.FixedWidth = true;
            this.part_name.Visible = true;
            this.part_name.VisibleIndex = 0;
            this.part_name.Width = 120;
            // 
            // pid
            // 
            this.pid.Caption = "P&ID";
            this.pid.FieldName = "pid";
            this.pid.MinWidth = 40;
            this.pid.Name = "pid";
            this.pid.Visible = true;
            this.pid.VisibleIndex = 1;
            this.pid.Width = 230;
            // 
            // tag
            // 
            this.tag.Caption = "TAG";
            this.tag.FieldName = "tag";
            this.tag.MinWidth = 40;
            this.tag.Name = "tag";
            this.tag.Visible = true;
            this.tag.VisibleIndex = 2;
            this.tag.Width = 230;
            // 
            // description
            // 
            this.description.Caption = "DESCRIPTION";
            this.description.FieldName = "service_description";
            this.description.MinWidth = 40;
            this.description.Name = "description";
            this.description.Visible = true;
            this.description.VisibleIndex = 3;
            this.description.Width = 230;
            // 
            // instrument_type
            // 
            this.instrument_type.Caption = "INSTRUMENT_TYPE";
            this.instrument_type.FieldName = "instrument_type";
            this.instrument_type.MinWidth = 40;
            this.instrument_type.Name = "instrument_type";
            this.instrument_type.Visible = true;
            this.instrument_type.VisibleIndex = 4;
            this.instrument_type.Width = 230;
            // 
            // location
            // 
            this.location.Caption = "LOCATION";
            this.location.FieldName = "location";
            this.location.MinWidth = 40;
            this.location.Name = "location";
            this.location.Visible = true;
            this.location.VisibleIndex = 5;
            this.location.Width = 150;
            // 
            // io_type
            // 
            this.io_type.Caption = "IO_TYPE";
            this.io_type.FieldName = "io_type";
            this.io_type.MinWidth = 40;
            this.io_type.Name = "io_type";
            this.io_type.Visible = true;
            this.io_type.VisibleIndex = 6;
            this.io_type.Width = 230;
            // 
            // system
            // 
            this.system.Caption = "SYSTEM";
            this.system.FieldName = "system";
            this.system.MinWidth = 40;
            this.system.Name = "system";
            this.system.Visible = true;
            this.system.VisibleIndex = 7;
            this.system.Width = 150;
            // 
            // external_power
            // 
            this.external_power.Caption = "POWER";
            this.external_power.FieldName = "external_power";
            this.external_power.MinWidth = 40;
            this.external_power.Name = "external_power";
            this.external_power.OptionsColumn.FixedWidth = true;
            this.external_power.Visible = true;
            this.external_power.VisibleIndex = 8;
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
            this.plc.VisibleIndex = 9;
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
            this.signal_type.VisibleIndex = 10;
            this.signal_type.Width = 100;
            // 
            // remark
            // 
            this.remark.Caption = "REMARK";
            this.remark.FieldName = "remark";
            this.remark.MinWidth = 40;
            this.remark.Name = "remark";
            this.remark.Visible = true;
            this.remark.VisibleIndex = 11;
            this.remark.Width = 150;
            // 
            // PopupUnSortedList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2451, 1279);
            this.Controls.Add(this.gridList);
            this.Name = "PopupUnSortedList";
            this.Text = "PopupUnSortedList";
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
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
    }
}