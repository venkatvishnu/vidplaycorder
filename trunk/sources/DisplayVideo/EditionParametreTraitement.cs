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
    public partial class EditionParametreTraitement : Form
    {
        public EditionParametreTraitement()
        {
            InitializeComponent();

            var traitement = Traitement.Instance;
            contrasteNumericUpDown.Value = Convert.ToDecimal(traitement.Contraste);
            brillanceNumericUpDown.Value = Convert.ToDecimal(traitement.Brillance);

            numericUpDown1.Value = Convert.ToDecimal(traitement.Convolution[0]);
            numericUpDown2.Value = Convert.ToDecimal(traitement.Convolution[1]);
            numericUpDown3.Value = Convert.ToDecimal(traitement.Convolution[2]);
            numericUpDown4.Value = Convert.ToDecimal(traitement.Convolution[3]);
            numericUpDown5.Value = Convert.ToDecimal(traitement.Convolution[4]);
            numericUpDown6.Value = Convert.ToDecimal(traitement.Convolution[5]);
            numericUpDown7.Value = Convert.ToDecimal(traitement.Convolution[6]);
            numericUpDown8.Value = Convert.ToDecimal(traitement.Convolution[7]);
            numericUpDown9.Value = Convert.ToDecimal(traitement.Convolution[8]);
        }

        private void UpdateValue()
        {
            
            var traitement = Traitement.Instance;
            traitement.Contraste = Convert.ToDouble(contrasteNumericUpDown.Value);
            traitement.Brillance = Convert.ToInt32(contrasteNumericUpDown.Value);

            traitement.Convolution = new int[]
                                         {
                                             Convert.ToInt32(numericUpDown1.Value),
                                             Convert.ToInt32(numericUpDown2.Value),
                                             Convert.ToInt32(numericUpDown3.Value),
                                             Convert.ToInt32(numericUpDown4.Value),
                                             Convert.ToInt32(numericUpDown5.Value),
                                             Convert.ToInt32(numericUpDown6.Value),
                                             Convert.ToInt32(numericUpDown7.Value),
                                             Convert.ToInt32(numericUpDown8.Value),
                                             Convert.ToInt32(numericUpDown9.Value)
                                         };
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            UpdateValue();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void buttonAppliquer_Click(object sender, EventArgs e)
        {
            UpdateValue();
        }
    }
}
