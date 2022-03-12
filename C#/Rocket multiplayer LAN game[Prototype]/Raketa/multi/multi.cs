using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Net.Sockets;
namespace Raketa
{
	
	public partial class multi : Form
	{
		public byte[] informacije = new byte[100000];
		/*
		 * Struktura informacija
		 * Prvi bajt broj igraca max 256 realno 6
		 * Sledecih 51200 * 4 informacije za prikaz metkova X(short) i Y(short) coo
		 * Sledeci 1024 bajta prazno (ako zatreba za nesto)
		 * Sledecih 256*(4+4+16+4) za igrace 4-x i y , 4- ugao , 16 - ime od max 16 karaktera i 4 -skor
		 * ukupno: 1 + 512*4 + 1024 + 256*28 = 93 296 stavicu 100 000
		 * procenjen prenos 100 000B/s oko 100kB/s Za 256 igraca i maximum 512 metkova (po maks 20s)
		 * Za realnu igru bilo bio bi potreban prenos od 564kB/s ali radi otimizacije programa to sam ograncio na 512 metkova
		 * */
		System.Media.SoundPlayer sp,bmw;
		void DobijGranice(PointF[] tacke,out float xmin,out float xmax,out float ymin,out float ymax)
		{
			xmin = tacke[0].X;
			xmax = xmin;
			ymin = tacke[0].Y;
			ymax = ymin;
			foreach(PointF tacka in tacke)
			{
				if(xmin > tacka.X) xmin = tacka.X;
				if(xmax < tacka.X) xmax = tacka.X;
				if(ymin > tacka.Y) ymin = tacka.Y;
				if(ymax < tacka.Y) ymax = tacka.Y;
			}
		}
		
		Bitmap Rotiraj(Bitmap sliku,float ugao)
		{
			Matrix original = new Matrix();
			original.Rotate(ugao);
			PointF[] coskovi = { new PointF(0,0),new PointF(sliku.Width,0),new PointF(sliku.Width,sliku.Height),new PointF(0,sliku.Height)};
			original.TransformPoints(coskovi);
			float xmin,xmax,ymin,ymax;
			DobijGranice(coskovi,out xmin,out xmax,out ymin,out ymax);
			int duzina = (int)Math.Round(xmax-xmin);
			int visina = (int)Math.Round(ymax-ymin);
			Bitmap rotirana = new Bitmap(duzina,visina);
			
			Matrix reorg = new Matrix();
			reorg.RotateAt(ugao,new PointF(duzina/2,visina/2));
			
			using (Graphics g = Graphics.FromImage(rotirana))
			{
				g.InterpolationMode = InterpolationMode.High; //da bude cista
				g.Clear(sliku.GetPixel(0,0));//providi je
				g.Transform = reorg;
				int x = (duzina-sliku.Width)/2;
				int y = (visina-sliku.Height)/2;
				g.DrawImage(sliku,x,y);
			}
			return rotirana;
			
		}
		
		Image img,pumpa,upaljena,nazad,novac,turbo,prodavnica;
		float gorivo=20,novacc=1200;
		Keys k = Keys.Alt;
		//Keys[] buff = new Keys[10];
		//int buffed=0;
		bool radi=true,bezzvuka=true;
		float glavniugao=0,promenaUGL=5,speed = -4;
		public multi()
		{
			sp = new System.Media.SoundPlayer("rocket.wav");
			InitializeComponent();
			timer1.Interval=20;
			timer1.Start();
			timer2.Interval=1;
			timer2.Start();
			img = new Bitmap("rocketOFF.png");
			prodavnica = new Bitmap("prodavnica.png");
			turbo = new Bitmap("rokcetTURBO.png");
			novac = new Bitmap("novac.png");
			upaljena= new Bitmap("rocketON.png");
			nazad= new Bitmap("rocketNAZAD.png");
			pumpa = new Bitmap("fuel.png");
			img = (Image)(new Bitmap(img,new Size(100,100)));
			prodavnica = (Image)(new Bitmap(prodavnica,new Size(50,50)));
			turbo = (Image)(new Bitmap(turbo,new Size(100,100)));
			novac = (Image)(new Bitmap(novac,new Size(75,75)));
			nazad = (Image)(new Bitmap(nazad,new Size(100,100)));
			upaljena = (Image)(new Bitmap(upaljena,new Size(100,100)));
			pumpa = (Image)(new Bitmap(pumpa,new Size(200,100)));
			pictureBox1.Image=img;
			pictureBox2.Image=pumpa;
			pictureBox3.Image=novac;
			pictureBox4.Image=prodavnica;
			Graphics g = Graphics.FromImage(img);
			g.Transform.Rotate(35);
			DoubleBuffered=true;
			g.DrawImage(img,new Point(0,0));
			pictureBox1.Image=img;
			OsvežiGorivo();
		}
		void OsvežiNovac()
		{
			label1.Text = novacc.ToString();
		}
		void OsvežiGorivo()
		{
			if(gorivo>0){
				if(gorivo<100)progressBar1.Value=(int)gorivo;
			}
			
			
			else{
				//MessageBox.Show("Prazno gorivo");
				progressBar1.Value=0;
				timer1.Stop();
				timer2.Stop();
				k=Keys.Alt;
				Upali(0);
				radi=false;
				sp.Stop();
				bezzvuka=true;
			} 
			
		}
		void Upali(int p)
		{
			if(p==1)
			{
				pictureBox1.Image = Rotiraj((Bitmap)upaljena,glavniugao);
			}
			else if(p==2)
			{
				pictureBox1.Image = Rotiraj((Bitmap)nazad,glavniugao);
			}
			else if(p==4)
			{
				pictureBox1.Image = Rotiraj((Bitmap)turbo,glavniugao);
			}
			else 
			{
				pictureBox1.Image = Rotiraj((Bitmap)img,glavniugao);
			}
			
		}
		void multiKeyDown(object sender, KeyEventArgs e)
		{
			k=e.KeyCode;
			if(k==Keys.P&&gorivo <=80 && novacc>=100 && radi){
				novacc-=100;
				gorivo+=20;
				OsvežiGorivo();
				OsvežiNovac();
			}
			else if(k==Keys.K)textBox1.Visible=!textBox1.Visible;
		}
		void RLvozi(int pp)
		{
			glavniugao=(glavniugao+promenaUGL*pp)%360;
			pictureBox1.Image = Rotiraj((Bitmap)img,glavniugao);
			gorivo-=0.2f;OsvežiGorivo();
			Upali(1);
			OsvežiGorivo();
			if(bezzvuka){sp.Play();bezzvuka=false;};
		}
		void Timer1Tick(object sender, EventArgs e)
		{
			//if(k==Keys.D){glavniugao=(glavniugao+promenaUGL)%360;pictureBox1.Image = Rotiraj((Bitmap)img,glavniugao);gorivo-=0.2f;OsvežiGorivo();Upali(1);}
			//if(k==Keys.A){glavniugao=(glavniugao-promenaUGL)%360;pictureBox1.Image = Rotiraj((Bitmap)img,glavniugao);gorivo-=0.2f;OsvežiGorivo();Upali(1);}
			if(k==Keys.D)RLvozi(1);
			else if(k==Keys.A)RLvozi(-1);
			else if(k==Keys.W)IdiNapred(1);
			else if(k==Keys.S)IdiNapred(-1);
			else if(k==Keys.Space)IdiNapred(4);
		}
		void multiKeyUp(object sender, KeyEventArgs e)
		{
			
				Upali(0);
				k=Keys.Alt;
				sp.Stop();
				bezzvuka=true;
			
		}
		void IdiNapred(int pz)
		{
			if(pz==1)Upali(1);
			else if(pz==4)Upali(4);
			else Upali(2);
			int x=(int)(pz*speed*Math.Sin((glavniugao/180)*Math.PI));
			int y=(int)(pz*speed*Math.Cos((glavniugao/180)*Math.PI));
			pictureBox1.Location=new Point(pictureBox1.Location.X-x,pictureBox1.Location.Y+y);
			if(pz==1)gorivo-=0.2f;
			else if(pz==4)gorivo-=1.6f;
			else gorivo-=0.4f;
			OsvežiGorivo();
			if(bezzvuka){sp.Play();bezzvuka=false;};
			
		}
		void ReSeter()
		{
			sp = new System.Media.SoundPlayer("rocket.wav");
			img = new Bitmap("rocketOFF.png");
			turbo = new Bitmap("rokcetTURBO.png");
			upaljena= new Bitmap("rocketON.png");
			nazad= new Bitmap("rocketNAZAD.png");
			
			img = (Image)(new Bitmap(img,new Size(100,100)));
			turbo = (Image)(new Bitmap(turbo,new Size(100,100)));
			nazad = (Image)(new Bitmap(nazad,new Size(100,100)));
			upaljena = (Image)(new Bitmap(upaljena,new Size(100,100)));
			pictureBox1.Image=img;
		}
		void Seter()
		{
			sp = new System.Media.SoundPlayer("bmw.wav");
			img = new Bitmap("mrocketOFF.png");
			turbo = new Bitmap("mrocketTURBO.png");
			upaljena= new Bitmap("mrocketON.png");
			nazad= new Bitmap("mrocketNAZAD.png");
			
			img = (Image)(new Bitmap(img,new Size(100,100)));
			turbo = (Image)(new Bitmap(turbo,new Size(100,100)));
			nazad = (Image)(new Bitmap(nazad,new Size(100,100)));
			upaljena = (Image)(new Bitmap(upaljena,new Size(100,100)));
			pictureBox1.Image=img;
		}
		void Timer2Tick(object sender, EventArgs e)
		{
			/*if(k==Keys.W)IdiNapred(1);
			if(k==Keys.S)IdiNapred(-1);
			if(k==Keys.Space)IdiNapred(4);*/
			
			
		}
		void TextBox1KeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar==(char)Keys.Enter)
			{
				if(textBox1.Text == "bembara")
				{
					Seter();
					
				}
				else if(textBox1.Text == "opet")
				{
					ReSeter();
					timer1.Start();
					novacc=1200;
					gorivo=20;
					timer2.Start();
					OsvežiGorivo();
					OsvežiNovac();
					radi=true;
					speed=-4;
				}
				else if(textBox1.Text == "brzo")
				{
					if(speed*4<float.MaxValue)speed*=4;
				}
				else if(textBox1.Text == "daj pari")
				{
					if(novacc*16<float.MaxValue)novacc*=16;
					OsvežiNovac();
				}
				else if(textBox1.Text == "max gorivo")
				{
					gorivo=float.MaxValue;
					progressBar1.Value=100;
				}
				textBox1.Clear();
				this.ActiveControl=null;
			}
		}
		void multiKeyPress(object sender, KeyPressEventArgs e)
		{
	
		}
	}
}
