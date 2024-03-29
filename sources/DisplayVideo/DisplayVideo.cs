﻿using System;
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
        private delegate void RefreshInterfaceDelegate();
        
        private PlayerStateController _controller;

        private string _outputFile = null;

        public DisplayVideo()
        {
            InitializeComponent();

            OutputFile = null;
        }

        public string OutputFile
        {
            get { return _outputFile; }
            set
            {
                _outputFile = value;

                recordFileToolStripLabel.Text = string.IsNullOrEmpty(_outputFile) ? "Aucun" : _outputFile;
            }
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
            _controller.CurrentStateChanged += new EventHandler(ControllerCurrentStateChanged);
            RefreshInterface();
            BringToFront();
        }

        void ControllerCurrentStateChanged(object sender, EventArgs e)
        {
            BeginInvoke(new RefreshInterfaceDelegate(RefreshInterface));
        }

        private void DisplayVideo_FormClosing(object sender, FormClosingEventArgs e)
        {
            UseWaitCursor = true;
            Visible = false;
            _controller.Dispose();
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
                lecturePauseToolStripMenuItem.Enabled = playButton.Enabled = true;
                arrêterToolStripMenuItem.Enabled = stopButton.Enabled = _controller.IsPlaying || _controller.IsPaused;
                rembobinToolStripMenuItem.Enabled = rewindButton.Enabled = _controller.IsPlaying || _controller.IsPaused;
                avanceRapideToolStripMenuItem.Enabled = forwardButton.Enabled = _controller.IsPlaying || _controller.IsPaused;
                débutArrêtToolStripMenuItem.Enabled = reccordButton.Enabled = (_controller.IsPlaying || _controller.IsPaused) && !_controller.IsFastPlaying;
                fermerToolStripMenuItem.Enabled = true;
                sélectionnerFichierDenregistrementToolStripMenuItem.Enabled = !(_controller.IsReccording && _controller.IsPlaying);
            }

            if (_controller.IsPlaying && !_controller.IsFastPlaying)
                playButton.Image = VideoPlayer.Properties.Resources.pause;
            else
                playButton.Image = VideoPlayer.Properties.Resources.play;

            enregistrementEnCoursPictureBox.Visible = _controller.IsReccording;
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
            if(!_controller.IsPlaying || _controller.IsFastPlaying)
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
            // Si lanregistrement n'est pas lancé
            if (_controller.IsReccording == false)
            {
                // S'il n'y a pas de fichier de sortie
                if (string.IsNullOrEmpty(OutputFile))
                {
                    SelectOutputFile();
                }
            }

            // Il doit y avoir une fichier de sortie pour l'enregistrement
            if(!string.IsNullOrEmpty(OutputFile))
                _controller.Record(OutputFile);

            RefreshInterface();
        }

        private void sélectionnerFichierDenregistrementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectOutputFile();
        }

        private void SelectOutputFile()
        {
            saveFileDialog1.Title = "Quel sera le nom du fichier vidéo de sorti?";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OutputFile = saveFileDialog1.FileName;
            }
        }
    }
}
