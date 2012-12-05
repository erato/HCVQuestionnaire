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
    public partial class frmCESD : Form
    {
        private DataTable tQuestionAnswer;        
        private int iCurrentQuestion;
        //private DataTable tQuestionAnswer = new DataTable();
        private string sAnswer = "";
        private int iQuestionStart = 20;

        public frmCESD(DataTable dt)
        {
            InitializeComponent();
            tQuestionAnswer = dt;

            InitiateTable();

            btnBack.Visible = false;
            iCurrentQuestion = iQuestionStart;
            sAnswer = "";
            ShowQuestion();
        }

        
        private void btnBack_Click(object sender, EventArgs e)
        {
            //Save Answer 
            SaveAnswer();

            sAnswer = "";
            //Prompt for Last Question
            iCurrentQuestion--;

            if (iCurrentQuestion <= iQuestionStart)
            {
                iCurrentQuestion = iQuestionStart;
                btnBack.Visible = false;
            }

            btnNext.Text = "Next";
            ShowQuestion();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //Save Answer
            SaveAnswer();

            sAnswer = "";

            if (btnNext.Text == "Finish")
            {
                //this is the end of the survey.
                frmThankYou frmFinish = new frmThankYou();
                frmFinish.Show();
                this.Close();
            }
            else
            {
                //Prompt for Next Question
                iCurrentQuestion++;
                btnBack.Visible = true;

                if (iCurrentQuestion == tQuestionAnswer.Rows.Count)
                {
                    btnNext.Text = "Finish";

                }
                else if (iCurrentQuestion > tQuestionAnswer.Rows.Count)
                {
                    iCurrentQuestion = tQuestionAnswer.Rows.Count;
                }
                ShowQuestion();
            }
        }


        private void ShowQuestion()
        {            
            DataRow[] result = tQuestionAnswer.Select("QuestionNumber = " + iCurrentQuestion.ToString());

            if (result.Length > 0)
            {
                gbQuestionNumber.Text = "Question " + iCurrentQuestion.ToString();
                lblQuestion.Text = result[0]["Question"].ToString();
                string sAnswer = result[0]["Answer"].ToString();

                switch (sAnswer)
                {
                    case "Rare":
                        rbRare.Checked=true;
                        break;
                    case "Some":
                        rbSome.Checked = true;
                        break;
                    case "Occasionally":
                        rbOccasionally.Checked = true;
                        break;
                    case "Most":
                        rbMost.Checked = true;
                        break; 
                    default:
                        gbCESD.Controls.OfType<RadioButton>().ToList().ForEach(p => p.Checked = false);

                        //unchecked the form
                        break;
                }
            }            
        }

        private void SaveAnswer()
        {

            DataRow[] AnswerRow =
                tQuestionAnswer.Select("QuestionNumber = " + iCurrentQuestion.ToString());
            sAnswer = GetRadioSelection(gbCESD);       
           
            AnswerRow[0]["Answer"] = sAnswer;
        }

        /// <summary>
        /// This example method generates a DataTable.
        /// </summary>
        private void InitiateTable()
        {

            int i = iQuestionStart;
            tQuestionAnswer.Rows.Add(i++, "I was bothered by things that usually don't bother me", "CESD");
            tQuestionAnswer.Rows.Add(i++, "I did not feel like eating; my appetite was poor", "CESD");
            tQuestionAnswer.Rows.Add(i++, "I felt that I could not shake off the blues even with help from my family or friends", "CESD");
            tQuestionAnswer.Rows.Add(i++, "I felt that I was just as good as other people", "CESDR");
            tQuestionAnswer.Rows.Add(i++, "I had trouble keeping my mind on what I was doing", "CESD");
            tQuestionAnswer.Rows.Add(i++, "I felt depressed", "CESD");
            tQuestionAnswer.Rows.Add(i++, "I felt that everything I did was an effort", "CESD");
            tQuestionAnswer.Rows.Add(i++, "I felt hopeful about the future", "CESDR");
            tQuestionAnswer.Rows.Add(i++, "I thought my life had been a failure", "CESD");
            tQuestionAnswer.Rows.Add(i++, "I felt fearful", "CESD");
            tQuestionAnswer.Rows.Add(i++, "My sleep was restless", "CESD");
            tQuestionAnswer.Rows.Add(i++, "I was happy", "CESDR");
            tQuestionAnswer.Rows.Add(i++, "I talked less than usual", "CESD");
            tQuestionAnswer.Rows.Add(i++, "I felt lonely", "CESD");
            tQuestionAnswer.Rows.Add(i++, "People were unfriendly", "CESD");
            tQuestionAnswer.Rows.Add(i++, "I enjoyed life", "CESDR");
            tQuestionAnswer.Rows.Add(i++, "I had crying spells", "CESD");
            tQuestionAnswer.Rows.Add(i++, "I felt sad", "CESD");
            tQuestionAnswer.Rows.Add(i++, "I felt that people disliked me", "CESD");
            tQuestionAnswer.Rows.Add(i++, "I could not get \"going\"", "CESD");
            
        }

        public string GetRadioSelection(GroupBox gb)
        {
            foreach (Control c in gb.Controls)
            {
                RadioButton r = c as RadioButton;
                if (r != null && r.Checked) return r.Text;
            }
            return "";
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            // Initialize a new instance of the SpeechSynthesizer.
            SpeechSynthesizer synth = new SpeechSynthesizer();

            // Configure the audio output. 
            synth.SetOutputToDefaultAudioDevice();
            
            // Speak a string.
            synth.Speak(lblQuestion.Text);

        }

        
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox1 frmAbout = new AboutBox1();
            frmAbout.Show();
        }

    }
}
