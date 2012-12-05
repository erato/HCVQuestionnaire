using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;

namespace HCVQuestionnaire
{
    public partial class frmQ1 : Form
    {        
        private int iCurrentQuestion;
        private DataTable tQuestionAnswer = new DataTable();
        private string sAnswer = "";
        private int iQuestionStart = 1;

        public frmQ1()
        {
            InitializeComponent();

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
            //Prompt for Next Question
            iCurrentQuestion++;
            btnBack.Visible = true;

            if (iCurrentQuestion == tQuestionAnswer.Rows.Count)
            {
                btnNext.Text = "Start CESD";
             
            }
            else if (iCurrentQuestion > tQuestionAnswer.Rows.Count)
            {
                frmCESD frmNext = new frmCESD(tQuestionAnswer);
                frmNext.Show();

                this.Hide();

            }
            
            ShowQuestion();        
        }


        private void ShowQuestion()
        {
            DataRow[] result = tQuestionAnswer.Select("QuestionNumber = " + iCurrentQuestion.ToString());

            if (result.Length > 0)
            {
                gbQuestionNumber.Text = "Question " + iCurrentQuestion.ToString();
                lblQuestion.Text = result[0]["Question"].ToString();
                string sQuestionType = result[0]["QuestionType"].ToString();
                switch (sQuestionType)
                {
                    case "YesNo":
                        gbYesNo.Visible = true;
                        gbHighLow.Visible = false;
                        gb654321.Visible = false;
                        gbDays.Visible = false;
                        break;
                    case "HighLow":
                        gbHighLow.Visible = true;
                        gbYesNo.Visible = false;
                        gb654321.Visible = false;
                        gbDays.Visible = false;
                        break;
                    case "654321":
                        gbHighLow.Visible = false;
                        gbYesNo.Visible = false;
                        gbDays.Visible = false;
                        gb654321.Visible = true;
                        break;
                    case "Days":
                        gb654321.Visible = false;
                        gbDays.Visible = true;
                        gbHighLow.Visible = true;
                        gbYesNo.Visible = false;
                        break;
                }

                string sAnswer = result[0]["Answer"].ToString();

                switch (sAnswer)
                {
                    case "Yes":
                        rbYes.Checked=true;
                        break;
                    case "No":
                        rbNo.Checked = true;
                        break;
                    case "High":
                        rbHigh.Checked = true;
                        break;
                    case "Low":
                        rbLow.Checked = true;
                        break;
                    case "Moderate":
                        rbModerate.Checked= true;
                        break;
                    case "Stable":
                        rbStable.Checked=true;
                        break;
                    case "1":
                        rb1.Checked = true;
                        break;
                    case "2":
                        rb2.Checked = true;
                        break;
                    case "3":
                        rb3.Checked=true;
                        break;
                    case "4":
                        rb4.Checked=true;
                        break;
                    case "5":
                        rb5.Checked=true;
                        break;
                    case "6":
                       rb6.Checked = true;
                        break;
                    case "Monday":
                        rbMonday.Checked = true;
                        break;
                    case "Tuesday":
                        rbTuesday.Checked = true;
                        break;
                    case "Wednesday":
                        rbWednesday.Checked = true;
                        break;
                    case "Thursday":
                        rbThursday.Checked=true;
                        break;
                    case "Friday":
                        rbFriday.Checked=true;
                        break;
                    case "Saturday":
                        rbSaturday.Checked=true;
                        break;
                    case "Sunday":
                        rbSunday.Checked=true;
                        break;
                    default:
                        gbHighLow.Controls.OfType<RadioButton>().ToList().ForEach(p => p.Checked = false);
                        gbYesNo.Controls.OfType<RadioButton>().ToList().ForEach(p => p.Checked = false);
                        gb654321.Controls.OfType<RadioButton>().ToList().ForEach(p => p.Checked = false);
                        gbDays.Controls.OfType<RadioButton>().ToList().ForEach(p => p.Checked = false);
                        //unchecked the form
                        break;
                }
            }            
            
        }

        private void SaveAnswer()
        {

            DataRow[] AnswerRow =
                tQuestionAnswer.Select("QuestionNumber = " + iCurrentQuestion.ToString());

            string sQuestionType = AnswerRow[0]["QuestionType"].ToString();
            switch (sQuestionType)
            {
                case "YesNo":
                    sAnswer = GetRadioSelection(gbYesNo);
                    break;
                case "HighLow":
                    sAnswer = GetRadioSelection(gbHighLow);
                    break;
                case "Days":
                    sAnswer = GetRadioSelection(gbDays);
                    break;
                case "654321":
                    sAnswer = GetRadioSelection(gb654321);
                    break;
            }
           
            AnswerRow[0]["Answer"] = sAnswer;
        }

        /// <summary>
        /// This example method generates a DataTable.
        /// </summary>
        private void InitiateTable()
        {
            //
            // Here we create a DataTable with four columns.
            //
            
            tQuestionAnswer.Columns.Add("QuestionNumber", typeof(int));
            tQuestionAnswer.Columns.Add("Question", typeof(string));
            tQuestionAnswer.Columns.Add("QuestionType", typeof(string));
            tQuestionAnswer.Columns.Add("Answer", typeof(string));

            int i = 1;
            tQuestionAnswer.Rows.Add(i++, "Have you missed any dose of your medicine?", "YesNo");
            tQuestionAnswer.Rows.Add(i++, "Have you missed more than 1 day of work since the last visit?", "YesNo");
            tQuestionAnswer.Rows.Add(i++, "Do you have any problems or signs of infection where you got the shot?", "YesNo");
            tQuestionAnswer.Rows.Add(i++, "Do you have pain in your bottom/rectum?", "YesNo");
            tQuestionAnswer.Rows.Add(i++, "Are you concerned about your energy level or fatigue?", "YesNo");
            tQuestionAnswer.Rows.Add(i++, "Are  you drinking alcohol?", "YesNo");
            tQuestionAnswer.Rows.Add(i++, "Do you have a rash?", "YesNo");
            tQuestionAnswer.Rows.Add(i++, "How is your stress level?", "HighLow");
            tQuestionAnswer.Rows.Add(i++, "Have you lost weight?", "YesNo");
            tQuestionAnswer.Rows.Add(i++, "Are you vomiting?", "YesNo");
            tQuestionAnswer.Rows.Add(i++, "Do you feel nausea or sick at your stomach?", "YesNo");
            tQuestionAnswer.Rows.Add(i++, "Are you taking any new medication?", "YesNo");
            tQuestionAnswer.Rows.Add(i++, "Are your bowels regular?", "YesNo");
            tQuestionAnswer.Rows.Add(i++, "Are you having breathing problems?", "YesNo");
            tQuestionAnswer.Rows.Add(i++, "Do you have chest pain?", "YesNo");
            tQuestionAnswer.Rows.Add(i++, "How many Ribovirin pills do you take each day?", "654321");
            tQuestionAnswer.Rows.Add(i++, "What day do you take Pegasys?", "Days");
            tQuestionAnswer.Rows.Add(i++, "Are you taking Incivek pills every 7-9 hours?", "YesNo");
            tQuestionAnswer.Rows.Add(i++, "Do you have new concerns to talk about today?", "YesNo");


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

        private void About_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
            AboutBox1 frmAbout = new AboutBox1();
            frmAbout.Show();
        }

    }
}
