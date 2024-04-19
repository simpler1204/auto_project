using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using auto_proj.Enum;

namespace auto_proj.UserControls
{
    public partial class DiCard : Card
    {
        public DiCard()
        {
            InitializeComponent();
        }

         public DiCard(IO_TYPE type, int projId, int cpuId, int panelId, int rack, int rail, int slot, int channel)
            :base( type,  projId,  cpuId,  panelId,  rack,  rail,  slot, channel)
        {
	
        }        

    }
}
