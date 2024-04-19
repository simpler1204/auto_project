using auto_proj.Classes;
using auto_proj.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace auto_proj.UserControls
{  

    public partial class Rail : UserControl
    {
        Project _project = null;

        int _cpuId = -1;
        int _panelId = -1;
        int _rackId = -1;
        int _railId = -1;
        int _slot = 3;
       // bool delChecked = false;
        int xLocation = 0;

        List<Card> cardList = new List<Card>();
        public Rail()
        {
            InitializeComponent();
            this.Load += Rail_Load;
        }

        private void Rail_Load(object sender, EventArgs e)
        {
           
        }

        public Rail(Project project, int cpuId, int panelId, int rail)
        {
            InitializeComponent();
            this._project = project;
            this._cpuId = cpuId;
            this._panelId = panelId;
            this._railId = rail;
            lblRail.Text = rail.ToString();

            cmbAi.Text = project.AiChannel.ToString();
            cmbAo.Text = project.AoChannel.ToString();
            cmbDi.Text = project.DiChannel.ToString();
            cmbDo.Text = project.DoChannel.ToString();
        }

        public Project Project
        {
            get => this._project;
        }

        public int CpuId
        {
            get => this._cpuId;
        }

        public int PanelId
        {
            get => this._panelId;
        }

        public int RackId { 
            get => this._rackId; 
            set
            {
                this._rackId = value;
                cmbRack.Text = value.ToString();
            }
        }
        public int RailId { get => this._railId; }
        
        public int Slot { get => this._slot; set => this._slot = value; }


       

        public void AddCard(Card card)
        {
            this.cardList.Add(card);
        }

        public void Clear()
        {
            for (int i = 0; i < cardList.Count; i++)
            {
                cardList[i] = null;
            }
            this._slot = 3;
            xLocation = 0;
            cardList.Clear();
            DrawingCard();
        }
     
               

        public IEnumerator<Card> GetEnumerator()
        {
            //return (IEnumerator<Card>)GetEnumerator();

            return cardList.GetEnumerator();

        }

      

       

        private void btnAi_Click(object sender, EventArgs e)
        {
            if (!CheckComboBox(cmbAi)) return;

            DevExpress.XtraEditors.SimpleButton btn = sender as DevExpress.XtraEditors.SimpleButton;
            InsertCard(btn.Name);
        }

        private void btnAo_Click(object sender, EventArgs e)
        {
            if (!CheckComboBox(cmbAo)) return;
            DevExpress.XtraEditors.SimpleButton btn = sender as DevExpress.XtraEditors.SimpleButton;
            InsertCard(btn.Name);
        }

        private void btnDi_Click(object sender, EventArgs e)
        {
            if (!CheckComboBox(cmbDi)) return;
            DevExpress.XtraEditors.SimpleButton btn = sender as DevExpress.XtraEditors.SimpleButton;
            InsertCard(btn.Name);
        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            if (!CheckComboBox(cmbDo)) return;
            DevExpress.XtraEditors.SimpleButton btn = sender as DevExpress.XtraEditors.SimpleButton;
            InsertCard(btn.Name);
        }


        private void InsertCard(string btnName)
        {
            if (!CheckComboBox(cmbRack)) return;

            int channel = -1;           
            int.TryParse(cmbAi.Text, out channel);         
            Card card = null;
            switch (btnName)
            {
                case "btnAi":
                    int.TryParse(cmbAi.Text, out channel);
                    if (channel == 0) break;
                    card = new AiCard(IO_TYPE.AI, _project.ProjID, _cpuId, _panelId, _rackId, _railId, ++_slot, channel);
                    cardList.Add(card);
                    card.Location = new Point(xLocation, 0);
                    groupBox1.Controls.Add(card);
                    xLocation += card.Width;
                    // DrawingCard();
                    break;
                case "btnAo":
                    int.TryParse(cmbAo.Text, out channel);
                    if (channel == 0) break;
                    card = new AoCard(IO_TYPE.AO, _project.ProjID, _cpuId, _panelId, _rackId, _railId, ++_slot, channel);
                    cardList.Add(card);
                    card.Location = new Point(xLocation, 0);
                    groupBox1.Controls.Add(card);
                    xLocation += card.Width;
                    // DrawingCard();
                    break;
                case "btnDi":
                    int.TryParse(cmbDi.Text, out channel);
                    if (channel == 0) break;
                    card = new DiCard(IO_TYPE.DI, _project.ProjID, _cpuId, _panelId, _rackId, _railId, ++_slot, channel);
                    cardList.Add(card);
                    card.Location = new Point(xLocation, 0);
                    groupBox1.Controls.Add(card);
                    xLocation += card.Width;
                    // DrawingCard();
                    break;
                case "btnDo":
                    int.TryParse(cmbDo.Text, out channel);
                    if (channel == 0) break;
                    card = new DoCard(IO_TYPE.DO, _project.ProjID, _cpuId, _panelId, _rackId, _railId, ++_slot, channel);
                    cardList.Add(card);
                    card.Location = new Point(xLocation, 0);
                    groupBox1.Controls.Add(card);
                    xLocation += card.Width;
                    // DrawingCard();
                    break;
            }
            
        }

        public void DrawingCard()
        {
            groupBox1.Controls.Clear();            
            foreach (Card card in cardList)
            {
                card.Location = new Point(xLocation, 0);
                groupBox1.Controls.Add(card);
                xLocation += card.Width;
            }
        }

        private bool CheckComboBox(ComboBox cmb)
        {
            if (cmb.Text == "")
            { 
                if(cmb.Name == "cmbRack")
                {
                    MessageBox.Show("Rack 번호를 선택하여 주세요");
                }
                else
                {
                    MessageBox.Show("해당 모듈의 채널을 선택하여 주세요");
                }
                return false; 
            }

            return true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void cmbRack_SelectedValueChanged(object sender, EventArgs e)
        {
            this.RackId = int.Parse(cmbRack.Text);
            foreach (var card in cardList)
            {
                card.Rack = RackId;
            }
        }
    }

}
