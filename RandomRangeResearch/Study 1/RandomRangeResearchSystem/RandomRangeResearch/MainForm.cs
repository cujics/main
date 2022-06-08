using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace RandomRangeResearch
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}
		int Case_Q_Index(int min,int max,double Q_Index)
		{
			return min+Convert.ToInt32(Math.Round((max-min)*Q_Index));
		}
		void Show_Case(int min,int max,int mQ_Index)
		{
			if(File.Exists("raw_data.txt"))File.Delete("raw_data.txt");
			StreamWriter To_File = new StreamWriter("raw_data.txt");
			listBox1.Items.Clear();
			listBox2.Items.Clear();
			Dictionary<int,int> map = new Dictionary<int, int>();
			for(int i=0;i<1000*mQ_Index+1;i++){
				double Q_Index = (i/1000.0)/mQ_Index;
				int result =Case_Q_Index(min,max,Q_Index);
				if(map.ContainsKey(result))map[result]++;
				else map[result]=1;
				string temp = Q_Index.ToString() + ":" + result.ToString();
				To_File.WriteLine(temp);
				listBox1.Items.Add(temp);
			}
			To_File.Close();
			if(File.Exists("raw_data2.txt"))File.Delete("raw_data2.txt");
			To_File = new StreamWriter("raw_data2.txt");
			foreach(KeyValuePair<int,int> pair in map){
				string temp = pair.Key.ToString() + ":" + pair.Value.ToString();
				listBox2.Items.Add(temp);
				To_File.WriteLine(temp);
			}
			To_File.Close();
		}
		void Button1Click(object sender, EventArgs e)
		{
			try{Show_Case(Convert.ToInt32(textBox1.Text),Convert.ToInt32(textBox2.Text),trackBar1.Value);}
			catch{}
		}
	}
}
