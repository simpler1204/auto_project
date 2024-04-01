using auto_proj.Classes;
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

    public partial class Rail : UserControl, IEnumerable<Card>
    {
        Project _project = null;

        int  xLocation = 0;
        int _rackId;
        int _railId;
       
        List<Card> cards = new List<Card>();
        public Rail()
        {
            InitializeComponent();
        }

        public Rail(int rail)
        {
            InitializeComponent();
            this._railId = rail;
            lblRail.Text = rail.ToString();
        }

        public int RackId { 
            get => this._rackId; 
            set
            {
                this._rackId = value;
            }
        }
        public int RailId { get => this._railId; }
               

        public IEnumerator<Card> GetEnumerator()
        {
            return (IEnumerator<Card>)GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new CardList(cards);
        }

        public class CardList : IEnumerator
        {
            public List<Card> _cards = new List<Card>();
            int currentIndex = -1;

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public Card Current
            {
                get
                {
                    try
                    {
                        return _cards[currentIndex];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            public CardList(List<Card> cards)
            {
                this._cards = cards;
            }

            public bool MoveNext()
            {
                currentIndex++;
                return (currentIndex < _cards.Count);
            }

            public void Reset()
            {
                currentIndex = -1;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            AiCard card = new AiCard("AI", 16);
            card.Location = new Point(xLocation, 0);
            groupBox1.Controls.Add(card);

            xLocation += 70;
        }

        private void btnAo_Click(object sender, EventArgs e)
        {
            AoCard card = new AoCard("AO", 16);
            card.Location = new Point(xLocation, 0);
            groupBox1.Controls.Add(card);

            xLocation += 70;
        }

        private void btnDi_Click(object sender, EventArgs e)
        {
            DiCard card = new DiCard("DI", 32);
            card.Location = new Point(xLocation, 0);
            groupBox1.Controls.Add(card);

            xLocation += 70;
        }

        private void btnDo_Click(object sender, EventArgs e)
        {

            DoCard card = new DoCard("DO", 32);
            card.Location = new Point(xLocation, 0);
            groupBox1.Controls.Add(card);

            xLocation += 70;
        }
    }
}
