/*
 * Created by SharpDevelop.
 * User: TS
 * Date: 11.5.2021.
 * Time: 22.51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Raketa
{
	partial class multi
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Timer timer2;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox4;
		private System.Windows.Forms.TextBox textBox1;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(multi));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox4 = new System.Windows.Forms.PictureBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox1.Location = new System.Drawing.Point(449, 294);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(119, 134);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
			// 
			// timer2
			// 
			this.timer2.Tick += new System.EventHandler(this.Timer2Tick);
			// 
			// pictureBox2
			// 
			this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox2.Location = new System.Drawing.Point(0, 12);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(229, 119);
			this.pictureBox2.TabIndex = 1;
			this.pictureBox2.TabStop = false;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(84, 57);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(75, 18);
			this.progressBar1.TabIndex = 2;
			// 
			// pictureBox3
			// 
			this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox3.Location = new System.Drawing.Point(844, 12);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(100, 79);
			this.pictureBox3.TabIndex = 3;
			this.pictureBox3.TabStop = false;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label1.ForeColor = System.Drawing.SystemColors.Control;
			this.label1.Location = new System.Drawing.Point(679, 41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(159, 34);
			this.label1.TabIndex = 4;
			this.label1.Text = "1200";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// pictureBox4
			// 
			this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox4.Location = new System.Drawing.Point(12, 439);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new System.Drawing.Size(65, 50);
			this.pictureBox4.TabIndex = 5;
			this.pictureBox4.TabStop = false;
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.textBox1.Location = new System.Drawing.Point(242, 468);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(676, 20);
			this.textBox1.TabIndex = 6;
			this.textBox1.Visible = false;
			this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox1KeyPress);
			// 
			// multi
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(944, 501);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.pictureBox4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox3);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.pictureBox1);
			this.Name = "multi";
			this.Text = "Raketa";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.multiKeyDown);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.multiKeyPress);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.multiKeyUp);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
