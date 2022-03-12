/*
 * Created by SharpDevelop.
 * User: TS
 * Date: 13.5.2021.
 * Time: 21.59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Raketa
{
	/// <summary>
	/// Description of Uvod.
	/// </summary>
	public partial class Uvod : Form
	{
		System.Media.SoundPlayer sp;
		int speed1=2,speed2=3,speed4=4;
		public Uvod()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			sp = new System.Media.SoundPlayer("Uvodna.wav");
			sp.Play();
			timer1.Interval=100;
			timer1.Start();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void Button3Click(object sender, EventArgs e)
		{
			MessageBox.Show("Snadji se druze");
			button3.Visible=false;
		}
		void Timer1Tick(object sender, EventArgs e)
		{
			if(button1.Location.X>this.Width+button1.Width){button1.Location=new Point(-button1.Width,button1.Location.Y);}
			button1.Left+=speed1;
			if(button3.Location.X>this.Width+button3.Width){button3.Location=new Point(-button3.Width,button3.Location.Y);}
			button3.Left+=speed4;
			if(button2.Location.X<-button2.Width){button2.Location=new Point(this.Width+button2.Width,button2.Location.Y);}
			button2.Left-=speed2;
		}
		void Button2Click(object sender, EventArgs e)
		{
			MainForm mf = new MainForm();
			mf.Show();
			mf.FormClosed+=(s,args)=>this.Close();
			//mf.FormClosed += new EventHandler(Ovo);
			this.Hide();
		}
		void Ovo()
		{
			this.Close();
		}
	}
}
