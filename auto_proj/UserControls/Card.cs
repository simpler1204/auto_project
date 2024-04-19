using auto_proj.Enum;
using System;
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
    public partial class Card : UserControl
    {
        protected IO_TYPE _type;
        protected int _cardId = -1;
        protected string _cardName = null;
        protected int _projId = -1;
        protected int _cpuId = -1;
        protected int _panelId = -1;
        protected int _rack = -1;
        protected int _rail = -1;
        protected int _slot = -1;
        protected int _channel = -1;


        public Card()
        {
            InitializeComponent();
        }



        public Card(IO_TYPE type, int projId, int cpuId, int panelId, int rack, int rail, int slot, int channel)
        {
            InitializeComponent();

            _type = type;
            if (type == IO_TYPE.AI) lblCardName.Text = "AI";
            if (type == IO_TYPE.AO) lblCardName.Text = "AO";
            if (type == IO_TYPE.DI) lblCardName.Text = "DI";
            if (type == IO_TYPE.DO) lblCardName.Text = "DO";

            _channel = channel;
            lblChannel.Text = channel.ToString();

            _projId = projId;
            _cpuId = cpuId;
            _panelId = panelId;
            _rack = rack;
            _rail = rail;
            _slot = slot;

            lblSlot.Text = slot.ToString();
        }

        public int CardId { get => this._cardId; }
        public string CardName { get => this._cardName; }
        public int ProjId { get => this._projId; }
        public int PanelId { get => this._panelId; }
        public int PlcId { get => this.PlcId; }
        public int Rack { get => this._rack; set => this._rack = value; }
        public int Rail { get => this._rail; set => this._rail = value; }
        public int Slot { get => this._slot; }
        public IO_TYPE Type { get => this._type; }
        public int CpuId { get => this._cpuId; }
        public int Channel { get => this._channel; }

    }
}
