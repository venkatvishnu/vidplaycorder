using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VideoPlayer.State;

namespace VideoPlayer
{
    public partial class DisplayVideo : Form, IFrameDisplay
    {

        private delegate void RefreshImageDelegate(Bitmap image);

        private PlayerStateController _controller;

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

        private void RefreshImage(Bitmap image)
        {
            framePictureBox.Image = image;
            framePictureBox.Refresh();
        }
        
        public void UpdateFrame(Bitmap frame)
        {
            RefreshImageDelegate del = RefreshImage;

            this.BeginInvoke(del, new[] {frame});
        }

        private void DisplayVideo_Shown(object sender, EventArgs e)
        {
            _controller = new PlayerStateController(this);
            RefreshInterface();
        }

        private void DisplayVideo_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void RefreshInterface()
        {
            if(!_controller.FileOpen)
            {
                lecturePauseToolStripMenuItem.Enabled = playButton.Enabled = false;
                arrêterToolStripMenuItem.Enabled = stopButton.Enabled = false;
                rembobinToolStripMenuItem.Enabled = rewindButton.Enabled = false;
                avanceRapideToolStripMenuItem.Enabled = forwardButton.Enabled = false;
                débutArrêtToolStripMenuItem.Enabled = reccordButton.Enabled = false;
                fermerToolStripMenuItem.Enabled = false;
            }
            else
            {
                if(_controller.IsPlaying)
                    playButton.Image = VideoPlayer.Properties.Resources.pause;
                else
                    playButton.Image = VideoPlayer.Properties.Resources.play;

                lecturePauseToolStripMenuItem.Enabled = playButton.Enabled = true;
                arrêterToolStripMenuItem.Enabled = stopButton.Enabled = _controller.IsPlaying || _controller.IsPaused;
                rembobinToolStripMenuItem.Enabled = rewindButton.Enabled = true;
                avanceRapideToolStripMenuItem.Enabled = forwardButton.Enabled = true;
                débutArrêtToolStripMenuItem.Enabled = reccordButton.Enabled = true;
                fermerToolStripMenuItem.Enabled = true;
            }
        }

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Text = openFileDialog1.FileName;
                _controller.Open(openFileDialog1.FileName);
            }
            RefreshInterface();
        }

        private void fermerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.Close();
            RefreshInterface();
        }

        private void lecturePauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!_controller.IsPlaying)
                _controller.Play();
            else
                _controller.Pause();

            RefreshInterface();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            _controller.Stop();
            RefreshInterface();
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            _controller.Forward();
            RefreshInterface();
        }

        private void rewindButton_Click(object sender, EventArgs e)
        {
            _controller.Rewind();
            RefreshInterface();
        }

        private void reccordButton_Click(object sender, EventArgs e)
        {
            _controller.Record();
            RefreshInterface();
        }
    }
}
