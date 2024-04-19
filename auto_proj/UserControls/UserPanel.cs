using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using auto_proj.Classes;
using System.Data.SqlClient;
using auto_proj.Enum;
using auto_proj.Popup;
using DevExpress.Spreadsheet;

namespace auto_proj.UserControls
{
    public partial class UserPanel : UserControl
    {
        PopupPIDMapping1 pidMapping = null;
        DataTable dtList = new DataTable();

        Project _project = null;
        Rail[] _rails = new Rail[2];

        int _cpuId;
        string _cpuName = null;
        int _panelId = -1;
        string _panelName = null;
        int _railCount = 0;
        int railHeight = 0;

        DataTable dtPid = new DataTable();

        public UserPanel(Project project, int cpuId, string cpuName, int panelId, string panelName)
        {
            InitializeComponent();

            this._project = project;
            this._cpuId = cpuId;
            this._cpuName = cpuName;
            this._panelId = panelId;
            this._panelName = panelName;
            this.groupControl1.Text = panelName;           

            this.Load += UserPanel_Load;
        }

        private void UserPanel_Load(object sender, EventArgs e)
        {
            _rails[0] = new Rail(_project, _cpuId, _panelId, 1);
            _rails[0].Location = new Point(10, 30);
            this.groupControl1.Controls.Add(_rails[0]);
            railHeight = _rails[0].Height;
            RailCount++;

            _rails[1] = new Rail(_project, _cpuId, _panelId, 2);
            _rails[1].Location = new Point(10, railHeight - 10);
            this.groupControl1.Controls.Add(_rails[1]);
            RailCount++;

            CreateDataTable();
            GetPid(_project.ProjID, _panelId, _cpuName);
        }

        public int RailCount
        {
            get => this._railCount;
            set
            {
                if (value < 0)
                    this._railCount = 0;
                else if (value > 2)
                    this._railCount = 2;
                else
                    this._railCount = value;
            }
        }

        public int PanelId
        {
            get => this._panelId;
            set
            {
                this._panelId = value;
            }
        }

        public Rail this[int idx]
        {
            get => this._rails[idx];
            set => this._rails[idx] = value;
        }      


       

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this[0] == null && this[1] == null) return;
            if (_project == null) return;
            
            for(int i=0; i<2; i++)
            {
                if (this[i] == null) continue;
                string connectString = SIDS.Instance.MakeConnectionString("DB");

                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.proc_rail_card_delete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;                      

                        cmd.Parameters.Add("@projId", SqlDbType.Int).Value = this[i].Project.ProjID;
                        cmd.Parameters.Add("@cpuId", SqlDbType.Int).Value = this[i].CpuId;
                        cmd.Parameters.Add("@panelId", SqlDbType.Int).Value = this[i].PanelId;
                        cmd.Parameters.Add("@rackNum", SqlDbType.Int).Value = this[i].RackId;
                        cmd.Parameters.Add("@railNum", SqlDbType.Int).Value = this[i].RailId;                      

                        cmd.ExecuteNonQuery();

                    }
                }


                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.proc_insert_card_rail", conn))
                    {
                        IEnumerator<Card> enumerator = this[i].GetEnumerator();
                        cmd.CommandType = CommandType.StoredProcedure;

                        while (enumerator.MoveNext())
                        {
                            Card card = enumerator.Current;

                            if (card.Type == IO_TYPE.AI)
                                cmd.Parameters.Add("@cardType", SqlDbType.Char, 2).Value = "AI";
                            else if (card.Type == IO_TYPE.AO)
                                cmd.Parameters.Add("@cardType", SqlDbType.Char, 2).Value = "AO";
                            else if (card.Type == IO_TYPE.DI)
                                cmd.Parameters.Add("@cardType", SqlDbType.Char, 2).Value = "DI";
                            else if (card.Type == IO_TYPE.DO)
                                cmd.Parameters.Add("@cardType", SqlDbType.Char, 2).Value = "DO";

                           
                            cmd.Parameters.Add("@cardProjId", SqlDbType.Int).Value = card.ProjId;
                            cmd.Parameters.Add("@cardCpuId", SqlDbType.Int).Value = card.CpuId;
                            cmd.Parameters.Add("@cardPanelId", SqlDbType.Int).Value = card.PanelId;
                            cmd.Parameters.Add("@cardRackNum", SqlDbType.Int).Value = card.Rack;
                            cmd.Parameters.Add("@cardRailNum", SqlDbType.Int).Value = card.Rail;
                            cmd.Parameters.Add("@cardSlot", SqlDbType.Int).Value = card.Slot;
                            cmd.Parameters.Add("@cardChannel", SqlDbType.Int).Value = card.Channel;

                            cmd.Parameters.Add("@railProjId", SqlDbType.Int).Value = this[i].Project.ProjID;
                            cmd.Parameters.Add("@railCpuId", SqlDbType.Int).Value = this[i].CpuId;
                            cmd.Parameters.Add("@railPanelId", SqlDbType.Int).Value = this[i].PanelId;
                            cmd.Parameters.Add("@railRack", SqlDbType.Int).Value = card.Rack;
                            cmd.Parameters.Add("@railNum", SqlDbType.Int).Value = card.Rail;
                            cmd.Parameters.Add("@railSlot", SqlDbType.Int).Value = card.Slot;

                            cmd.ExecuteNonQuery();                         

                            cmd.Parameters.Clear();
                        }
                    }
                }
            }

        }

        private void btnPid_Click(object sender, EventArgs e)
        {
            pidMapping = new PopupPIDMapping1(this._project, this._panelId, this._panelName, this._cpuId, this._cpuName);
            pidMapping.SelectedPid += PidMapping_SelectedPid;
            pidMapping.ShowDialog();
            pidMapping.SelectedPid -= PidMapping_SelectedPid;
        }       

       
        private void PidMapping_SelectedPid(object sender, EventArgs e)
        {           
            DataTable dt = ((PopupPIDMapping1.SelectedPidArgs)e).dt;
            gridPid.DataSource = dt;

            dtList.Rows.Clear();
            string connectString = SIDS.Instance.MakeConnectionString("DB");
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.proc_panel_sorted_pnid_insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@projId", SqlDbType.Int).Value = _project.ProjID;
                    cmd.Parameters.Add("@panelId", SqlDbType.Int).Value = _panelId;
                    cmd.Parameters.Add("@cpuId", SqlDbType.Int).Value = _cpuId;
                    cmd.Parameters.Add("@ai", SqlDbType.NVarChar).Value = _project.AiDefine;
                    cmd.Parameters.Add("@ao", SqlDbType.NVarChar).Value = _project.AoDefine;
                    cmd.Parameters.Add("@di", SqlDbType.NVarChar).Value = _project.DiDefine;
                    cmd.Parameters.Add("@do", SqlDbType.NVarChar).Value = _project.DoDefine;
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtList.Load(reader);
                    gridList.DataSource = dtList;
                }
            }
        }
                

        private void GetPid(int projId, int panelId, string cpuName)
        {
            dtPid.Rows.Clear();
            string connectString = SIDS.Instance.MakeConnectionString("DB");
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.proc_pid_select_panel", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@projId", SqlDbType.Int).Value = projId;
                    cmd.Parameters.Add("@panelId", SqlDbType.Int).Value = panelId;
                    cmd.Parameters.Add("@cpuName", SqlDbType.NVarChar).Value = cpuName;


                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DataRow row = dtPid.NewRow();
                        row["id"] = int.Parse(reader["id"].ToString());
                        row["part_name"] = reader["part_name"].ToString();
                        row["page_name"] = reader["page_name"].ToString();
                        dtPid.Rows.Add(row);
                    }

                    gridPid.DataSource = dtPid;                 
                }                
            }

            if (dtPid.Rows.Count > 0)
            {
                dtList.Rows.Clear();
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.proc_panel_sorted_pnid_insert", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@projId", SqlDbType.Int).Value = _project.ProjID;
                        cmd.Parameters.Add("@panelId", SqlDbType.Int).Value = _panelId;
                        cmd.Parameters.Add("@cpuId", SqlDbType.Int).Value = _cpuId;
                        cmd.Parameters.Add("@ai", SqlDbType.NVarChar).Value = _project.AiDefine;
                        cmd.Parameters.Add("@ao", SqlDbType.NVarChar).Value = _project.AoDefine;
                        cmd.Parameters.Add("@di", SqlDbType.NVarChar).Value = _project.DiDefine;
                        cmd.Parameters.Add("@do", SqlDbType.NVarChar).Value = _project.DoDefine;
                        SqlDataReader reader = cmd.ExecuteReader();
                        dtList.Load(reader);
                        gridList.DataSource = dtList;
                    }
                }
            }
        }

        private void CreateDataTable()
        {
            {
                DataColumn column1 = new DataColumn("id", typeof(int));
                DataColumn column2 = new DataColumn("part_name", typeof(string));
                DataColumn column3 = new DataColumn("page_name", typeof(string));
                dtPid.Columns.Add(column1);
                dtPid.Columns.Add(column2);
                dtPid.Columns.Add(column3);
            }

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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;

            string fileTotalName;
            string fileName;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)

            {
                fileTotalName = saveFileDialog.FileName;
                string[] splited = fileTotalName.Split('\\');
                fileName = splited.Last<string>();

                gridList.ExportToXlsx(fileTotalName);
            }


        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.Multiselect = false;
            string fileTotalName = null;
            string fileName;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileTotalName = openFileDialog.FileName;
                string[] splited = fileTotalName.Split('\\');
                fileName = splited.Last<string>();               
            }

            byte[] instExcelBytes;
            instExcelBytes = BinaryFile.GetBinaryFromFile(fileTotalName);

            string connectString = SIDS.Instance.MakeConnectionString("DB");
            string query = @"INSERT INTO excel_document(template) values(@excel)";
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@excel", SqlDbType.VarBinary).Value = instExcelBytes;
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}
