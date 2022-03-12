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
    public partial class Menadzment : Form
    {
        double[] najbolji_w = new double[110 * 100];
        int BROJPONVALJANJA;
        public bool NASTAVI;
        public Menadzment()
        {
            NASTAVI = true;
            InitializeComponent();
            BROJPONVALJANJA = 100;
            Glavna();
        }
        private void Glavna()
        {
            List<PRIMER> kontejner = new List<PRIMER>();
            for (int i = 0; i < 100; i++)
            {
                PRIMER r = new PRIMER(i,this);
                kontejner.Add(r);
            }
            int[] fitnesbox = new int[100];
            for (int ponv = 0; ponv < BROJPONVALJANJA; ponv++)
            {
                if (!NASTAVI) break;
                foreach (PRIMER i in kontejner) i.Poziv();
                foreach (PRIMER i in kontejner)
                {
                    fitnesbox[i.index] = i.fitnes * 100 + i.index;
                }
                Array.Sort(fitnesbox);
                PRIMER[] kon = kontejner.ToArray();
                kontejner.Clear();
                for (int i = 0; i < 100; i++)
                {
                    bool primam = true;
                    for (int j = 0; j < 50; j++)
                    {

                        if (kon[i].index == fitnesbox[j] % 100)
                        {
                            primam = false;
                            break;
                        }
                    }
                    if (primam)
                    {
                        kontejner.Add(kon[i]);
                    }
                }
                for (int i = 0; i < 50; i++)
                {
                    kontejner.Add(kontejner[i]);
                }
                if (ponv != BROJPONVALJANJA - 1)
                { for (int i = 0; i < 100; i++) { kontejner[i].index = i; kontejner[i].fitnes = 0; kontejner[i].Mutiraj(); } }//ovde sam odlucio da resetujem fitnes
                else { for (int i = 0; i < 100; i++) kontejner[i].index = i; }
            }
            //nadji najboljeg
            if (NASTAVI)
            {
                foreach (PRIMER i in kontejner)
                {
                    fitnesbox[i.index] = i.fitnes * 100 + i.index;
                }
                Array.Sort(fitnesbox);
                int najbolji_index = fitnesbox[99] % 100;
                PRIMER najbolji = null;
                foreach (PRIMER d in kontejner)
                {
                    if (d.index == najbolji_index)
                    {
                        najbolji = d;
                        break;
                    }
                }
                for (int g = 0; g < 100 * 110; g++) najbolji_w[g] = najbolji.w[g];
                StreamWriter sw = new StreamWriter("c:\\CUdata\\(A)dataset.txt");
                for (int g = 0; g < 110 * 100; g++) sw.WriteLine(najbolji.w[g]);
                sw.Close();
                label1.Text = "DONE!MaxFitnes:" + najbolji.fitnes.ToString();
            }
            else label1.Text = "Break cause:GOOD FITNESS";
        }

        private void Test()
        {
            List<Deo> lista = new List<Deo>();
            for(int i=0;i<100;i++)
            {
                Deo p = new Deo(i);
                lista.Add(p);
            }
            for (int i = 0; i < 50; i++) lista.RemoveAt(0);
            string alfa = "";
            foreach (Deo d in lista) alfa += d.a.ToString()+" ";
            label1.Text = alfa;
        }
    }
}
