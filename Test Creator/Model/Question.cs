﻿using System;
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
            Console.WriteLine("UPDATE QUESTION");
        }
    }
}
