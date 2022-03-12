using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace RekognajzerBrojeva_ANN
{
    public partial class TESTER : Form
    {
        double[] w = new double[110 * 100];
        double[,] n = new double[2, 100];
        string path;
        int fitnes;
        int i ,j;
        private double[] res = new double[10];
        public TESTER()
        {
            i = 0;j = 1;
            fitnes = 0;
            InitializeComponent();
            load();
        }
        private void Nulovanje()
        {
            for (int i = 0; i < 100; i++) { n[1, i] = 0; }
            for (int i = 0; i < 10; i++) res[i] = 0;
        }
        private int Najveci()
        {
            int inda = 0;
            double mmax = res[inda];
            for (int i = 1; i < 10; i++)
            {
                if (res[i] > mmax)
                {
                    mmax = res[i];
                    inda = i;
                }
            }
            return inda;
        }
        private double sigmoid(double p)
        {
            return 1 / (1 + Math.Pow(Math.E, -p));
        }
        private void test()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    
                }
            }
            label1.Text = fitnes.ToString();
            string g = "";
            for (int i = 0; i < 10; i++) g += res[i].ToString() + " ";label1.Text = g;
        }
        private void load()
        {
            StreamReader sr = new StreamReader(@"c:\CUdata\(A)dataset.txt");
            for (int i = 0; i < 110 * 100; i++) w[i] = Convert.ToDouble(sr.ReadLine());
            sr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (j > 3) { j = 1; i++; }
            if (i > 9) i = 0;            
            Nulovanje();
            path = "c:\\CUdata\\bmp\\" + i.ToString() + "_" + j.ToString() + ".bmp";
            Bitmap slika = new Bitmap(path);
            pictureBox1.Image = slika;
            for (int a = 0; a < 100; a++) { if (slika.GetPixel(a / 10, a % 10).R > 50) n[0, a] = 1; else n[0, a] = 0; }
            for (int a = 0; a < 100 * 100; a++) n[1, a % 100] += n[0, a / 100] * w[a];
            for (int a = 0; a < 100; a++) n[1, a] = sigmoid(n[1, a]);
            for (int a = 0; a < 10 * 100; a++) res[a % 10] += n[1, a / 10] * w[100 * 100 + a];
            if (i == Najveci()) fitnes++;
            label1.Text = Najveci().ToString();
            j++;
        }
    }
}
