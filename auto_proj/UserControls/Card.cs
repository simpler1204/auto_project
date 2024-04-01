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
        protected int _cardId;
        protected string _cardName;
        protected int _projId;
        protected int _panelId;
        protected int _plcId;        
        protected int _rackId;
        protected int _railId;
        protected int _slotId;


        public Card()
        {
            InitializeComponent();
        }

        public Card(string cardName, int channel)
        {
            InitializeComponent();
            lblCardName.Text = cardName;
            lblChannel.Text = channel.ToString();
        }


        public Card(int cardId, string cardName, int projId, int panelId, int plcId, int rackId, int railId, int slotId)
        {
            InitializeComponent();

            _cardId = cardId;
            _cardName = cardName;
            _projId = projId;
            _panelId = panelId;
            _plcId = plcId;            
            _rackId = rackId;
            _railId = railId;
            _slotId = slotId;
        }

        public int CardId { get => this._cardId; }
        public string CardName { get => this._cardName; }
        public int ProjId { get => this._projId; }
        public int PanelId { get => this._panelId; }
        public int PlcId { get => this.PlcId; }        
        public int RackId { get => this._rackId; }
        public int RailId { get => this._railId; }
        public int Slot { get => this._slotId; }
    }
}
