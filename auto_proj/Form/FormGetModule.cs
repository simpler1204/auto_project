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
using auto_proj.Popup;
using auto_proj.Classes;
using System.Data.SqlClient;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace auto_proj.Form
{
    public partial class FormGetModule : DevExpress.XtraEditors.XtraForm
    {
        Project project = null;
        PopupSelectProj selectProj = null;
        DataTable dtModuleCount = null;
        DataTable dtTemp = null;
        DataTable dtSelected = null;
        int clickedRow = 0;
        int deleteRow = 0;
        int cpuNumber = 0;

        public FormGetModule()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            selectProj = new PopupSelectProj();
            selectProj.SelectedProj += SelectProj_SelectedProj;
            selectProj.ShowDialog();
            selectProj.SelectedProj -= SelectProj_SelectedProj;           
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

        private void btnSelect_Click_1(object sender, EventArgs e)
        {
            selectProj = new PopupSelectProj();
            selectProj.SelectedProj += SelectProj_SelectedProj;
            selectProj.ShowDialog();
            selectProj.SelectedProj -= SelectProj_SelectedProj;

            if(project != null)
            {
                GetProjectIoCount(project.ProjID);
                SelectModuleCount(project.ProjID);
            }
        }

        private void FormGetModule_Load(object sender, EventArgs e)
        {
            CreateDataTable();           
            gridView1.Click += GridView1_Click;
            gridView2.Click += GridView2_Click;            
        }

        private void SelectModuleCount(int projID)
        {
            if(dtSelected != null) dtSelected.Clear();
            string connectString = SIDS.Instance.MakeConnectionString("DB");
            using(SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("proc_select_module", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@proj_id", SqlDbType.Int).Value = project.ProjID;
                    SqlDataReader reader = cmd.ExecuteReader();

                    dtSelected = new DataTable();
                    dtSelected.Load(reader);

                   /* while (reader.Read())
                    {
                        DataRow row = dtSelected.NewRow();
                        row["TITLE"] = reader["title"].ToString();
                        
                        string part = reader["PART"].ToString();
                        if (part == "CPU")
                        {
                            row["PART"] = part;                           
                        }
                        else
                        {
                            row["PART"] = part;
                            row["AI"] = int.Parse(reader["ai_count"].ToString());
                            row["AO"] = int.Parse(reader["ao_count"].ToString());
                            row["DI"] = int.Parse(reader["di_count"].ToString());
                            row["DO"] = int.Parse(reader["do_count"].ToString());
                        }

                        dtSelected.Rows.Add(row);
                    }*/

                    gridModuleCount.DataSource = dtSelected;
                }

            }
        }

        private void GridView2_Click(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridView view = (GridView)sender;
            GridHitInfo info = view.CalcHitInfo(ea.Location);
            deleteRow = info.RowHandle;
        }

        private void GridView1_Click(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridView view = (GridView)sender;
            GridHitInfo info = view.CalcHitInfo(ea.Location);
            clickedRow = info.RowHandle;

            if (info.InRow || info.InRowCell)
            {
                DataRow row = view.GetDataRow(info.RowHandle);

                string title = row["MAIN_TITLE"].ToString();
                int _ai = int.Parse(row["AI"].ToString());
                int _ao = int.Parse(row["AO"].ToString());
                int _di = int.Parse(row["DI"].ToString());
                int _do = int.Parse(row["DO"].ToString());

                if (title != "")
                {
                    string[] splited = title.Split(' ');
                    cmbPart.Properties.Items.Clear();
                    foreach (string s in splited)
                    {
                        cmbPart.Properties.Items.Add(s);
                    }

                    if (cmbPart.Properties.Items.Count > 0)
                    {
                        cmbPart.SelectedIndex = 0;
                    }
                }
            }
        }

        private void GetProjectIoCount(int projId)
        {
            dtModuleCount.Rows.Clear();
            string connectString = SIDS.Instance.MakeConnectionString("DB");
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                string query = @"SELECT main_title,  ai_count, ao_count, di_count, do_count 
                                 FROM project_detail_io
                                 WHERE project_id = @project_id and sub_title = 'TOTAL MODULE'";

                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@project_id", SqlDbType.Int).Value = projId;

                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            DataRow row = dtModuleCount.NewRow();
                            row["MAIN_TITLE"] = reader["main_title"].ToString();                           
                            row["AI"] = reader["ai_count"].ToString();
                            row["AO"] = reader["ao_count"].ToString();
                            row["DI"] = reader["di_count"].ToString();
                            row["DO"] = reader["do_count"].ToString();
                            dtModuleCount.Rows.Add(row);
                        }

                        gritTotal.DataSource = dtModuleCount;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

        }

        private void CreateDataTable()
        {
            {
                dtModuleCount = new DataTable();
                DataColumn column1 = new DataColumn("MAIN_TITLE", typeof(string));               
                DataColumn column2 = new DataColumn("AI", typeof(int));
                DataColumn column3 = new DataColumn("AO", typeof(int));
                DataColumn column4 = new DataColumn("DI", typeof(int));
                DataColumn column5 = new DataColumn("DO", typeof(int));
                dtModuleCount.Columns.Add(column1);               
                dtModuleCount.Columns.Add(column2);
                dtModuleCount.Columns.Add(column3);
                dtModuleCount.Columns.Add(column4);
                dtModuleCount.Columns.Add(column5);
            }
            {
                dtTemp = new DataTable();
                DataColumn column1 = new DataColumn();
                column1.DataType = typeof(int);
                column1.ColumnName = "IDX";
                column1.AutoIncrement = true;

                DataColumn column2 = new DataColumn("TITLE", typeof(string));
                DataColumn column3 = new DataColumn("PART", typeof(string));
                DataColumn column4 = new DataColumn("CPU", typeof(int));
                DataColumn column5 = new DataColumn("AI", typeof(int));
                DataColumn column6 = new DataColumn("AO", typeof(int));
                DataColumn column7 = new DataColumn("DI", typeof(int));
                DataColumn column8 = new DataColumn("DO", typeof(int));

                dtTemp.Columns.Add(column1);
                dtTemp.Columns.Add(column2);
                dtTemp.Columns.Add(column3);
                dtTemp.Columns.Add(column4);
                dtTemp.Columns.Add(column5);
                dtTemp.Columns.Add(column6);
                dtTemp.Columns.Add(column7);
                dtTemp.Columns.Add(column8);
            }
            //{
            //    dtSelected = new DataTable();
            //    DataColumn column1 = new DataColumn("TITLE", typeof(string));
            //    DataColumn column2 = new DataColumn("PART", typeof(string));
            //    DataColumn column3 = new DataColumn("AI", typeof(int));
            //    DataColumn column4 = new DataColumn("AO", typeof(int));
            //    DataColumn column5 = new DataColumn("DI", typeof(int));
            //    DataColumn column6 = new DataColumn("DO", typeof(int));
            //    dtSelected.Columns.Add(column1);
            //    dtSelected.Columns.Add(column2);
            //    dtSelected.Columns.Add(column3);
            //    dtSelected.Columns.Add(column4);
            //    dtSelected.Columns.Add(column5);
            //    dtSelected.Columns.Add(column6);
            //}
        }

        private void btnCpu_Click(object sender, EventArgs e)
        {
            if (project == null) return;
            if (cmbPart.Text == "") return;

            DataRow row = dtTemp.NewRow();
            //int num = FindCpuIndexFromDtTemp();
            cpuNumber++;
            row["TITLE"] = $"{project.ProjCode}-CPU{0}-100";
            row["PART"] = "CPU";
            row["CPU"] = cpuNumber;
           
           
            dtTemp.Rows.Add(row);
            gridTemp.DataSource = dtTemp;
        }

        private int FindCpuIndexFromDtTemp()
        {
           var results = dtTemp.AsEnumerable().Where(c => c["PART"].ToString().Contains("CPU"));
            return results.Count();
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (project == null) return;
            if (cmbPart.Text == "") return;

            DataRow row = dtTemp.NewRow();
            row["TITLE"] = $"{project.ProjCode}-PLC{clickedRow + 1}-100";
            row["PART"] = cmbPart.Text;
            row["CPU"] = cpuNumber;
            row["AI"] = 0;
            row["AO"] = 0;
            row["DI"] = 0;
            row["DO"] = 0;
            dtTemp.Rows.Add(row);
            gridTemp.DataSource = dtTemp;


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataRow row = gridView2.GetDataRow(deleteRow);
            SelectModuleCount(project.ProjID);
            if (row["PART"].ToString() == "CPU") cpuNumber--;
            dtTemp.Rows.Remove(row);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (project == null) return;
                       
            string connectString = SIDS.Instance.MakeConnectionString("DB");


            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                //먼저 삭제
                using (SqlCommand cmd = new SqlCommand("proc_delete_module", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@proj_id", SqlDbType.Int).Value = project.ProjID;
                    cmd.ExecuteNonQuery();
                }


                using (SqlCommand cmd = new SqlCommand("proc_save_module", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var rows = dtTemp.Select();
                    foreach (var item in rows)
                    {
                        cmd.Parameters.Add("@proj_id", SqlDbType.Int).Value = project.ProjID;
                        cmd.Parameters.Add("@cpu", SqlDbType.Int).Value = item["CPU"];
                        cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = item["TITLE"].ToString();
                        cmd.Parameters.Add("@part", SqlDbType.NVarChar).Value = item["PART"].ToString();
                        
                        if(item["PART"].ToString() == "CPU")
                        {
                            cmd.Parameters.Add("@ai_count", SqlDbType.Int).Value = 0;
                            cmd.Parameters.Add("@ao_count", SqlDbType.Int).Value = 0;
                            cmd.Parameters.Add("@di_count", SqlDbType.Int).Value = 0;
                            cmd.Parameters.Add("@do_count", SqlDbType.Int).Value = 0;
                        }
                        else
                        {
                            cmd.Parameters.Add("@ai_count", SqlDbType.Int).Value = item["AI"].ToString() == "" ? 0 : int.Parse(item["AI"].ToString());
                            cmd.Parameters.Add("@ao_count", SqlDbType.Int).Value = item["AO"].ToString() == "" ? 0 : int.Parse(item["AO"].ToString());
                            cmd.Parameters.Add("@di_count", SqlDbType.Int).Value = item["DI"].ToString() == "" ? 0 : int.Parse(item["DI"].ToString());
                            cmd.Parameters.Add("@do_count", SqlDbType.Int).Value = item["DO"].ToString() == "" ? 0 : int.Parse(item["DO"].ToString());
                        }
                        
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                    }

                    SelectModuleCount(project.ProjID);

                }


            }           
        }
    }
}