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

namespace auto_proj.Form
{
    public partial class FormPLCConfigcs : DevExpress.XtraEditors.XtraForm
    {
        Project project = null;
        PopupSelectProj selectProj = null;
        int selectedCpuNumber = 0;

        public FormPLCConfigcs()
        {
            InitializeComponent();
        }

        private void FormPLCConfigcs_Load(object sender, EventArgs e)
        {
            //UserPanel panel = new UserPanel();
            //panel.Location = new Point(0, 0);
            //this.Controls.Add(panel);


        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            selectProj = new PopupSelectProj();
            selectProj.SelectedProj += SelectProj_SelectedProj;
            selectProj.ShowDialog();
            selectProj.SelectedProj -= SelectProj_SelectedProj;

            if (project == null) return;
            SelectCpu(project.ProjID);
        }

        private void SelectCpu(int projID)
        {
            string connectString = SIDS.Instance.MakeConnectionString("DB");
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand("proc_select_cpu", conn))
                {
                    cmd.Parameters.Add("@proj_id", SqlDbType.Int).Value = project.ProjID;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    cmbCpu.Properties.Items.Clear();
                    while(reader.Read())
                    {
                        cmbCpu.Properties.Items.Add(reader[0].ToString());
                    }
                }

                
            }
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

        private void cmbCpu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string connectString = SIDS.Instance.MakeConnectionString("DB");
            string cpuName = cmbCpu.Text.Trim();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("proc_select_cpu_number", conn))
                {
                    cmd.Parameters.Add("@proj_id", SqlDbType.Int).Value = project.ProjID;
                    cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = cpuName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        selectedCpuNumber = int.Parse(reader[0].ToString());
                    }
                    reader.Close();
                    cmd.Dispose();
                }

                using (SqlCommand cmd = new SqlCommand("proc_select_panel", conn))
                {
                    cmd.Parameters.Add("@proj_id", SqlDbType.Int).Value = project.ProjID;
                    cmd.Parameters.Add("@cpu", SqlDbType.NVarChar).Value = selectedCpuNumber;
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    cmbPanel.Properties.Items.Clear();
                    while (reader.Read())
                    {
                        cmbPanel.Properties.Items.Add(reader[0].ToString());
                    }
                }
            }           
        }

        private void btnCreatePanel_Click(object sender, EventArgs e)
        {
            if (cmbPanel.Text == "") return;

            UserPanel panel = new UserPanel(project, selectedCpuNumber, cmbPanel.Text.Trim()); ;
            panel.Location = new Point(10, 250);
            this.Controls.Add(panel);
        }
    }
}