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

namespace auto_proj.UserControls
{
    public partial class UserPanel : UserControl
    {
        private int _yLocation = 30;

        Project _project = null;
        Rail[] _rails = new Rail[2];

        string _panelName;
        int _cpu;
        int _railCount = 0;


        public UserPanel(Project project, int cpu, string panelName)
        {
            InitializeComponent();

            this._project = project;
            this._cpu = cpu;
            this._panelName = panelName;

           // InitRails();
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

        private void InitRails()
        {
            for(int i = 0; i<_rails.Length; i++)
            {
                _rails[i] = new Rail();
                _rails[i].Location = new Point(10, _yLocation); 
                this.groupControl1.Controls.Add(_rails[i]);
                //if(i == 0) 
                    _yLocation += 270;
            }
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (RailCount > 1) return;

            _rails[RailCount] = new Rail(RailCount + 1);
            _rails[RailCount].Location = new Point(10, _yLocation);
            this.groupControl1.Controls.Add(_rails[RailCount]);           
            RailCount++;           
            _yLocation += 300;
        }
    }
}
