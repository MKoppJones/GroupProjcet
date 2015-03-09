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
    public partial class mButton : Button
    {
        public byte state = 0;
        public mButton() : base()
        {
            InitializeComponent();
            Width = 16;
            Height = 16;
            Margin = new Padding(0);
        }



        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (state == 2) state = 0;
            else state++;

            switch (state)
            {
                case 0:
                    BackColor = System.Drawing.Color.White;
                    break;
                case 1:
                    BackColor = System.Drawing.Color.Black;
                    break;
                case 2:
                    BackColor = System.Drawing.Color.Red;
                    break;
                default:
                    break;
            }
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
