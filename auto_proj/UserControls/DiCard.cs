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
    public partial class DiCard : Card
    {
        public DiCard()
        {
            InitializeComponent();
        }

         public DiCard(string cardName, int channel) :base(cardName, channel)
        {
        
        }

        public DiCard(int cardId, string cardName, int projId, int panelId, int plcId, int rackId, int railId, int slotId)
            :base(cardId, cardName, projId, panelId, plcId, rackId, railId, slotId)
        {

        }
    }
}
