using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VideoPlayer
{
    public partial class DisplayVideo : Form
    {
        public DisplayVideo()
        {
            InitializeComponent();
        }

        private void paramètresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new EditionParametreTraitement().ShowDialog();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

            }                
        }
    }
}
