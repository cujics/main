using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RekognajzerBrojeva_ANN
{
    public partial class Form2 : Form
    {
        Form1 f1;
        int i = 0,j=0;
        public Form2(Form1 ovo)
        {
            InitializeComponent();
            f1 = ovo;
            dgvINIT();
            f1.Enabled = false;
            this.Show();
        }
        private void Close()
        {
            f1.Enabled = true;
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            j++;
            if (j > 3)
            {
                j = 1;i++;
            }
            if (i > 9) Close();
            dgvUPDATE(i, j);
        }
        private void dgvCRNA()
        {
            for (int i = 0; i < 10; i++) for (int j = 0; j < 10; j++) dataGridView1[i, j].Style.BackColor = Color.Black;
        }
        private void dgvUPDATE(int i,int j)
        {
            string put = "c:\\CUdata\\bmp\\" + i.ToString()+"_"+j.ToString() + ".bmp";
            Bitmap slik = new Bitmap(put);
            dgvCRNA();
            for (int a = 0; a < 10; a++) for (int b = 0; b < 10; b++) if (slik.GetPixel(a, b).R > 50) dataGridView1[a, b].Style.BackColor = Color.Red;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvINIT()
        {
            dataGridView1.RowCount = 10;
            dataGridView1.ColumnCount = 10;
            for (int i = 0; i < dataGridView1.RowCount; i++) dataGridView1.Rows[i].Height = dataGridView1.Size.Height / dataGridView1.RowCount;
            for (int i = 0; i < dataGridView1.ColumnCount; i++) dataGridView1.Columns[i].Width = dataGridView1.Size.Width / dataGridView1.ColumnCount;
        }
    }
}
