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
        Project project;
        DataTable dtPid = null;
        DataTable selectedPid = null;
        public PopupPIDMapping1(Project project)
        {
            InitializeComponent();
            this.project = project;
            this.Load += PopupPIDMapping1_Load;
        }

        private void PopupPIDMapping1_Load(object sender, EventArgs e)
        {
            if(project != null)
                GetPidPart(project.ProjID);
        }

        private void GetPidPart(int projID)
        {
            string connectString = SIDS.Instance.MakeConnectionString("DB");
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();

            }
        }

        private void GetPidPage(int projId, string partName)
        {
            string connectString = SIDS.Instance.MakeConnectionString("DB");
            using (SqlConnection conn = new SqlConnection(connectString))
            {

            }
        }
    }
}