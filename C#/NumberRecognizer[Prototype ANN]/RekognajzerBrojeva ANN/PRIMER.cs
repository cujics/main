using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
namespace RekognajzerBrojeva_ANN
{
    class PRIMER
    {
        public int fitnes;
        public int index;
        private string path;
        private string datapath;
        public double[] w = new double[100 * 100 + 10 * 100];
        double[,] n = new double[2, 100];
        private double[] res = new double[10];
        Menadzment m;
        public PRIMER(int bp,Menadzment m1)
        {
            index = bp;
            fitnes = 0;
            Rand_Incijacija();
            m = m1;
            //datapath = "c:\\CUdata\\datapath.txt";
        }
        public void Rand_Incijacija()
        {
            Random r = new Random();
            for (int i = 0; i < 110 * 100; i++) w[i] = r.Next(1, 499) / 1000.0;
        }
        public void Real_Incijacija()//trenutno nepotrebna
        {
            StreamReader sr = new StreamReader(datapath);
            for (int i = index * 110 * 100; i < (index + 1) * 110 * 100; i++) w[i] = Convert.ToDouble(sr.ReadLine());
            sr.Close();
        }
        private void Nulovanje()
        {
            for (int i = 0; i < 100; i++) { n[1, i] = 0; }
            for (int i = 0; i < 10; i++) res[i] = 0;
        }
        public void Poziv()
        {
            fitnes = 0;
            Radnja();
        }
        private int Najveci()
        {
            int inda = 0;
            double mmax = res[inda];
            for(int i=1;i<10;i++)
            {
                if(res[i]>mmax)
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
        private void Radnja()
        {
            for(int i=0;i<10;i++)
            {
                for(int j=1;j<4;j++)
                {
                    Nulovanje();
                    path = "c:\\CUdata\\bmp\\" + i.ToString() + "_" + j.ToString() + ".bmp";
                    Bitmap slika = new Bitmap(path);
                    for(int a = 0; a < 100; a++) { if (slika.GetPixel(a / 10, a % 10).R > 50) n[0, a] = 1; else n[0, a] = 0; }
                    for (int a = 0; a < 100 * 100; a++) n[1, a % 100] += n[0, a / 100] * w[a];
                    for (int a = 0; a < 100; a++) n[1, a] = sigmoid(n[1, a]);
                    for (int a = 0; a < 10 * 100; a++) res[a %10] += n[1, a / 10] * w[100 * 100 + a];
                    if (i == Najveci()) fitnes++;
                }
            }
            if(fitnes>25)
            {
                StreamWriter sw = new StreamWriter("c:\\CUdata\\(B)dataset.txt");
                for (int g = 0; g < 110 * 100; g++) sw.WriteLine(w[g]);
                sw.Close();
                m.NASTAVI = false;
            }
        }
        public void Mutiraj()
        {
            Random r = new Random();
            Random r1 = new Random();
            int znak = 1;
            for (int i = 0; i < 110 * 100; i++) { if (r1.Next(1, 100) > 60) znak *= -1; w[i] += w[i]*(1 + (r.Next(1, 500) / 1000.0) * znak); }
        }
    }
}
