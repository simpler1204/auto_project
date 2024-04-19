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
using auto_proj.UserControls;
using auto_proj.Popup;
using auto_proj.Classes;
using System.Data.SqlClient;
using auto_proj.Enum;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace auto_proj.Form
{
    public partial class FormPLCConfigcs : DevExpress.XtraEditors.XtraForm
    {
        Project project = null;
        PopupSelectProj selectProj = null;
        UserPanel userPanel = null;
        DataTable dtCpuPanel = new DataTable();

        int cpuId = -1;
        string cpuName = null;
        int panelId = -1;
        string panelName = null;
        const int BLANK = 10;

        DataTable dtTemp = new DataTable();

        public FormPLCConfigcs()
        {
            InitializeComponent();
            gridView1.Click += GridView1_Click;
            DoubleBuffered = true;
        }

        private void GridView1_Click(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridView view = (GridView)sender;
            GridHitInfo info = view.CalcHitInfo(ea.Location);
            //int clickedRow = info.RowHandle;

            if (info.InRow || info.InRowCell)
            {
                DataRow row = view.GetDataRow(info.RowHandle);

                cpuId = int.Parse(row["CPU_ID"].ToString());
                cpuName = row["CPU_NAME"].ToString();
                panelId = int.Parse(row["PANEL_ID"].ToString());
                panelName = row["PANEL_NAME"].ToString();

                CreatePanel();
                SelectRailCard(project.ProjID, cpuId, panelId);
            }
       
        }

        private void FormPLCConfigcs_Load(object sender, EventArgs e)
        {            
            ControlsLocationDefine();
            CreateDataTable();
            this.WindowState = FormWindowState.Maximized;
            txtSorting1.Text = SIDS.Instance.GetAppConfigToString("sorting1");
            txtSorting2.Text = SIDS.Instance.GetAppConfigToString("sorting2");
            txtSorting3.Text = SIDS.Instance.GetAppConfigToString("sorting3");
        }

        private void CreateDataTable()
        {
            DataColumn column1 = new DataColumn("rack", typeof(int));
            DataColumn column2 = new DataColumn("rail", typeof(int));
            DataColumn column3 = new DataColumn("slot", typeof(int));
            DataColumn column4 = new DataColumn("card", typeof(int));
            dtTemp.Columns.Add(column1);
            dtTemp.Columns.Add(column2);
            dtTemp.Columns.Add(column3);
            dtTemp.Columns.Add(column4);
        }

        private void ControlsLocationDefine()
        {
            groupControl1.Location = new Point(1,1);
            gridCpuPanel.Location = new Point(1, groupControl1.Height + BLANK);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            selectProj = new PopupSelectProj();
            selectProj.SelectedProj += SelectProj_SelectedProj;
            selectProj.ShowDialog();
            selectProj.SelectedProj -= SelectProj_SelectedProj;

            if (project == null) return;
            
            int rtn = SelectCpuPanel(project.ProjID);          

        }

        private int SelectCpuPanel(int projID)
        {
            string connectString = SIDS.Instance.MakeConnectionString("DB");
            int rows = 0;
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.proc_module_count_select", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@projId", SqlDbType.Int).Value = project.ProjID;
                    SqlDataReader reader = cmd.ExecuteReader();

                    dtCpuPanel.Load(reader);

                    gridCpuPanel.DataSource = dtCpuPanel;
                   
                }
            }

            return rows;
        }

        private void SelectProj_SelectedProj(object sender, EventArgs e)
        {
            project = ((PopupSelectProj.SelectedProjArgs)e).project;
            txtCode.Text = project.ProjCode;
            txtName.Text = project.ProjName;
            txtPlc.Text = project.PlcBrand;
            txtCount.Text = project.PlcCount.ToString();
            txtAi.Text = project.AiDefine;
            txtAo.Text = project.AoDefine;
            txtDi.Text = project.DiDefine;
            txtDo.Text = project.DoDefine;
            txtInst.Text = project.InstFileName;
            txtCreated.Text = project.Created.ToString("yyyy-MM-dd HH:mm:ss");

            txtAiCh.Text = project.AiChannel.ToString();
            txtAoCh.Text = project.AoChannel.ToString();
            txtDiCh.Text = project.DiChannel.ToString();
            txtDoCh.Text = project.DoChannel.ToString();
        }

    
        private void CreatePanel()
        {
            if (project == null) return;           
            if (cpuId == -1) return;
            if (panelName == "") return;

            ReleaseUserPanel();

            Point point = gridCpuPanel.Location;
            int groupHeight = groupControl1.Height;
            int gridHeight = gridCpuPanel.Height;

            userPanel = new UserPanel(project, cpuId, cpuName, panelId, panelName);
            userPanel.Location = new Point(1, groupHeight + gridHeight - 30);
            this.Controls.Add(userPanel);
        }

        private void ReleaseUserPanel()
        {
            if (userPanel == null) return;

            for(int i=0; i < 2; i++)
            {
                userPanel[i].Clear();
            }
            this.Controls.Remove(userPanel);           
            userPanel = null;
        }


        private void gridPanel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void SelectRailCard(int projId, int cpuId, int panelId)
        {          

            dtTemp.Rows.Clear();
            string connectString = SIDS.Instance.MakeConnectionString("DB");
            using(SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand("dbo.proc_rail_select", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@projId", SqlDbType.Int).Value = projId;
                    cmd.Parameters.Add("@cpuId", SqlDbType.Int).Value = cpuId;
                    cmd.Parameters.Add("@panelId", SqlDbType.Int).Value = panelId;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DataRow row = dtTemp.NewRow();
                        row["rack"] = int.Parse(reader["rack_num"].ToString());
                        row["rail"] = int.Parse(reader["rail_num"].ToString());
                        row["slot"] = int.Parse(reader["slot_num"].ToString());
                        row["card"] = int.Parse(reader["card_id"].ToString());
                        dtTemp.Rows.Add(row);
                    }
                    reader.Close();                    
                    cmd.Dispose();
                }

               
                using(SqlCommand cmd = new SqlCommand("dbo.proc_card_select", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (DataRow row in dtTemp.Select())
                    {
                        cmd.Parameters.Add("@cardId", SqlDbType.Int).Value = row["card"];
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            IO_TYPE type = IO_TYPE.AI;
                            if (reader["card_type"].ToString() == "AI")
                                type = IO_TYPE.AI;
                            else if (reader["card_type"].ToString() == "AO")
                                type = IO_TYPE.AO;
                            else if (reader["card_type"].ToString() == "DI")
                                type = IO_TYPE.DI;
                            else if (reader["card_type"].ToString() == "DO")
                                type = IO_TYPE.DO;

                            Card card = new Card(
                                    type,
                                    int.Parse(reader["proj_id"].ToString()),
                                    int.Parse(reader["cpu_id"].ToString()),
                                    int.Parse(reader["panel_id"].ToString()),
                                    int.Parse(reader["rack_num"].ToString()),
                                    int.Parse(reader["rail_num"].ToString()),
                                    int.Parse(reader["slot"].ToString()),
                                    int.Parse(reader["channel"].ToString())
                                );


                            if (int.Parse(reader["rail_num"].ToString()) == 1)
                            {
                                userPanel[0].AddCard(card);
                                userPanel[0].Slot++;
                                userPanel[0].RackId = int.Parse(reader["rack_num"].ToString());
                            }
                            else
                            {
                                userPanel[1].AddCard(card);
                                userPanel[1].Slot++;
                                userPanel[1].RackId = int.Parse(reader["rack_num"].ToString());
                            }
                        }                       
                        cmd.Parameters.Clear();      
                        reader.Close();
                    }                  
                }

                userPanel[0].DrawingCard();
                userPanel[1].DrawingCard();
            }
        }

        private void btnSorting_Click(object sender, EventArgs e)
        {
            string sorting1 = txtSorting1.Text.Trim();
            string sorting2 = txtSorting2.Text.Trim();
            string sorting3 = txtSorting3.Text.Trim();
                        
            SIDS.Instance.SetAppConfig("sorting1", sorting1);
            SIDS.Instance.SetAppConfig("sorting2", sorting2);
            SIDS.Instance.SetAppConfig("sorting3", sorting3);
        }
    }
}