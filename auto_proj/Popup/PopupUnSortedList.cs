using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace auto_proj.Popup
{
    public partial class PopupUnSortedList : DevExpress.XtraEditors.XtraForm
    {
        DataTable dtList = new DataTable();

        int _projId = 0;
        public PopupUnSortedList(int projId)
        {
            InitializeComponent();
            this._projId = projId;
            this.Load += PopupUnSortedList_Load;
        }

        private void PopupUnSortedList_Load(object sender, EventArgs e)
        {
            CreateDataTable();
            SelectUnSortedList(this._projId);
        }

        private void SelectUnSortedList(int projId)
        {
            string connectString = SIDS.Instance.MakeConnectionString("DB");
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.proc_sorting_list_total", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@projId", SqlDbType.Int).Value = projId;
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtList.Load(reader);
                    gridList.DataSource = dtList;
                }
            }
        }

        private void CreateDataTable()
        {
            {
                DataColumn column1 = new DataColumn("id", typeof(int));
                DataColumn column2 = new DataColumn("proj_id", typeof(string));
                DataColumn column3 = new DataColumn("origin_excel_list_id", typeof(string));
                DataColumn column4 = new DataColumn("seq_no", typeof(int));
                DataColumn column5 = new DataColumn("part_name", typeof(string));
                DataColumn column6 = new DataColumn("pid", typeof(string));
                DataColumn column7 = new DataColumn("tag", typeof(string));
                DataColumn column8 = new DataColumn("service_description", typeof(string));
                DataColumn column9 = new DataColumn("instrument_type", typeof(string));
                DataColumn column10 = new DataColumn("location", typeof(string));
                DataColumn column11 = new DataColumn("io_type", typeof(string));
                DataColumn column12 = new DataColumn("system", typeof(string));
                DataColumn column13 = new DataColumn("external_power", typeof(string));
                DataColumn column14 = new DataColumn("plc", typeof(string));
                DataColumn column15 = new DataColumn("signal_type", typeof(string));
                DataColumn column16 = new DataColumn("remark", typeof(string));
                dtList.Columns.Add(column1);
                dtList.Columns.Add(column2);
                dtList.Columns.Add(column3);
                dtList.Columns.Add(column4);
                dtList.Columns.Add(column5);
                dtList.Columns.Add(column6);
                dtList.Columns.Add(column7);
                dtList.Columns.Add(column8);
                dtList.Columns.Add(column9);
                dtList.Columns.Add(column10);
                dtList.Columns.Add(column11);
                dtList.Columns.Add(column12);
                dtList.Columns.Add(column13);
                dtList.Columns.Add(column14);
                dtList.Columns.Add(column15);
                dtList.Columns.Add(column16);
            }
        }
    }
}