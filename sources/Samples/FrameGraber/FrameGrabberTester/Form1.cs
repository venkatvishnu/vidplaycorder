using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using User.DirectShow;

namespace FrameGrabberTester
{
	public partial class Form1 : Form
	{
		private FrameGrabber fg;

		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				fg = new FrameGrabber(openFileDialog1.FileName);
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			folderBrowserDialog1.ShowDialog();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			button1.Enabled = false;
			button2.Enabled = false;
			button3.Enabled = false;

			string outPath = (folderBrowserDialog1.SelectedPath == "" ? System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test") : folderBrowserDialog1.SelectedPath);
			System.IO.Directory.CreateDirectory(outPath);

			if(fg != null)
			{
				foreach(FrameGrabber.Frame f in fg)
				{
					using(f)
					{
						pictureBox1.Image = (Bitmap)f.Image.Clone();
						f.Image.Save(System.IO.Path.Combine(outPath, "frame" + f.FrameIndex + ".bmp"), System.Drawing.Imaging.ImageFormat.Bmp);
						Application.DoEvents();
					}

					if(fg == null)
					{
						return;
					}
				}
			}

			button1.Enabled = true;
			button2.Enabled = true;
			button3.Enabled = true;
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			fg = null;
		}
	}
}