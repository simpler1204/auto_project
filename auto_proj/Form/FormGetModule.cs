﻿using DevExpress.XtraEditors;
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
       // int clickedRow = 0;
        int deleteRow = 0;
       // int cpuNumber = 0;

        public FormGetModule()
        {
            InitializeComponent();
        }

        //private void btnSelect_Click(object sender, EventArgs e)
        //{
        //    selectProj = new PopupSelectProj();
        //    selectProj.SelectedProj += SelectProj_SelectedProj;
        //    selectProj.ShowDialog();
        //    selectProj.SelectedProj -= SelectProj_SelectedProj;           
        //}

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
            dtTemp.Rows.Clear();
            string connectString = SIDS.Instance.MakeConnectionString("DB");
            using(SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.proc_select_cpu_panel", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@projId", SqlDbType.Int).Value = project.ProjID;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        DataRow row = dtTemp.NewRow();
                        row["CPU_ID"] = reader["cpu_id"].ToString();
                        row["CPU_NAME"] = reader["cpu_name"].ToString();
                        row["PANEL_ID"] = reader["panel_id"].ToString();
                        row["PANEL_NAME"] = reader["panel_name"].ToString();
                        row["PART"] = reader["part"].ToString();
                        row["AI"] = reader["ai_count"].ToString();
                        row["AO"] = reader["ao_count"].ToString();
                        row["DI"] = reader["di_count"].ToString();
                        row["DO"] = reader["do_count"].ToString();

                        dtTemp.Rows.Add(row);
                    }

                    gridTemp.DataSource = dtTemp;
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
            //DXMouseEventArgs ea = e as DXMouseEventArgs;
            //GridView view = (GridView)sender;
            //GridHitInfo info = view.CalcHitInfo(ea.Location);
            //clickedRow = info.RowHandle;

            //if (info.InRow || info.InRowCell)
            //{
            //    DataRow row = view.GetDataRow(info.RowHandle);

            //    string title = row["MAIN_TITLE"].ToString();
            //    int _ai = int.Parse(row["AI"].ToString());
            //    int _ao = int.Parse(row["AO"].ToString());
            //    int _di = int.Parse(row["DI"].ToString());
            //    int _do = int.Parse(row["DO"].ToString());

            //    if (title != "")
            //    {
            //        string[] splited = title.Split(' ');
            //        cmbPart.Properties.Items.Clear();
            //        foreach (string s in splited)
            //        {
            //            cmbPart.Properties.Items.Add(s);
            //        }

            //        if (cmbPart.Properties.Items.Count > 0)
            //        {
            //            cmbPart.SelectedIndex = 0;
            //        }
            //    }
            //}
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
                DataColumn column1 = new DataColumn("CPU_ID", typeof(int));
                DataColumn column2 = new DataColumn("CPU_NAME", typeof(string));
                DataColumn column3 = new DataColumn("PANEL_ID", typeof(int));
                DataColumn column4 = new DataColumn("PANEL_NAME", typeof(string));
                DataColumn column5 = new DataColumn("PART", typeof(string));                
                DataColumn column6 = new DataColumn("AI", typeof(int));
                DataColumn column7 = new DataColumn("AO", typeof(int));
                DataColumn column8 = new DataColumn("DI", typeof(int));
                DataColumn column9 = new DataColumn("DO", typeof(int));

                dtTemp.Columns.Add(column1);
                dtTemp.Columns.Add(column2);
                dtTemp.Columns.Add(column3);
                dtTemp.Columns.Add(column4);
                dtTemp.Columns.Add(column5);
                dtTemp.Columns.Add(column6);
                dtTemp.Columns.Add(column7);
                dtTemp.Columns.Add(column8);
                dtTemp.Columns.Add(column9);
            }
   
        }

        private void btnCpu_Click(object sender, EventArgs e)
        {
        }

        private int FindCpuIndexFromDtTemp()
        {
           var results = dtTemp.AsEnumerable().Where(c => c["PART"].ToString().Contains("CPU"));
            return results.Count();
            
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (project == null) return;
            
            DataRow[] rows = dtTemp.Select();

            string connectString = SIDS.Instance.MakeConnectionString("DB");

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                //먼저 삭제
                using (SqlCommand cmd = new SqlCommand("dbo.proc_module_count_insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (var item in rows)
                    {
                        cmd.Parameters.Add("@projId", SqlDbType.Int).Value = project.ProjID;
                        cmd.Parameters.Add("@cpuId", SqlDbType.Int).Value = item["CPU_ID"];
                        cmd.Parameters.Add("@panelId", SqlDbType.Int).Value = item["PANEL_ID"];
                        cmd.Parameters.Add("@part", SqlDbType.NVarChar).Value = item["PART"];
                        cmd.Parameters.Add("@aiCount", SqlDbType.Int).Value = item["AI"];
                        cmd.Parameters.Add("@aoCount", SqlDbType.Int).Value = item["AO"];
                        cmd.Parameters.Add("@diCount", SqlDbType.Int).Value = item["DI"];
                        cmd.Parameters.Add("@doCount", SqlDbType.Int).Value = item["DO"];
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    
                }

            }

        }
    }
}