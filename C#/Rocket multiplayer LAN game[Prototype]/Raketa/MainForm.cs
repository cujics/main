using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Net.Sockets;
namespace Raketa
{
	
	public partial class MainForm : Form
	{
		
		//SERVERSKI DEO
		
		TcpListener serverSocket;
            
		TcpClient clientSocket;
            



		NetworkStream networkStream;
		
		
		
		
		
		public byte[] informacije = new byte[13+40];// 12. bajt prazan
		/*
		 * Struktura informacija
		 * Prvi bajt broj igraca max 256 realno 6
		 * Sledecih 512 * 4 informacije za prikaz metkova X(short) i Y(short) coo
		 * Sledeci 1024 bajta prazno (ako zatreba za nesto)
		 * Sledecih 256*(4+4+16+4) za igrace 4-x i y , 4- ugao , 16 - ime od max 16 karaktera i 4 -skor
		 * ukupno: 1 + 512*4 + 1024 + 256*28 = 93 296 stavicu 100 000
		 * procenjen prenos 100 000B/s oko 100kB/s Za 256 igraca i maximum 512 metkova (po maks 20s)
		 * Za realnu igru bilo bio bi potreban prenos od 564kB/s ali radi otimizacije programa to sam ograncio na 512 metkova
		 * */
		
		
		
		
		/*
		 * 2552021
		 * Struktura promenjena radi optimizacije
		 * Promene:
		 * 1)Igra je namenjena samo za dva igraca
		 * 2)Broj metkova je ogranicen na ukupno deset
		 * 3)Putanja metkova je ogracnicena
		 * Nove kalkulacije:
		 * INFORMACIJE :  IGRAC BR 1(pos 2B + 2B + helt 2B)+IBR2(6B)+10*4B(pos METKOVA)
		 * 
		 * 
		 * */
		System.Media.SoundPlayer sp,bmw;
		public int getTotalM()
		{
			return informacije[2050]+informacije[2051]*(1<<8); // u 2050 i 2051 bajt sam upisao brojno stanje metaka radi optimizacije
		}
		public void ReWtotalM(int z)
		{
			informacije[2050]=(byte)(z%(1<<8));
			informacije[2051]=(byte)(z/(1<<8));
		}
		
		
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
		
		Image img,pumpa,upaljena,nazad,novac,turbo,prodavnica,pR,pRu,pRn,pRt;                  																			//AA
		float gorivo=20,novacc=1200;
		Keys k = Keys.Alt;
		//Keys[] buff = new Keys[10];
		//int buffed=0;
		bool radi=true,bezzvuka=true;
		float glavniugao=0,promenaUGL=5,speed = -4;
		public float METAKspeed=-5f;
		public Control[] cls = new Control[10+1];
		public void OsvInfoDugmeta(int l,int xl,int yl)//Upis pocevsi od 1 VAZI ZA KORDINATE DO MAX SHORT
		{
			int ing=12;//info igraca pos i helt
			label4.Text = "Ichange:"+l.ToString()+" " + xl.ToString() + " " +yl.ToString();
			try{informacije[ing+(l-1)*4+1]=(byte)(xl%(1<<8));}catch{MessageBox.Show("greka1");}
			informacije[ing+(l-1)*4+1+1]=(byte)(xl/(1<<8));
			informacije[ing+(l-1)*4+1+1+1]=(byte)(yl%(1<<8));
			informacije[ing+(l-1)*4+1+1+1+1]=(byte)(yl/(1<<8));
			
		}
		//NOVI REFORMISANI DEO
		
		
		
		
		
		
		
		
		
		//
		int NadjiSlobodnoDugme()
		{
			int ing=12;
			for(int i=0;i<10;i++)
			{
				if((informacije[i*4+1+ing]+informacije[i*4+1+ing+1]*(1<<8))==10000&&(informacije[i*4+1+ing+2]+informacije[i*4+1+ing+3]*(1<<8))==10000)
				{
					label2.Text = (i+1).ToString();
					return i+1;
					
				}
			}
			return -1;
		}
		public int zz=100;
		public void OSV(int zx)
		{
			zz=zx;
			label4.Text = zz.ToString();
			//MessageBox.Show(spt);
		}
		class MetakJak : MainForm
		{
			public int index;
			float glvu,duzina;
			int curx,cury,x,y;
			MainForm ff;
			Timer t;
			public Label ll;
			public MetakJak(int i,Label lc,MainForm fc)
			{
				ff=fc;
				ff.ReWtotalM(ff.getTotalM()+1);
				ll=lc;
				index = i;
				t = new Timer();
				t.Interval=20;
				t.Tick+=new EventHandler(Pocni);
			}
			public void Priprema(float ugao,int currposX,int currposY,float duzz)
			{
				//bc.Location=new Point(currposX,currposY);
				ff.OsvInfoDugmeta(index,currposX,currposY);
				glvu=ugao;
				t.Start();
				duzina=duzz;
				curx=currposX;
				cury=currposY;
			}
			void Pocni(object mender,EventArgs es)
			{
				//MessageBox.Show(bc.Location.X.ToString()+" "+bc.Location.Y.ToString());
				ff.OSV(52);
				ll.Text=(curx.ToString() +" " + cury.ToString() + index.ToString());
				x=(int)(METAKspeed*Math.Sin((glvu/180)*Math.PI));
				y=(int)(METAKspeed*Math.Cos((glvu/180)*Math.PI));
				curx+=x;
				cury+=y;
				//MessageBox.Show(x.ToString()+" "+y.ToString());
				//bc.Location=new Point(bc.Location.X-x,bc.Location.Y+y);
				//cls[index+1].Location=new Point(400,400);
				//cls[index+1].Size=new Size(5000,5000);
				//MessageBox.Show(cls[index+1].Tag.ToString());
				if(curx >=0 && cury >=0 )
				{ff.OsvInfoDugmeta(index,curx,cury);}
				else Kraj();
				duzina--;
				if(duzina<2)Kraj();
			}
			public void Kraj()
			{
				//ll.Text="kraj";
				ff.ReWtotalM(ff.getTotalM()-1);
				t.Dispose();
				ff.OsvInfoDugmeta(index,10000,10000);
				Dispose();
			}
			
		}
		public int HELT;
		MetakJak[] lMetkova;
		int[] dostupnoRastojanje = new int[512+1];
		List<LAVA> BUUT;
		public MainForm()
		{
			//SERVERSKI DEO
			
			serverSocket = new TcpListener(8888);
            
			clientSocket = default(TcpClient);
            
			serverSocket.Start();

			clientSocket = serverSocket.AcceptTcpClient();



			networkStream = clientSocket.GetStream();
			networkStream.Write(informacije,0,53);
			
			ACTIVATED=1;
			HELT =100;
			InitializeComponent();
			OSVHELT();
			BUUT = new List<LAVA>();
			lMetkova=new MetakJak[522];
			//test space
			for(int i=1;i<=10;i++)
			{	Button b = new Button();
				b.Tag=i.ToString();
				b.BackColor=Color.White;
				/////////////////////////b.Location= new Point(10000,10000);
				b.Location=new Point(240+i*2,240+i*2);
				b.Enabled=false;
				b.FlatStyle=FlatStyle.Flat;
				b.Size = new Size(12,12);
				//b.Height=50;b.Width=50;
				this.Controls.Add(b);
				cls[i]=b;
				try{OsvInfoDugmeta(i,10000,10000);}
				catch{MessageBox.Show(i.ToString());}
			}
			
			
			//b.Show();
			
			
			//
			//metak.Start();
			//
			sp = new System.Media.SoundPlayer("rocket.wav");
			
			timer1.Interval=20;
			timer1.Start();
			timer2.Interval=1;
			timer2.Start();
			
			
			//zad																															//BB
			pR = new Bitmap("pR.png");
			pRu = new Bitmap("pRu.png");
			pRn = new Bitmap("pRn.png");
			pRt = new Bitmap("pRt.png");
			pR = (Image)(new Bitmap(pR,new Size(100,100)));
			pRu = (Image)(new Bitmap(pRu,new Size(100,100)));
			pRn = (Image)(new Bitmap(pRn,new Size(100,100)));
			pRt = (Image)(new Bitmap(pRt,new Size(100,100)));
			pictureBox5.Image=pR;
			
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
			
			metak.Start();
			pozicija.Interval=10;
			metak.Interval=10;
			pozicija.Start();
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
				metak.Stop();
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
		void MainFormKeyDown(object sender, KeyEventArgs e)
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
		void MainFormKeyUp(object sender, KeyEventArgs e)
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
					metak.Start();
					pobeda=false;
					informacije[12]=2;
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
		void MainFormKeyPress(object sender, KeyPressEventArgs e)
		{
	
		}
		void MainFormLoad(object sender, EventArgs e)
		{
	
		}
		void MainFormClick(object sender, EventArgs e)
		{
			int f=NadjiSlobodnoDugme();
			if(f!=-1)
			{
				LAVA ipt = new LAVA();
				cls[f].Location=pictureBox1.Location;
				int offset = Convert.ToInt32(50*Math.Sin(Math.PI*glavniugao/180));
				cls[f].Left+=offset;
				ipt.B=(Button)cls[f];
				OsvInfoDugmeta(f,ipt.B.Location.X+offset,ipt.B.Location.Y);
				ipt.value=50;
				ipt.ugao = glavniugao;
				BUUT.Add(ipt);
				//MetakJak mjk = new MetakJak(f,label2,this);
				//mjk.Priprema(glavniugao,(int)(pictureBox1.Location.X*Math.Sin((glavniugao/180)*Math.PI)),(int)(pictureBox1.Location.Y*Math.Cos((glavniugao/180)*Math.PI)));
				//mjk.Priprema(glavniugao,pictureBox1.Location.X+40,pictureBox1.Location.Y,200);
				//MessageBox.Show("Ispaljen"+glavniugao.ToString());
			}
		}
		struct LAVA{
			public Button B;
			public int value;
			public float ugao;
		};
		void OSVHELT()
		{
			if(HELT>0){
				if(HELT<101)progressBar2.Value=HELT;
			}
			
			
			else{
				//MessageBox.Show("Prazno gorivo");
				progressBar2.Value=0;
				timer1.Stop();
				timer2.Stop();
				k=Keys.Alt;
				Upali(0);
				radi=false;
				sp.Stop();
				bezzvuka=true;
			}
		}
		int info=1,check=1,chekcer2=0;
		void MetakTick(object sender, EventArgs e)
		{
				/*chekcer2= 0;
				int xx = informacije[info]+informacije[info+1]*(1<<8);
				int yy = informacije[info+2]+informacije[info+3]*(1<<8);
				if(info==1){label5.Text = xx.ToString() + " " + yy.ToString() + "C:" + check.ToString();}
				//MessageBox.Show(chekcer2.ToString());
				cls[info/4+1].Location= new Point(xx,yy);
				
				
				
				//if(xx!=10000 || yy!=10000)
				if((info/4)+1==1)
				{label3.Text = "C:"+check.ToString()+" I:"+(info/4+1).ToString()+" X:"+xx.ToString()+" Y:"+yy.ToString();check++;}
				info+=4;
				if(info/4+1>getTotalM())info=1;
				label6.Text = "totalM:"+ getTotalM().ToString();*/
				
				
				//TIK:
				
				for(int i=0;i<BUUT.Count;i++)
				{
					if(BUUT[i].value>0&&BUUT[i].B.Location.X>0&&BUUT[i].B.Location.Y>0)
					{
						int x=(int)(-METAKspeed*Math.Sin((BUUT[i].ugao/180)*Math.PI));
						int y=(int)(METAKspeed*Math.Cos((BUUT[i].ugao/180)*Math.PI));
						BUUT[i].B.Left+=x;
						BUUT[i].B.Top+=y;
						if(BUUT[i].B.Bounds.IntersectsWith(pictureBox1.Bounds)){HELT--;OSVHELT();}
						LAVA p = BUUT[i];
						p.value--;
						BUUT[i]=p;
						OsvInfoDugmeta(Convert.ToInt32(BUUT[i].B.Tag),BUUT[i].B.Location.X,BUUT[i].B.Location.Y);
					}
					else
					{
						BUUT[i].B.Location=new Point(10000,10000);
						OsvInfoDugmeta(Convert.ToInt32(BUUT[i].B.Tag),BUUT[i].B.Location.X,BUUT[i].B.Location.Y);
						//L.value = -1;
						BUUT.Remove(BUUT[i]);
					}
					
				}
				
		}
		void Button1Click(object sender, EventArgs e)
		{
			if(1==1)MessageBox.Show(informacije[(1-1)*4+3].ToString());
		}
		int ACTIVATED;
		void KRAJ()
		{
			metak.Stop();
			timer1.Stop();
			radi=false;
			informacije[12]=(byte)(3);
		}
		void OPET()
		{
			//ReSeter();
			timer1.Start();
			novacc=1200;
			gorivo=20;
			//timer2.Start();
			OsvežiGorivo();
			OsvežiNovac();
			radi=true;
			speed=-4;
			metak.Start();
			pobeda=false;
		}
		bool pobeda=false;
		void PozicijaTick(object sender, EventArgs e)
		{
			//SERVERSKI DEO
			networkStream.Read(informacije,0,53);
			if(informacije[12]==0)
			{
				informacije[0]=(byte)(pictureBox1.Location.X%(1<<8));
			informacije[1]=(byte)(pictureBox1.Location.X/(1<<8));
			informacije[2]=(byte)(pictureBox1.Location.Y%(1<<8));
			informacije[3]=(byte)(pictureBox1.Location.Y/(1<<8));
			informacije[4]=(byte)(HELT%(1<<8));
			pictureBox5.Location = new Point(informacije[6]+informacije[7]*(1<<8),informacije[8]+informacije[9]*(1<<8));
			if(informacije[10]==0){pobeda=true;MessageBox.Show("Pobedio si!");KRAJ();informacije[12]=3;}
			int ing=12;
			for(int i=1;i<=40;i+=4)
			{
				int xx = informacije[ing+i]+informacije[ing+i+1]*(1<<8);
				int yy = informacije[ing+i+2]+informacije[ing+i+3]*(1<<8);
				cls[i/4+1].Location=new Point(xx,yy);
			}
			}
			else if(informacije[12]==2)
			{
				informacije[12]=(byte)(0);
				OPET();
			}
			else if(informacije[12]==3 && pobeda==false){
				KRAJ();
				informacije[12]=(byte)(5);
				MessageBox.Show("IZGUBIO SI!");
			}
			networkStream.Write(informacije,0,53);
		}
	}
}
