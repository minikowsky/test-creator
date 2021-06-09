using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Creator.Model
{
    class Question
    {
        private string questionContent;
        public string QuestionContent { get => questionContent; }
        
        private string[] answers;
        public string[] Answers { get => answers; }

        private int correctAnswer;
        public int CorrectAnswer { get => correctAnswer; }

        public Question(){
            this.questionContent = "Question";
            this.answers = new string[4] { "Answer 1", "Answer 2", "Answer 3", "Answer 4" };
            this.correctAnswer = 0;
        }

        public void UpdateData(string questionContent, string[] answers, int correctAnswer)
        {
            this.questionContent = questionContent;
            this.answers = answers;
            this.correctAnswer = correctAnswer;
        }
        public override string ToString()
        {
            string s = questionContent + "\n";
            for(int i = 0; i < 4; i++)
            {
                if (correctAnswer == i+1) s += "1|";
                else s += "0|";
                s += answers[i] + "\n";
            }
            s += "**********";
            return s;
        }
        public bool HasAllValues()
        {
            if (questionContent.Equals("Question") || questionContent[questionContent.Length - 1] != '?') return false;
            for (int j = 0; j < 4; j++)
            {
                if (answers[j].Equals("Answer " + (j + 1).ToString()) || answers[j].Equals("")) return false;
            }
            if (correctAnswer == 0) return false;
            return true;
        }
    }
}
