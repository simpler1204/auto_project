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
using auto_proj.Classes;
using auto_proj.Popup;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;
//using Microsoft.Office.Interop.Excel;
using System.IO;
using DevExpress.Spreadsheet;

namespace auto_proj.Form
{
    public partial class listProcess : DevExpress.XtraEditors.XtraForm
    {
        Project project = null;
        PopupSelectProj selectProj = null;

        const int NO = 0;
        const int FLOOR = 1;
        const int CPU = 2;
        const int PANEL = 3;
        const int RACK = 4;
        const int RAIL = 5;
        const int SLOT = 6;
        const int CH = 7;
        const int IO = 8;
        const int ADDRESS = 9;
        const int SIGNAL1 = 10;
        const int SIGNAL2 = 11;
        const int POWER = 12;
        const int PNID = 13;
        const int TAG = 14;
        const int DESC1 = 15;
        const int DESC2 = 16;
        const int LOCATION = 17;
        const int IO_TYPE = 18;
        const int SYSTEM = 19;
        const int UNIT = 20;
        const int REMARK = 21;
        public listProcess()
        {
            InitializeComponent();
            this.Load += FormSorting_Load;
        }

        private void FormSorting_Load(object sender, EventArgs e)
        {


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



        private void btnCreate_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;

            string fileTotalName = null;
            byte[] templateBytes = null;


            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileTotalName = saveFileDialog.FileName;

                string connectString = SIDS.Instance.MakeConnectionString("DB");
                string query = "SELECT template from excel_document";
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            templateBytes = (byte[])reader[0];
                        }
                        reader.Close();                        
                        cmd.Dispose();
                    }
                    conn.Close();
                    
                }

                if (templateBytes != null)
                {
                    BinaryFile.MakeFileFromBinary(templateBytes, fileTotalName);
                    listBox1.Items.Add("1. Template 파일 초기화.");
                }
                else
                {
                    listBox1.Items.Add("1. Template 파일 초기화 실패.");
                    return;
                }
               

                DoMakeExcel(project.ProjID, fileTotalName);


                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.proc_template_document_insert", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@projId", SqlDbType.Int).Value = project.ProjID;
                        cmd.Parameters.Add("@ai", SqlDbType.NVarChar).Value = project.AiDefine;
                        cmd.Parameters.Add("@ao", SqlDbType.NVarChar).Value = project.AoDefine;
                        cmd.Parameters.Add("@di", SqlDbType.NVarChar).Value = project.DiDefine;
                        cmd.Parameters.Add("@do", SqlDbType.NVarChar).Value = project.DoDefine;

                        cmd.ExecuteNonQuery();

                    }
                    conn.Close();

                }

                DoFillExcelData(project.ProjID, fileTotalName);

                FileInfo fi = new FileInfo(fileTotalName);
                if (fi.Exists)
                {
                    System.Diagnostics.Process.Start(fileTotalName);
                }

            }
        }

        private void DoFillExcelData(int projID, string fileTotalName)
        {

            Worksheet sheet = null;
            using (Workbook wb = new Workbook())
            {
                wb.LoadDocument(fileTotalName);
                wb.History.IsEnabled = false;

                foreach (var s in wb.Worksheets)
                {
                    if (s.Name == "REV.0")
                        sheet = s;
                }
                if (sheet == null) return;

                string connectString = SIDS.Instance.MakeConnectionString("DB");              

                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.proc_template_document_select", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@projId", SqlDbType.Int).Value = project.ProjID;
                        SqlDataReader reader = cmd.ExecuteReader();
                        int i = 1;
                        while (reader.Read())
                        {
                            sheet.Cells[i, NO].Value = int.Parse(reader["NO"].ToString());
                            sheet.Cells[i, FLOOR].Value = "-";
                            sheet.Cells[i, CPU].Value = reader["CPU"].ToString();
                            sheet.Cells[i, PANEL].Value = reader["REMOTE_PANEL"].ToString();
                            sheet.Cells[i, RACK].Value = int.Parse(reader["TOTAL_RACK"].ToString());
                            sheet.Cells[i, RAIL].Value = int.Parse(reader["RACK"].ToString());
                            sheet.Cells[i, SLOT].Value = int.Parse(reader["SLOT"].ToString());
                            sheet.Cells[i, CH].Value = int.Parse(reader["CH"].ToString());
                            sheet.Cells[i, IO].Value = reader["IO"].ToString();
                            sheet.Cells[i, ADDRESS].Value = reader["ADDRESS"].ToString();
                            sheet.Cells[i, SIGNAL1].Value = reader["SIGNAL_TYPE1"].ToString();
                            sheet.Cells[i, SIGNAL2].Value = reader["SIGNAL_TYPE2"].ToString();
                            sheet.Cells[i, POWER].Value = reader["POWER_SOURCE"].ToString();
                            sheet.Cells[i, PNID].Value = reader["PNID"].ToString();
                            sheet.Cells[i, TAG].Value = reader["TAG"].ToString();
                            sheet.Cells[i, DESC1].Value = reader["DESCRIPTION1"].ToString();
                            sheet.Cells[i, DESC2].Value = reader["DESCRIPTION2"].ToString();
                            sheet.Cells[i, LOCATION].Value = reader["LOCATION"].ToString();
                            sheet.Cells[i, IO_TYPE].Value = reader["IO_TYPE"].ToString();
                            sheet.Cells[i, SYSTEM].Value = reader["SYSTEM"].ToString();
                            sheet.Cells[i, UNIT].Value = reader["UNIT"].ToString();
                            sheet.Cells[i, REMARK].Value = reader["REMARK"].ToString();
                            i++;
                        }
                    }
                    conn.Close();
                }

                wb.SaveDocument(fileTotalName);

            }

           

        }

        private void DoMakeExcel(int projID, string fileTotalName)
        {           

            Worksheet sheet = null;
            using (Workbook wb = new Workbook())
            {
                wb.LoadDocument(fileTotalName);
                wb.History.IsEnabled = false;

                foreach (var s in wb.Worksheets)
                {
                    if (s.Name == "REV.0")
                        sheet = s;
                }

                if (sheet == null) return;

                string connectString = SIDS.Instance.MakeConnectionString("DB");
                using(SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.proc_create_template", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@projId", SqlDbType.Int).Value = project.ProjID;
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    conn.Close();
                    listBox1.Items.Add("2. Card 생성 완료");                   
                }

                wb.SaveDocument(fileTotalName);

            }

           

        }
    }
}