using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sinisakalendar
{
    public partial class Form1 : Form
    {
        int godina,mesec,dan;
        string[] mes = { "greska", "januar", "februar", "mart", "april", "maj", "jun", "jul", "avgust", "septembar", "oktrobar", "novembar", "decembar" };
        public Form1()
        {
            InitializeComponent();
            dataGridView1.RowCount = 7;
            dataGridView1.ColumnCount = 7;
            for (int i = 0; i < 7; i++)
                dataGridView1.Rows[i].Height = dataGridView1.Height / 7;
            for (int i = 0; i < 7; i++)
                dataGridView1.Columns[i].Width = dataGridView1.Width/7;
            string[] dani = { "ponedeljak","utorak","sreda","četvrtak","petak","subota","nedelja"};
            for (int i = 0; i < 7; i++)
                dataGridView1[i, 0].Value = dani[i];
            godina = 1900;
            mesec = 1;
            dan = 0;
            popuni(godina, mesec,dan);
        }
        private void popuni(int godina,int mesec,int dan)
        {
            resetcolor();
            label1.Text = godina.ToString();
            label2.Text = mes[mesec];
            int p = 0;
            int b = daniumesecu(godina, mesec);
            int d = prvidan(godina, mesec);
            int j = 1;
            int k = 1;
            while(p<b)
            {
                dataGridView1[(d + p)%7,j].Value = p + 1;
                if (p + 1 == dan)
                    dataGridView1[(d + p) % 7, j].Style.BackColor = Color.Purple;
                p++;
                
                if ((p+d)==7*k)
                {
                    dataGridView1[6, j].Style.BackColor = Color.Red;
                    if (p == dan)
                        dataGridView1[(d + p - 1) % 7, j].Style.BackColor = Color.Purple;
                    k++;
                    j++;
                }    
            }
            int en = 1;
            while((d+p)<42)
            {
                dataGridView1[(d+p)%7, j].Value = en;
                dataGridView1[(d + p) % 7, j].Style.BackColor = Color.Gray;   
                en++;
                p++;

                if ((p + d) == 7 * k)
                {
                    dataGridView1[6, j].Style.BackColor = Color.Red;
                    k++;
                    j++;
                }
                
                    


            }
            if (mesec == 1 && godina != 1900)
                en = daniumesecu(godina-1, 12);
            else if(godina == 1900 && mesec==1)
                en = 0;
            else
                en = daniumesecu(godina, mesec - 1);
            if(en!=0)
            {
                int gn = 0;
                while(gn<d)
                {
                    dataGridView1[gn, 1].Value = en-d+1;
                    dataGridView1[gn, 1].Style.BackColor = Color.Gray;
                    en++;
                    gn++;
                }
            }
        }
        private int prvidan(int godina,int mesec)//0-ponedeljak 1-januar
        {
            int dan=0;
            dan += ((godina - 1900)/4)*2+((godina-1900)- (godina - 1900) / 4);
            if (godina % 4 == 0 && godina % 100 != 0 && godina > 1903)
                dan --;
            dan %= 7;
            mesec--;
            if (mesec == 0)
                return dan;
            if (mesec <= 6)
            {
                dan += (mesec / 2) * 2 + (mesec - mesec / 2) * 3;
                if (mesec >= 2)
                    dan -= 2;
                if (godina % 4 == 0 && godina%100!=0 && mesec>=2)
                    dan++;
                dan %= 7;
                return dan;
            }
            else if(mesec>8)
            {
                dan += ((mesec - 8) / 2) * 3 + ((mesec - 8) - (mesec - 8) / 2) * 2+5;
                if (godina % 4 == 0)
                    dan++;
                dan %= 7;
                return dan;
            }
            else if(mesec == 7)
            {
                if (godina % 4 == 0 && godina % 100 != 0)
                    dan++;
                dan += 2;
                dan %= 7;
                return dan;
            }
            if (godina % 4 == 0 && godina % 100 != 0)
                dan++;
            dan += 12;
            dan%=7;
            return dan;
                
            
        }
        private int daniumesecu(int godina,int mesec)
        {
            if (mesec == 2)
            {
                if (godina % 4 == 0 && godina % 100 != 0)
                    return 29;
                return 28;
            }
            if(mesec<=6)
            {
                if (mesec % 2 == 0)
                    return 30;
                return 31;
            }
            if(mesec>8)
            {
                if (mesec % 2 == 0)
                    return 31;
                return 30;
            }
            return 31;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mesec == 1)
            {
                godina--;
                mesec = 12;
            }
            else
                mesec--;
            popuni(godina, mesec,0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (mesec == 12)
            {
                godina++;
                mesec = 1;
            }
            else
                mesec++;
            popuni(godina, mesec,0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                godina = Convert.ToInt32(textBox1.Text);
            }
            catch { }
            
            try
            {
                mesec = Convert.ToInt32(textBox2.Text);
            }
            catch { }
            try
            {
                dan = Convert.ToInt32(textBox3.Text);
            }
            catch
            { }
            popuni(godina, mesec,dan);

        }

        private void resetcolor()
        {
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                    dataGridView1[i, j].Style.BackColor = Color.White;
        }
    }
}
