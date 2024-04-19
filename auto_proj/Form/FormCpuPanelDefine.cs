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

namespace auto_proj.Form
{
    public partial class FormCpuPanelDefine : DevExpress.XtraEditors.XtraForm
    {
        Project project = null;
        PopupSelectProj selectProj = null;
        DataTable dtCpu = new DataTable();
        DataTable dtPanel = new DataTable();       
        int selectedCpuId = 0;
        int selectedCpuRow = -1;


        public FormCpuPanelDefine()
        {
            InitializeComponent();
            this.Load += FormCpuPanelDefine_Load;                    
        }


        private void FormCpuPanelDefine_Load(object sender, EventArgs e)
        {
            CreateDataTable();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            selectProj = new PopupSelectProj();
            selectProj.SelectedProj += SelectProj_SelectedProj;
            selectProj.ShowDialog();
            selectProj.SelectedProj -= SelectProj_SelectedProj;

            if (project != null) SelectCpu(project.ProjID);

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

    

        private void btnCreatePanel_Click(object sender, EventArgs e)
        {
            if (project == null) return;
            if (selectedCpuRow < 0) return;
            if (txtPanel.Text.Trim() == "") return;

            string panelName = txtPanel.Text.Trim();

            int row = gridCpu.CurrentRow.Index;

            int.TryParse(gridCpu[0, row].Value.ToString(), out selectedCpuId); // cpu id
            

            string connectString = SIDS.Instance.MakeConnectionString("DB");

            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.proc_insert_panel", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@projId", SqlDbType.Int).Value = project.ProjID;
                        cmd.Parameters.Add("@cpuId", SqlDbType.Int).Value = selectedCpuId;
                        cmd.Parameters.Add("@panelName", SqlDbType.NVarChar).Value = panelName;
                        cmd.ExecuteNonQuery();
                        txtPanel.Text = null;
                    }
                }

                SelectPanel(project.ProjID, selectedCpuId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void CreateDataTable()
        {
            {
                DataColumn column1 = new DataColumn("ID", typeof(int));
                DataColumn column2 = new DataColumn("NAME", typeof(string));
                dtCpu.Columns.Add(column1);
                dtCpu.Columns.Add(column2);
            }
            {
                DataColumn column1 = new DataColumn("ID", typeof(int));
                DataColumn column2 = new DataColumn("NAME", typeof(string));
                dtPanel.Columns.Add(column1);
                dtPanel.Columns.Add(column2);
            }
        }

        private void btnCreateCpu_Click(object sender, EventArgs e)
        {
            if (project == null) return;
            if (txtCpu.Text.Trim() == "") return;         

            if (dtCpu.Rows.Count >= project.PlcCount)
            {
                MessageBox.Show("기준정보의  CPU 수량보다 많을 수 없습니다.", "Error");
                return;
            }

            string cpuName = txtCpu.Text.Trim();    
            string connectString = SIDS.Instance.MakeConnectionString("DB");

            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.proc_insert_cpu", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@projId", SqlDbType.Int).Value = project.ProjID;
                        cmd.Parameters.Add("@cpuName", SqlDbType.NVarChar).Value = cpuName;
                        cmd.ExecuteNonQuery();
                        txtCpu.Text = "";
                    }
                }

                SelectCpu(project.ProjID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SelectCpu(int projId)
        {
            if (project == null) return;

            dtCpu.Rows.Clear();

            string connectString = SIDS.Instance.MakeConnectionString("DB");

            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.proc_select_cpu", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@projId", SqlDbType.Int).Value = project.ProjID;
                        
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DataRow row = dtCpu.NewRow();
                            row["ID"] = int.Parse(reader["id"].ToString());
                            row["NAME"] = reader["cpu_name"].ToString();
                            dtCpu.Rows.Add(row);
                        }

                        gridCpu.DataSource = dtCpu;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            DataGridViewTextBoxColumn idColumn = (DataGridViewTextBoxColumn)gridCpu.Columns["ID"];
            DataGridViewTextBoxColumn nameColumn = (DataGridViewTextBoxColumn)gridCpu.Columns["NAME"];
            idColumn.Width = 70;
            idColumn.Visible = false;
            nameColumn.Width = 500;
        }

        private void SelectPanel(int projId, int cpuId)
        {
            if (project == null) return;

            dtPanel.Rows.Clear();

            string connectString = SIDS.Instance.MakeConnectionString("DB");

            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.proc_select_panel", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@projId", SqlDbType.Int).Value = project.ProjID;
                        cmd.Parameters.Add("@cpuId", SqlDbType.Int).Value = cpuId;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DataRow row = dtPanel.NewRow();
                            row["ID"] = int.Parse(reader["id"].ToString());
                            row["NAME"] = reader["panel_name"].ToString();
                            dtPanel.Rows.Add(row);
                        }

                        gridPanel.DataSource = dtPanel;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            DataGridViewTextBoxColumn idColumn = (DataGridViewTextBoxColumn)gridPanel.Columns["ID"];
            DataGridViewTextBoxColumn nameColumn = (DataGridViewTextBoxColumn)gridPanel.Columns["NAME"];
            idColumn.Width = 70;
            idColumn.Visible = false;
            nameColumn.Width = 500;
        }

        private void gridCpu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            selectedCpuRow = e.RowIndex;
            string cellValue = gridCpu[0, e.RowIndex].Value.ToString();
            int.TryParse(cellValue, out selectedCpuId);
            SelectPanel(project.ProjID, selectedCpuId);
        }

        private void btnDeleteCpu_Click(object sender, EventArgs e)
        {
            if (project == null) return;
            if (selectedCpuRow < 0) return;            

            string connectString = SIDS.Instance.MakeConnectionString("DB");
            int rtn = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.proc_delete_cpu", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@projId", SqlDbType.Int).Value = project.ProjID;
                        cmd.Parameters.Add("@cpuId", SqlDbType.Int).Value = selectedCpuId;

                        rtn = cmd.ExecuteNonQuery();
                    }

                    if (rtn > 0)
                    {
                        DataTable dtTemp = null;

                        if(dtCpu.Rows.Count > 0)
                        {
                            dtTemp = dtCpu.AsEnumerable().Where(v => (int)v["ID"] != selectedCpuId).CopyToDataTable();
                            gridCpu.DataSource = dtTemp;
                            dtCpu = dtTemp.Clone();
                        }
                        else
                        {
                            gridCpu.DataSource = dtCpu;
                        }
                             

                        if (gridCpu.Rows.Count > 0)
                        {
                            int currentRow = gridCpu.CurrentRow.Index;
                            int.TryParse(gridCpu[0, currentRow].Value.ToString(), out selectedCpuId);
                            SelectPanel(project.ProjID, selectedCpuId);
                        }
                        else
                        {
                            dtPanel.Rows.Clear();
                            gridPanel.Refresh();
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDeletePanel_Click(object sender, EventArgs e)
        {

        }
    }
}