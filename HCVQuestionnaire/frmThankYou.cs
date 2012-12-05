using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Windows.Forms;

namespace HCVQuestionnaire
{
    public partial class frmThankYou : Form
    {
        public frmThankYou()
        {
            InitializeComponent();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            this.Close();
            System.Windows.Forms.Application.Exit();

        }

        private void frmThankYou_Load(object sender, EventArgs e)
        {
            // Initialize a new instance of the SpeechSynthesizer.
            SpeechSynthesizer synth = new SpeechSynthesizer();

            // Configure the audio output. 
            synth.SetOutputToDefaultAudioDevice();

            // Speak a string.
            synth.Speak(lblQuestion.Text);
        }

    }
}
