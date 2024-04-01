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
    public partial class AoCard : Card
    {
        public AoCard()
        {
            InitializeComponent();
        }

         public AoCard(string cardName, int channel) :base(cardName, channel)
        {
        
        }

         public AoCard(int cardId, string cardName, int projId, int panelId, int plcId, int rackId, int railId, int slotId)
            :base(cardId, cardName, projId, panelId, plcId, rackId, railId, slotId)
        {
	
        }

    }
}
