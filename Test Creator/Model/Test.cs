using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test_Creator.Model
{
    class Test
    {
        private List<Model.Question> questions = new List<Question>();
        public string Title { get; set; }
        public void AddQuestion(Model.Question question)
        {
            questions.Add(question);
        }
        public Question GetQuestion(int index)
        {
            if (index >= 0 && index < questions.Count)
                return questions[index];
            return null;
        }
        public bool RemoveQuestion(int index)
        {
            if(index >= 0 && index < questions.Count)
            {
                questions.RemoveAt(index);
                return true;
            }
            return false;
        }
        public int GetNumberOfQuestions() { return questions.Count; }
        public void Load(string t)
        {
            questions = new List<Question>();
            string[] lines = Regex.Split(t, "[\r\n]+");
            Title = lines[0];
            for (int i = 1; i < lines.Length; i += 6)
            {
                string[] a = new string[4] { "", "", "", "" };
                Model.Question question = new Question();
                int correctAnswer = 0;
                for (int j = 1; j <= 4; j++)
                {
                    if (lines[i + j][0] == '1') correctAnswer = j;
                    a[j - 1] = lines[i + j].Substring(2);
                }
                question.UpdateData(lines[i], a, correctAnswer);
                this.AddQuestion(question);
            }
        }
    }
}
