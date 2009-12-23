using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InterProcessCommunication;

namespace TestDisplayVideo
{
    public partial class FormTestVideoTransfert : Form
    {
        public FormTestVideoTransfert()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var v = new InterProcessCommunication.VideoTranfert();

            v.WriteFrame(new VideoTranfert.Frame(textBox1.Text,25,new Bitmap(pictureBox1.Image)));

            var f = v.ReadFrame();

            pictureBox2.Image = f.Bitmap;
            label1.Text = f.FileName;
            Console.WriteLine(f.FrameRate);
            Console.WriteLine(f.EndOfRecord);
        }
    }
}
