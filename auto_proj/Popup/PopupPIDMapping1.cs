using auto_proj.Classes;
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
    public partial class PopupPIDMapping1 : DevExpress.XtraEditors.XtraForm
    {
        public event EventHandler SelectedPid;
        List<string> partList = new List<string>();

        Project _project;
        int _panelId;
        string _panelName = null;
        int _cpuId;
        string _cpuName = null;


        DataTable dtSource = new DataTable();
        DataTable dtTarget = new DataTable();


        public class SelectedPidArgs : EventArgs
        {
            public DataTable dt;
        }
       
        public PopupPIDMapping1(Project project, int panelId, string panelName, int cpuId, string cpuName)
        {
            InitializeComponent();
            this._project = project;
            this._panelId = panelId;
            this._panelName = panelName;
            this._cpuId = cpuId;
            this._cpuName = cpuName;
            this.Load += PopupPIDMapping1_Load;
            this.FormClosing += PopupPIDMapping1_FormClosing;
        }

        private void PopupPIDMapping1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SelectedPidArgs args = new SelectedPidArgs();
            args.dt = dtTarget;
            SelectedPid(this, args);
        }

        private void PopupPIDMapping1_Load(object sender, EventArgs e)
        {

            CreateDataTable();
            SelectCpuCombo();
            cmbCpu.SelectedValueChanged += CmbCpu_SelectedValueChanged;
            CreatePartCheckBox(_project.ProjID);

            if (_project != null)
                GetPidPage(_project.ProjID, _cpuName);
            lblCpuName.Text = this._cpuName;
        }

        private void CmbCpu_SelectedValueChanged(object sender, EventArgs e)
        {
            _cpuName = cmbCpu.SelectedItem.ToString();

            if (_project != null)
                GetPidPage(_project.ProjID, _cpuName);
        }

        private void SelectCpuCombo()
        {
            string connectString = SIDS.Instance.MakeConnectionString("DB");
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand("dbo.proc_cpu_select_combo", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@projId", SqlDbType.Int).Value = _project.ProjID;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cmbCpu.Items.Add(reader["cpu_name"].ToString());
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
                dtSource.Columns.Add(column1);
                dtSource.Columns.Add(column2);
                dtSource.Columns.Add(column3);
            }

            dtTarget = dtSource.Clone();
        }

        private void GetPidPage(int projID, string cpuName)
        {
            dtSource.Rows.Clear();
            string connectString = SIDS.Instance.MakeConnectionString("DB");
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand("dbo.proc_pid_select_all", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@projId", SqlDbType.Int).Value = this._project.ProjID;
                    cmd.Parameters.Add("@cpuName", SqlDbType.NVarChar).Value = cpuName;
                    cmd.Parameters.Add("@panelId", SqlDbType.NVarChar).Value = this._panelId;
                    cmd.Parameters.Add("@partNames", SqlDbType.NVarChar).Value = MakePartNames(partList);                    

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DataRow row = dtSource.NewRow();
                        row["id"] = int.Parse(reader["id"].ToString());
                        row["part_name"] = reader["part_name"].ToString();
                        row["page_name"] = reader["page_name"].ToString();
                        dtSource.Rows.Add(row);
                    }                    
                    gridSource.DataSource = dtSource;
                }

                GetPid(this._project.ProjID, this._panelId, cpuName);
            }
        }

        private void GetPid(int projId, int panelId, string cpuName)
        {
            dtTarget.Rows.Clear();
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
                        DataRow row = dtTarget.NewRow();
                        row["id"] = int.Parse(reader["id"].ToString());
                        row["part_name"] = reader["part_name"].ToString();
                        row["page_name"] = reader["page_name"].ToString();
                        dtTarget.Rows.Add(row);
                    }

                    gridTarget.DataSource = dtTarget;
                }
            }
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            List<DataRow> list = new List<DataRow>();
            foreach (int id in gridView1.GetSelectedRows())
            {
                list.Add((gridView1.GetRow(id) as DataRowView).Row);
            }            

            foreach (var row in list)
            {               
                DataRow newRow = dtTarget.NewRow();
                newRow["part_name"] = row["part_name"];
                newRow["page_name"] = row["page_name"];
                dtTarget.Rows.Add(newRow);
                dtSource.Rows.Remove(row);
            }

            dtTarget.DefaultView.Sort = "part_name, page_name";
            gridTarget.DataSource = dtTarget;


        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            List<DataRow> list = new List<DataRow>();
            foreach (int id in gridView2.GetSelectedRows())
            {
                list.Add((gridView2.GetRow(id) as DataRowView).Row);
            }

            foreach (var row in list)
            {
                DataRow newRow = dtSource.NewRow();
                newRow["part_name"] = row["part_name"];
                newRow["page_name"] = row["page_name"];
                dtSource.Rows.Add(newRow);
                dtTarget.Rows.Remove(row);
            }

            dtSource.DefaultView.Sort = "part_name, page_name";
            gridSource.DataSource = dtSource;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dtTarget.Rows.Count < 0) return;

            string connectString = SIDS.Instance.MakeConnectionString("DB");
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();

                using(SqlCommand cmd = new SqlCommand("dbo.proc_pid_delete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@projId", SqlDbType.Int).Value = this._project.ProjID;
                    cmd.Parameters.Add("@cpuId", SqlDbType.NVarChar).Value = _cpuId;
                    cmd.Parameters.Add("@panelId", SqlDbType.Int).Value = this._panelId;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }


                using (SqlCommand cmd = new SqlCommand("dbo.proc_pid_insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (var item in dtTarget.Select())
                    {
                        cmd.Parameters.Add("@projId", SqlDbType.Int).Value = this._project.ProjID;
                        cmd.Parameters.Add("@panelId", SqlDbType.Int).Value = this._panelId;
                        cmd.Parameters.Add("@partName", SqlDbType.NVarChar).Value = item["part_name"].ToString();
                        cmd.Parameters.Add("@pageName", SqlDbType.NVarChar).Value = item["page_name"].ToString();
                        cmd.Parameters.Add("@cpuId", SqlDbType.Int).Value = _cpuId;
                        cmd.Parameters.Add("@cpuName", SqlDbType.NVarChar).Value = _cpuName;
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }

                GetPidPage(this._project.ProjID, _cpuName);
                GetPid(this._project.ProjID, this._panelId, _cpuName);
            }

        }

        private void CreatePartCheckBox(int projId)
        {
            string connectString = SIDS.Instance.MakeConnectionString("DB");
            using(SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.proc_part_checkbox", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@projId", SqlDbType.Int).Value = _project.ProjID;
                    SqlDataReader reader = cmd.ExecuteReader();

                    int x = lblCpuName.Location.X + lblCpuName.Width + 100;
                    int y = lblCpuName.Location.Y;
                    while (reader.Read())
                    {
                        string partName = reader["part_name"].ToString();
                        System.Windows.Forms.CheckBox checkBox = new System.Windows.Forms.CheckBox();
                        checkBox.Name = "chk_" + partName;
                        checkBox.Width = 200;
                        checkBox.Height = lblCpuName.Height;
                        checkBox.Text = partName;
                        checkBox.Checked = true;
                        checkBox.Location = new Point(x, y);
                        checkBox.CheckedChanged += PartCheckBoxChanged;
                       // checkBox.Enabled = false;
                        this.Controls.Add(checkBox);

                        partList.Add(partName);
                        x += 210;

                    }
                }
            }
        }

        private void PartCheckBoxChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            string partName = checkBox.Text;
            if(checkBox.Checked == true)
            {
                partList.Add(partName);
            }
            else
            {
                partList.Remove(partName);
            }

            GetPidPage(_project.ProjID, _cpuName);
        }

        private string MakePartNames(List<string> names)
        {
            if(names.Count < 1) return "";

            string partNames = null;          
           
            for(int i = 0; i< names.Count; i++)
            {
                if(i < (partList.Count - 1))
                {
                    partNames += names[i] + ",";
                }
                else
                {
                    partNames += names[i];
                }
            }


            return partNames;
        }

        private void cmbCpu_SelectedIndexChanged(object sender, EventArgs e)
        {          
            foreach (var control in this.Controls)
            {
               if(control.GetType() == typeof(CheckBox))
                {
                    CheckBox cb = control as CheckBox;
                    if (cb.Name.Contains("chk_"))
                    {
                        cb.Enabled = true;
                    }
                }
            }
            
        }
    }
}