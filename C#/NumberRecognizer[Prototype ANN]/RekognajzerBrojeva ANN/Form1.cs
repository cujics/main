using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace RekognajzerBrojeva_ANN
{
    public partial class Form1 : Form
    {
        double[,] fl = new double[2,100];
        double[] wl1 = new double[10000];
        double[] wl2 = new double[1000];
        double[] res = new double[10];
        public Form1()
        {
            InitializeComponent();
            FillRandomWLS();
        }
        private double sigmoid(double p)
        {
            return 1 / (1 + Math.Pow(Math.E, -p));
        }
        private void FillRandomWLS()
        {
            Random r = new Random();
            for (int i = 0; i < 10000; i++) wl1[i] =(r.Next(1, 999)/1000.0);
            for(int i=0;i<1000;i++) wl2[i]= (r.Next(1, 100)/1000.0);
        }
        private int NajveciIndeks()
        {
            double mm = res[0];
            int indeks = 0;
            for(int i = 1; i < 10; i++)
            {
                if (res[i] > mm)
                {
                    mm = res[i];
                    indeks = i;
                }
            }
            return indeks;
        }
        private double SrednjiBroj(int ab)
        {
            double g = 0;
            for (int i = 0; i < 10; i++) if(i!=ab)g += res[i];
            return g / 9;
        }
        private void reset()
        {
            for(int i=0;i<100;i++)
            {
                fl[0, i] = 0;fl[1, i] = 0;
            }
            for (int i = 0; i < 10; i++) res[i] = 0;
        }
        private double najveciFLs(int i)
        {
            double najv = fl[1, i];
            for(;i<100;i+=10)
            {
                if(fl[1,i]>najv)
                {
                    najv = fl[1, i];
                }
            }
            return najv;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int uspeh = 0;
            StreamWriter delta = new StreamWriter(@"c:\CUdata\delta.txt");
            for (int i=0;i<1000;i++)
            {
                for(int j=1;j<4;j++)
                {
                    reset();
                    string put = "c:\\CUdata\\bmp\\" + (i % 10).ToString() + "_" + j.ToString() + ".bmp";
                    Bitmap slika = new Bitmap(put);
                    for (int a = 0; a < 100; a++)
                    {
                        if (slika.GetPixel(a / 10, a % 10).R > 50)
                            fl[0, a] = 1;
                        else fl[0, a] = 0;
                    }
                    for (int a = 0; a < 100; a++)
                    {
                        for (int g = 0; g < 100; g++)
                            fl[1, g] = (fl[1,g]+(fl[0, a] * wl1[a * 100 + g]));
                    }
                    for (int a = 0; a < 100; a++)
                    {
                        for (int g = 0; g < 10; g++)
                            res[g] = (res[g]+fl[1, a] * wl2[a * 10 + g]);
                    }
                    for (int a = 0; a < 100; a++) fl[1, a] = sigmoid(fl[1, a]);
                    int najv = NajveciIndeks();
                    double najveciFL = najveciFLs(i%10);
                    //double sred = SrednjiBroj(i % 10);
                    for (int a = 0; a < 10; a++) res[a] = sigmoid(res[a]);
                    //double n = 0.3; //learning constant 
                    if(najv == i%10)
                    {
                        //listBox1.Items.Add(najv);
                        uspeh++; //tacno
                    }
                    else//trening
                    {
                        //for (int a = i%10; a < 1000; a += 10) wl2[a] = sigmoid(wl2[a] * (1 + res[i%10] / res[najv]));
                        //for (int a = 0; a < 100; a++) fl[1,a] = sigmoid(fl[1,a]);
                        for (int a = i; a < 1000; a+=10)
                        {
                            double deltaW = (1+res[i%10]/res[najv])*(fl[1,a/10]/najveciFL);

                             wl2[a] *= deltaW; delta.WriteLine(deltaW);
                             wl2[a] = (wl2[a]);
                                
                            
  
                        }
                        for (int a = 0; a < 1000; a++) wl2[a] = sigmoid(wl2[a]);
                        /*for(int a=i;a<10000;a++)
                        {
                            //wl1[a] = sigmoid(wl2[a]* (1-res[i%10]))
                        }*/
                    }
                    
                }
                
            }
            delta.Close();

            if (false)
            {
                reset();
                int TESTER_A = 0;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 1; j < 4; j++)
                    {
                        reset();
                        string put = "c:\\CUdata\\bmp\\" + (i % 10).ToString() + "_" + j.ToString() + ".bmp";
                        Bitmap slika = new Bitmap(put);
                        for (int a = 0; a < 100; a++)
                        {
                            if (slika.GetPixel(a / 10, a % 10).R > 50)
                                fl[0, a] = 1;
                            else fl[0, a] = 0;
                        }
                        for (int a = 0; a < 100; a++)
                        {
                            for (int g = 0; g < 100; g++)
                                fl[1, g] += (fl[0, a] * wl1[a * 100 + g]);
                        }
                        for (int a = 0; a < 100; a++)
                        {
                            for (int g = 0; g < 10; g++)
                                res[g] += (fl[1, a] * wl2[a * 10 + g]);
                        }
                        int najv = NajveciIndeks();
                        for (int a = 0; a < 10; a++) res[a] = sigmoid(res[a]);
                        //double sred = SrednjiBroj(i % 10);
                        if (najv == i % 10)
                        {
                            TESTER_A++; //tacno
                            listBox1.Items.Add(najv);
                        }
                    }
                }
                label2.Text = TESTER_A.ToString() + "/30";
            }
                    label1.Text = uspeh.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reset();
            string unos = textBox1.Text;
            string put= "c:\\CUdata\\bmp\\"+unos+".bmp";
            Bitmap slika = new Bitmap(put);
            pictureBox1.Image = slika;

            for (int a = 0; a < 100; a++)
            {
                if (slika.GetPixel(a / 10, a % 10).R > 50)
                    fl[0, a] = 1;
                else fl[0, a] = 0;
            }
            for (int a = 0; a < 100; a++)
            {
                for (int g = 0; g < 100; g++)
                    fl[1, g] += (fl[0, a] * wl1[a * 100 + g]);
            }
            for (int a = 0; a < 100; a++)
            {
                for (int g = 0; g < 10; g++)
                    res[g] += (fl[1, a] * wl2[a * 10 + g]);
            }
            int najv = NajveciIndeks();
            /*StreamWriter sw = new StreamWriter(@"c:\CUdata\res.txt");
            for (int i = 0; i < 100; i++) sw.WriteLine(wl2[i]);
            sw.Close();*/
            label1.Text = najv.ToString();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(@"c:\CUdata\neurons.txt");
            for (int i = 0; i < 100; i++) sw.WriteLine(fl[0,i]+" "+fl[1,i]);
            sw.Close();
            StreamWriter sw1 = new StreamWriter(@"c:\CUdata\w1.txt");
            for (int i = 0; i < 10000; i++) sw1.WriteLine(wl1[i]);
            sw1.Close();
            StreamWriter sw2 = new StreamWriter(@"c:\CUdata\w2.txt");
            for (int i = 0; i < 1000; i++) sw2.WriteLine(wl2[i]);
            sw2.Close();
            StreamWriter sw3 = new StreamWriter(@"c:\CUdata\res.txt");
            for (int i = 0; i < 10; i++) sw3.WriteLine(res[i]);
            sw3.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 tes = new Form2(this);
            //tes.Activate();
            //tes.
        }
        int[] neuroni= new int[100];
        double[] weis= new double[100 * 10];
        double[] reš = new double[10];

        private void reset2()
        {
            for (int i = 0; i < 10; i++) reš[i] = 0;
        }

        private void rfill2()
        {
            Random r = new Random();
            for (int i = 0; i < 1000; i++) weis[i] = (r.Next(1, 500) / 1000.0);
        }
        private int nadjiNuReš_index()
        {
            int inn= 0;
            double mm = reš[inn];
            for(int i=1;i<10;i++)
            {
                if(reš[i]>mm)
                {
                    mm = reš[i];
                    inn = i;
                }
            }
            return inn;
        }
        private int act_count(int x)
        {
            int s = 0;
            for (int p = x; p < 100; p += 10) if (neuroni[p] == 1) s++;return s;
        }
        double K = 0.4;
        private double avg_zbir_weis(int x)
        {
            double sum = 0;
            for (int i = x; i < 1000; i += 10) sum += weis[i];
            return sum / 100;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            int testovi = 1000;
            int uspeh2 = 0;
            int tr_poz = 0;
            StreamWriter test_S = new StreamWriter(@"c:\CUdata\test_S.txt");
            for (int i=0;i<testovi;i++)
            {
                for(int j=1;j<4;j++)
                {
                    reset2();
                    string put= "c:\\CUdata\\bmp\\" + (i % 10).ToString() + "_" + j.ToString() + ".bmp";
                    Bitmap slika = new Bitmap(put);
                    for (int a = 0; a < 100; a++) if (slika.GetPixel(a / 10, a % 10).R > 50) neuroni[a] = 1;
                    for(int a=0;a<100;a++)
                    {
                        for (int g = 0; g < 10; g++) reš[g] += (neuroni[a] * weis[a * 10 + g]);
                    }
                    
                    int najv = nadjiNuReš_index();
                    //for (int y = 0; y < 10; y++) reš[y] = sigmoid(reš[y]);
                    int s = act_count(i % 10);
                    
                    if (i % 10 == najv) uspeh2++;
 
                    else
                    {
                        //if (s > 0 && reš[najv] > 0)
                        for (int x = 0; x < 1000; x++)
                        {
                            if (x % 10 == i % 10 && neuroni[x / 10] == 1)
                            { weis[x] += K * (res[i % 10] - res[najv]); }
                        }
                                //weis[x] = weis[x] * (1 + neuroni[x / 10] * reš[i % 10] / res[najv]) / (s*10);
                        listBox1.Items.Add(reš[i % 10]+" "+K);
                        test_S.WriteLine(s+" "+reš[najv]);
                    }
                }
            }
            test_S.Close();
            label5.Text = "Trening se pozvao:" + tr_poz.ToString() + " puta.";
            label3.Text = uspeh2.ToString() + "/" + (testovi*3).ToString();
            //TEST ZONE
            int TESZ = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    reset2();
                    string put = "c:\\CUdata\\bmp\\" + (i % 10).ToString() + "_" + j.ToString() + ".bmp";
                    Bitmap slika = new Bitmap(put);
                    for (int a = 0; a < 100; a++) if (slika.GetPixel(a / 10, a % 10).R > 50) neuroni[a] = 1;
                    for (int a = 0; a < 100; a++)
                    {
                        for (int g = 0; g < 10; g++) reš[g] += neuroni[a] * weis[a * 10 + g];
                    }
                    int najv = nadjiNuReš_index();
                    int s = act_count(i % 10);
                    if (i % 10 == najv) TESZ++;
                }
            }
            label4.Text = TESZ.ToString() + "/30";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            StreamWriter sw1 = new StreamWriter(@"c:\CUdata\WEIS.txt");
            for (int i = 0; i < 1000; i++) sw1.WriteLine(weis[i]);
            sw1.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            rfill2();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            K = Convert.ToDouble(textBox2.Text);
        }
    }
}
