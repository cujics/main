using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaint
{
    class Dugme
    {
        Button d = new Button();
        Form1 eng;
        public Dugme(Form1 f)
        {
            eng = f;
            d.Name = "B" + f.i.ToString();
            d.Size = new Size(f.r, f.r);
            d.Location = new Point(f.x, f.y);
            d.Click += Aska;
            //d.MouseEnter += Mika;
            f.Controls.Add(d);
        }
        private void Aska(object sender, EventArgs e)
        {
            eng.Transmiter(sender,e);
        }
        private void Mika(object sender, EventArgs e)
        {
            d.BackColor = System.Drawing.Color.Red;
            d.Text = d.Name;
        }
    }
}
