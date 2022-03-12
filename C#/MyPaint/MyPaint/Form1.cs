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
    public partial class Form1 : Form
    {
        public int x, y, i, r;
        public Color clr;
        bool ukljuci;
//        bool poceo;
        public void Transmiter(object sender, EventArgs e)
        {
            if (ukljuci)
            {
                timer1.Start();
                ukljuci = !ukljuci;
            }
            else
            {
                timer1.Stop();
                ukljuci = !ukljuci;
            }
                
        }
        private void button2_Click(object sender, EventArgs e)
        {
            clr = button2.BackColor;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clr = button3.BackColor;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clr = button4.BackColor;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clr = button5.BackColor;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            clr = button7.BackColor;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            clr = button6.BackColor;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            clr = button9.BackColor;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            clr = button8.BackColor;
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            r = hScrollBar1.Value;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            foreach (Control ci in Controls)
            {
                if (ci.Name[0] == 'B')
                    ci.BackColor = Color.Transparent;
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //timer1.Start();
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //poceo = true;
            foreach (Control cc in Controls)
                if (cc.Name[0] == 'B' && (Cursor.Position.X- Form1.ActiveForm.Location.X < cc.Location.X + r && Cursor.Position.X - Form1.ActiveForm.Location.X> cc.Location.X && Cursor.Position.Y-20 - Form1.ActiveForm.Location.Y< cc.Location.Y+r && Cursor.Position.Y- Form1.ActiveForm.Location.Y-20 > cc.Location.Y))
                    cc.BackColor = clr;
            //poceo = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            clr = button12.BackColor;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            ukljuci = !ukljuci;
            colorDialog1.ShowDialog();
            button10.BackColor = colorDialog1.Color;
            clr= colorDialog1.Color;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for(int g=0;g<Controls.Count;g++)
            {
                if(Controls[g].Name[0]=='B')
                {
                    for (int l = 0; l < i; l++)
                        Controls[g].Dispose();
                    break;
                }
            }
            foreach (Control cc in Controls)
                if (cc.Name[0] == 'B')
                    cc.Dispose();

                x = 0;
                y = 0;
                i = 0;
                while (y < Form1.ActiveForm.Size.Height - 200)
                {
                    while (x < Form1.ActiveForm.Size.Width)
                    {
                        Dugme a = new Dugme(this);
                        x += r;
                        i++;
                    }
                    x = 0;
                    y += r;
                }
            /*
            listBox1.Items.Clear();
            foreach (Control cc in Controls)
                listBox1.Items.Add(cc.Name);*/
            //Controls[13].Dispose();
        }

        public Form1()
        {
            InitializeComponent();
            x = 0;
            y = 0;
            i = 0;
            r = 100;
            clr = Color.Blue;
            r = hScrollBar1.Value;
            //Dugme a = new Dugme(this);
            ukljuci = false;
            //poceo = false; 
        }

    }
}
