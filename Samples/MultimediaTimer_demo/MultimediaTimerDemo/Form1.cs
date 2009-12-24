using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Multimedia;

namespace MultimediaTimerDemo
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtPeriod;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbPeriodic;
        private System.Windows.Forms.RadioButton rbOneShot;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtResolution;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPeriodMax;
        private System.Windows.Forms.Label lblPeriodMin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStart;
        private Multimedia.Timer mmTimer;
        private System.ComponentModel.IContainer components;

        int counter;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtPeriod = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbPeriodic = new System.Windows.Forms.RadioButton();
            this.rbOneShot = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblCounter = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtResolution = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPeriodMax = new System.Windows.Forms.Label();
            this.lblPeriodMin = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.mmTimer = new Multimedia.Timer(this.components);
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtPeriod);
            this.groupBox3.Location = new System.Drawing.Point(8, 96);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(152, 56);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Period";
            // 
            // txtPeriod
            // 
            this.txtPeriod.Location = new System.Drawing.Point(24, 24);
            this.txtPeriod.MaxLength = 7;
            this.txtPeriod.Name = "txtPeriod";
            this.txtPeriod.TabIndex = 0;
            this.txtPeriod.Text = "";
            this.txtPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPeriod.WordWrap = false;
            this.txtPeriod.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPeriod_KeyPress);
            this.txtPeriod.Validating += new System.ComponentModel.CancelEventHandler(this.txtPeriod_Validating);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbPeriodic);
            this.groupBox2.Controls.Add(this.rbOneShot);
            this.groupBox2.Location = new System.Drawing.Point(168, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(144, 80);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mode";
            // 
            // rbPeriodic
            // 
            this.rbPeriodic.Location = new System.Drawing.Point(24, 48);
            this.rbPeriodic.Name = "rbPeriodic";
            this.rbPeriodic.TabIndex = 1;
            this.rbPeriodic.Text = "Periodic";
            this.rbPeriodic.CheckedChanged += new System.EventHandler(this.rbPeriodic_CheckedChanged);
            // 
            // rbOneShot
            // 
            this.rbOneShot.Location = new System.Drawing.Point(24, 16);
            this.rbOneShot.Name = "rbOneShot";
            this.rbOneShot.TabIndex = 0;
            this.rbOneShot.Text = "One Shot";
            this.rbOneShot.CheckedChanged += new System.EventHandler(this.rbOneShot_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblCounter);
            this.groupBox5.Location = new System.Drawing.Point(168, 168);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(144, 56);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Counter";
            // 
            // lblCounter
            // 
            this.lblCounter.Location = new System.Drawing.Point(16, 24);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(112, 16);
            this.lblCounter.TabIndex = 0;
            this.lblCounter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(200, 136);
            this.btnStop.Name = "btnStop";
            this.btnStop.TabIndex = 12;
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtResolution);
            this.groupBox4.Location = new System.Drawing.Point(8, 168);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(152, 56);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Resolution";
            // 
            // txtResolution
            // 
            this.txtResolution.Location = new System.Drawing.Point(24, 24);
            this.txtResolution.Name = "txtResolution";
            this.txtResolution.Size = new System.Drawing.Size(104, 20);
            this.txtResolution.TabIndex = 0;
            this.txtResolution.Text = "";
            this.txtResolution.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtResolution.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtResolution_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPeriodMax);
            this.groupBox1.Controls.Add(this.lblPeriodMin);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 80);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Capabilities";
            // 
            // lblPeriodMax
            // 
            this.lblPeriodMax.Location = new System.Drawing.Point(88, 48);
            this.lblPeriodMax.Name = "lblPeriodMax";
            this.lblPeriodMax.Size = new System.Drawing.Size(48, 16);
            this.lblPeriodMax.TabIndex = 3;
            this.lblPeriodMax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPeriodMin
            // 
            this.lblPeriodMin.Location = new System.Drawing.Point(88, 24);
            this.lblPeriodMin.Name = "lblPeriodMin";
            this.lblPeriodMin.Size = new System.Drawing.Size(48, 16);
            this.lblPeriodMin.TabIndex = 2;
            this.lblPeriodMin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Period Max:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Period Min:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(200, 104);
            this.btnStart.Name = "btnStart";
            this.btnStart.TabIndex = 11;
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // mmTimer
            // 
            this.mmTimer.Mode = Multimedia.TimerMode.Periodic;
            this.mmTimer.Period = 1;
            this.mmTimer.Resolution = 1;
            this.mmTimer.SynchronizingObject = this;
            this.mmTimer.Tick += new System.EventHandler(this.mmTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(328, 246);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.groupBox3);
            this.Name = "Form1";
            this.Text = "Multimedia Timer Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

        private void btnStart_Click(object sender, System.EventArgs e)
        {
            try
            {
                counter = 0;
                lblCounter.Text = counter.ToString();
                mmTimer.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
            }
        }

        private void btnStop_Click(object sender, System.EventArgs e)
        {
            mmTimer.Stop();
        }

        private void rbOneShot_CheckedChanged(object sender, System.EventArgs e)
        {
            if(rbOneShot.Checked)
            {
                mmTimer.Mode = TimerMode.OneShot;
            }
        }

        private void rbPeriodic_CheckedChanged(object sender, System.EventArgs e)
        {
            if(rbPeriodic.Checked)
            {
                mmTimer.Mode = TimerMode.Periodic;
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            TimerCaps caps = Multimedia.Timer.Capabilities;

            lblPeriodMin.Text = caps.periodMin.ToString();
            lblPeriodMax.Text = caps.periodMax.ToString();
            txtPeriod.Text = mmTimer.Period.ToString();
            txtResolution.Text = mmTimer.Resolution.ToString();

            if(mmTimer.Mode == TimerMode.OneShot)
            {
                rbOneShot.Checked = true;
            }
            else
            {
                rbPeriodic.Checked = true;
            }
        }

        private void txtPeriod_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if(!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtResolution_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {            
            if(!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }        
        }

        private void txtPeriod_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int period = int.Parse(txtPeriod.Text);

            if(period < Multimedia.Timer.Capabilities.periodMin || 
                period > Multimedia.Timer.Capabilities.periodMax)
            {
                errorProvider1.SetError(txtPeriod, "Period out of range.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtPeriod, "");
                mmTimer.Period = period;
            }
        }

        private void txtResolution_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int resolution = int.Parse(txtResolution.Text);

            if(resolution < 0)
            {
                errorProvider1.SetError(txtResolution, "Resolution out of range.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtResolution, "");
                mmTimer.Resolution = resolution;
            }
        }

        private void mmTimer_Tick(object sender, System.EventArgs e)
        {
            counter++;
            lblCounter.Text = counter.ToString();
        }
	}
}
