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
    public partial class DoCard : Card
    {
        public DoCard()
        {
            InitializeComponent();
        }

        public DoCard(string cardName, int channel) :base(cardName, channel)
        {
        
        }

        public DoCard(int cardId, string cardName, int projId, int panelId, int plcId, int rackId, int railId, int slotId)
            : base(cardId, cardName, projId, panelId, plcId, rackId, railId, slotId)
        {

        }
    }
}
