using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Creator.Model;

namespace Test_Creator.ViewModel
{
    using BaseClass;
    using Microsoft.Win32;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Input;

    class CreatorViewModel : BaseViewModel
    {
        private List<Model.Question> questions = new List<Question>();
        public CreatorViewModel()
        {
            questions.Add(new Model.Question());
            loadQuestion();
        }

        #region Test to edit
        // Test to edit
        private string testFile="";
        public string TestFile { 
            set 
            {
                testFile = value;
                loadTestToEdit(testFile);
            } }
        private void loadTestToEdit(string tf)
        {
            questions = new List<Question>();
            string[] lines = Regex.Split(tf, "[\r\n]+");
            TestTitle = lines[0];
            total = -1;
            for (int i = 1; i < lines.Length; i += 6)
            {
                string[] a = new string[4] { "", "", "", "" };
                questions.Add(new Model.Question());
                int correctAnswer = 0;
                for (int j = 1; j <= 4; j++)
                {
                    if (lines[i + j][0] == '1') correctAnswer = j;
                    a[j - 1] = lines[i + j].Substring(2);
                }
                questions[questions.Count - 1].UpdateData(lines[i], a, correctAnswer);
                total++;
            }
            loadQuestion();
            onPropertyChanged(nameof(QuestionNumber));

        }
        #endregion
        
        #region Save to file

        //Save
        private ICommand save;
        public ICommand Save
        {
            get
            {
                return save ?? (save = new RelayCommand(p =>
                {
                    if (isCompleted())
                    {
                        string text = testToText();
                        SaveFileDialog saveFileDialog = new SaveFileDialog()
                        {
                            Filter = "Text Files(*.txt)|*.txt"
                        };
                        if(saveFileDialog.ShowDialog()==true)
                        {
                            File.WriteAllText(saveFileDialog.FileName, text);
                        }
                    }
                    else
                        MessageBox.Show("Test is incomplete or incorrect!","Error");
                    
                }, p => true));
            }
        }
        private bool isCompleted()
        {
            saveToModel();
            //checks if all questions have answers and correctanswers 
            bool toReturn = true; 
            if (testTitle.Equals("Test Title")|| testTitle.Equals("")) toReturn=false;
            for (int i = 0; i <= total; i++)
            {
                if (questions[i].QuestionContent.Equals("Question")|| questions[i].QuestionContent[questions[i].QuestionContent.Length-1]!='?') toReturn = false;
                for (int j = 0; j < 4; j++)
                {
                    if (questions[i].Answers[j].Equals("Answer " + (j + 1).ToString()) || questions[i].Answers[j].Equals("")) toReturn = false;
                }
                if (questions[i].CorrectAnswer == 0) toReturn = false;
                
            }
            return toReturn;
        }
        private string testToText()
        {
            string text = testTitle;
            for (int i = 0; i <= total; i++)
            {
                text += "\n"+ questions[i].ToString();
            }
            return text;
        }
        #endregion  

        # region Question control
        private string question, answer1, answer2, answer3, answer4;
        private bool radioButton1, radioButton2, radioButton3, radioButton4;
        public string Question { get => question;
            set
            {
                question = value;
                onPropertyChanged(nameof(Question));
            }
        }
        public string Answer1 { get => answer1;
            set
            {
                answer1 = value;
                onPropertyChanged(nameof(Answer1));
            }
        }
        public string Answer2
        {
            get => answer2;
            set
            {
                answer2 = value;
                onPropertyChanged(nameof(Answer2));
            }
        }
        public string Answer3
        {
            get => answer3;
            set
            {
                answer3 = value;
                onPropertyChanged(nameof(Answer3));
            }
        }
        public string Answer4
        {
            get => answer4;
            set
            {
                answer4 = value;
                onPropertyChanged(nameof(Answer4));
            }
        }
        private int correct;
        public bool RadioButton1
        {
            get => radioButton1;
            set
            {
                radioButton1 = value;
                onPropertyChanged(nameof(RadioButton1));
                correct = 1;
            }
        }
        public bool RadioButton2
        {
            get => radioButton2;
            set
            {
                radioButton2 = value;
                onPropertyChanged(nameof(RadioButton2));
                correct = 2;
            }
        }
        public bool RadioButton3
        {
            get => radioButton3;
            set
            {
                radioButton3 = value;
                onPropertyChanged(nameof(RadioButton3));
                correct = 3;
            }
        }
        public bool RadioButton4
        {
            get => radioButton4;
            set
            {
                radioButton4 = value;
                onPropertyChanged(nameof(RadioButton4));
                correct = 4;
            }
        }
        private void saveToModel()
        {
            string[] answers = new string[4] { answer1, answer2, answer3, answer4 };

            questions[current].UpdateData(question, answers, correct);
        }
        private void loadQuestion()
        {
            Question = questions[current].QuestionContent;
            Answer1 = questions[current].Answers[0];
            Answer2 = questions[current].Answers[1];
            Answer3 = questions[current].Answers[2];
            Answer4 = questions[current].Answers[3];
            RadioButton1 = false;
            RadioButton2 = false;
            RadioButton3 = false;
            RadioButton4 = false;
            switch (questions[current].CorrectAnswer)
            {
                case 1:
                    RadioButton1 = true;
                    break;
                case 2:
                    RadioButton2 = true;
                    break;
                case 3:
                    RadioButton3 = true;
                    break;
                case 4:
                    RadioButton4 = true;
                    break;
                default:
                    correct = 0;
                    break;
            }
        }
        #endregion

        #region Title and question number
        //Title
        private string testTitle = "Test Title";
        public string TestTitle
        {
            get => testTitle;
            set
            {
                testTitle = value;
                onPropertyChanged(nameof(TestTitle));
            }
        }
        //Question number
        private int current = 0, total = 0;
        public string QuestionNumber { get => $"{current+1}/{total+1}";}
        #endregion

        #region Navigation and delete
        private ICommand delete;
        public ICommand Delete
        {
            get
            {
                return delete ?? (delete = new RelayCommand(p =>
                {
                    questions.RemoveAt(current);
                    if (current == total) current--;
                    total--;
                    
                    loadQuestion();
                    onPropertyChanged(nameof(QuestionNumber));

                }, p =>
                {
                    if (total == 0) return false;
                    return true;
                }));
            }
        }

        private ICommand previous;
        public ICommand Previous
        {
            get
            {
                return previous ?? (previous = new RelayCommand(p =>
                {
                    saveToModel();
                    current--;
                    onPropertyChanged(nameof(QuestionNumber));
                    loadQuestion();
                }, p => 
                { 
                    if (current == 0) return false;
                    return true;          
                }));
            }
        }

        private ICommand next;
        public ICommand Next
        {
            get
            {
                return next ?? (next = new RelayCommand(p =>
                {
                    saveToModel();
                    Console.WriteLine(question);
                    Console.WriteLine(answer1);
                    Console.WriteLine(answer2);
                    Console.WriteLine(answer3);
                    Console.WriteLine(answer4);
                    Console.WriteLine(correct);
                    current++;
                    if (current > total)
                    {
                        total++;
                        questions.Add(new Model.Question());
                    }
                    loadQuestion();
                    onPropertyChanged(nameof(QuestionNumber));
                }, p => true));
            }
        }
        #endregion
    }
}
