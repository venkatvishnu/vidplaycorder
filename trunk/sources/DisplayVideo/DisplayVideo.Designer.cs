namespace VideoPlayer
{
    partial class DisplayVideo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ouvrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fermerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.sélectionnerFichierDenregistrementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lectureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lecturePauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arrêterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.rembobinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.avanceRapideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enregistrementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.débutArrêtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.traitementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paramètresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playButton = new System.Windows.Forms.Button();
            this.rewindButton = new System.Windows.Forms.Button();
            this.forwardButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.reccordButton = new System.Windows.Forms.Button();
            this.framePictureBox = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.framePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.lectureToolStripMenuItem,
            this.enregistrementToolStripMenuItem,
            this.traitementToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(750, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fichierToolStripMenuItem
            // 
            this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ouvrirToolStripMenuItem,
            this.fermerToolStripMenuItem,
            this.toolStripMenuItem1,
            this.sélectionnerFichierDenregistrementToolStripMenuItem,
            this.toolStripMenuItem3,
            this.quitterToolStripMenuItem});
            this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.fichierToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.fichierToolStripMenuItem.Text = "Fichier";
            // 
            // ouvrirToolStripMenuItem
            // 
            this.ouvrirToolStripMenuItem.Name = "ouvrirToolStripMenuItem";
            this.ouvrirToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.ouvrirToolStripMenuItem.Text = "Ouvrir";
            this.ouvrirToolStripMenuItem.Click += new System.EventHandler(this.ouvrirToolStripMenuItem_Click);
            // 
            // fermerToolStripMenuItem
            // 
            this.fermerToolStripMenuItem.Name = "fermerToolStripMenuItem";
            this.fermerToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.fermerToolStripMenuItem.Text = "Fermer";
            this.fermerToolStripMenuItem.Click += new System.EventHandler(this.fermerToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(236, 6);
            // 
            // sélectionnerFichierDenregistrementToolStripMenuItem
            // 
            this.sélectionnerFichierDenregistrementToolStripMenuItem.Name = "sélectionnerFichierDenregistrementToolStripMenuItem";
            this.sélectionnerFichierDenregistrementToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.sélectionnerFichierDenregistrementToolStripMenuItem.Text = "Créer le fichier d\'enregistrement";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(236, 6);
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.quitterToolStripMenuItem.Text = "Quitter";
            this.quitterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
            // 
            // lectureToolStripMenuItem
            // 
            this.lectureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lecturePauseToolStripMenuItem,
            this.arrêterToolStripMenuItem,
            this.toolStripMenuItem2,
            this.rembobinToolStripMenuItem,
            this.avanceRapideToolStripMenuItem});
            this.lectureToolStripMenuItem.Name = "lectureToolStripMenuItem";
            this.lectureToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.lectureToolStripMenuItem.Text = "Lecture";
            // 
            // lecturePauseToolStripMenuItem
            // 
            this.lecturePauseToolStripMenuItem.Name = "lecturePauseToolStripMenuItem";
            this.lecturePauseToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.lecturePauseToolStripMenuItem.Text = "Lecture/Pause";
            this.lecturePauseToolStripMenuItem.Click += new System.EventHandler(this.lecturePauseToolStripMenuItem_Click);
            // 
            // arrêterToolStripMenuItem
            // 
            this.arrêterToolStripMenuItem.Name = "arrêterToolStripMenuItem";
            this.arrêterToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.arrêterToolStripMenuItem.Text = "Arrêter";
            this.arrêterToolStripMenuItem.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(151, 6);
            // 
            // rembobinToolStripMenuItem
            // 
            this.rembobinToolStripMenuItem.Name = "rembobinToolStripMenuItem";
            this.rembobinToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.rembobinToolStripMenuItem.Text = "Rembobinage";
            // 
            // avanceRapideToolStripMenuItem
            // 
            this.avanceRapideToolStripMenuItem.Name = "avanceRapideToolStripMenuItem";
            this.avanceRapideToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.avanceRapideToolStripMenuItem.Text = "Avance rapide";
            // 
            // enregistrementToolStripMenuItem
            // 
            this.enregistrementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.débutArrêtToolStripMenuItem});
            this.enregistrementToolStripMenuItem.Name = "enregistrementToolStripMenuItem";
            this.enregistrementToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.enregistrementToolStripMenuItem.Text = "Enregistrement";
            // 
            // débutArrêtToolStripMenuItem
            // 
            this.débutArrêtToolStripMenuItem.Name = "débutArrêtToolStripMenuItem";
            this.débutArrêtToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.débutArrêtToolStripMenuItem.Text = "Début/Arrêt";
            // 
            // traitementToolStripMenuItem
            // 
            this.traitementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.paramètresToolStripMenuItem});
            this.traitementToolStripMenuItem.Name = "traitementToolStripMenuItem";
            this.traitementToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.traitementToolStripMenuItem.Text = "Traitement";
            // 
            // paramètresToolStripMenuItem
            // 
            this.paramètresToolStripMenuItem.Name = "paramètresToolStripMenuItem";
            this.paramètresToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.paramètresToolStripMenuItem.Text = "Paramètres";
            this.paramètresToolStripMenuItem.Click += new System.EventHandler(this.paramètresToolStripMenuItem_Click);
            // 
            // playButton
            // 
            this.playButton.Image = global::VideoPlayer.Properties.Resources.play;
            this.playButton.Location = new System.Drawing.Point(172, 8);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(52, 52);
            this.playButton.TabIndex = 2;
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.lecturePauseToolStripMenuItem_Click);
            // 
            // rewindButton
            // 
            this.rewindButton.Image = global::VideoPlayer.Properties.Resources.rewind;
            this.rewindButton.Location = new System.Drawing.Point(104, 8);
            this.rewindButton.Name = "rewindButton";
            this.rewindButton.Size = new System.Drawing.Size(52, 52);
            this.rewindButton.TabIndex = 3;
            this.rewindButton.UseVisualStyleBackColor = true;
            // 
            // forwardButton
            // 
            this.forwardButton.Image = global::VideoPlayer.Properties.Resources.fast_forward;
            this.forwardButton.Location = new System.Drawing.Point(240, 8);
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(52, 52);
            this.forwardButton.TabIndex = 4;
            this.forwardButton.UseVisualStyleBackColor = true;
            // 
            // stopButton
            // 
            this.stopButton.Image = global::VideoPlayer.Properties.Resources.stop;
            this.stopButton.Location = new System.Drawing.Point(36, 8);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(52, 52);
            this.stopButton.TabIndex = 5;
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 403F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 478);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(750, 75);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.stopButton);
            this.panel1.Controls.Add(this.playButton);
            this.panel1.Controls.Add(this.reccordButton);
            this.panel1.Controls.Add(this.rewindButton);
            this.panel1.Controls.Add(this.forwardButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(176, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(397, 69);
            this.panel1.TabIndex = 9;
            // 
            // reccordButton
            // 
            this.reccordButton.Image = global::VideoPlayer.Properties.Resources.record;
            this.reccordButton.Location = new System.Drawing.Point(308, 8);
            this.reccordButton.Name = "reccordButton";
            this.reccordButton.Size = new System.Drawing.Size(52, 52);
            this.reccordButton.TabIndex = 6;
            this.reccordButton.UseVisualStyleBackColor = true;
            // 
            // framePictureBox
            // 
            this.framePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.framePictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.framePictureBox.Image = global::VideoPlayer.Properties.Resources._00763808_photo_extrait_de_les_simpson_le_film;
            this.framePictureBox.Location = new System.Drawing.Point(0, 24);
            this.framePictureBox.Name = "framePictureBox";
            this.framePictureBox.Size = new System.Drawing.Size(750, 454);
            this.framePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.framePictureBox.TabIndex = 0;
            this.framePictureBox.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "avi";
            this.openFileDialog1.Title = "Fichier vidéo";
            // 
            // DisplayVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 553);
            this.Controls.Add(this.framePictureBox);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DisplayVideo";
            this.Text = "nomVideo";
            this.Shown += new System.EventHandler(this.DisplayVideo_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.framePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox framePictureBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ouvrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fermerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lectureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lecturePauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arrêterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem rembobinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem avanceRapideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enregistrementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem traitementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem débutArrêtToolStripMenuItem;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button rewindButton;
        private System.Windows.Forms.Button forwardButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button reccordButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem sélectionnerFichierDenregistrementToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem paramètresToolStripMenuItem;
        public System.Windows.Forms.OpenFileDialog openFileDialog1;

    }
}

