using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MappingSW
{
    public partial class Map : Form
    {
        public Map()
        {
            InitializeComponent();


        }

        private void updateMap_Click(object sender, EventArgs e)
        {
            foreach (Control item in this.Controls.OfType<mButton>())
            {
                this.Controls.Remove(item);
            }

            int r = Convert.ToInt32(yText.Text), c = Convert.ToInt32(xText.Text);
            
            mButton[,] buttons = new mButton[r,c];
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    buttons[i, j] = new mButton();
                    buttons[i, j].Location = new Point(j * 16 + 32, i * 16 + 32);
                    this.Controls.Add(buttons[i, j]);
                }
            }
        }
    }
}
