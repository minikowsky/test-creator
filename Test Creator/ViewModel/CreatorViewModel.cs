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
        private Test test = new Test();
        public CreatorViewModel()
        {
            test.AddQuestion(new Model.Question());
            loadQuestion();
        }

        #region Test to edit
        // Test to edit
        public void LoadTestToEdit(string t)
        {
            test.Load(t);
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
                    saveQuestion();
                    if (test.IsCompleted())
                    {
                        string text = test.ToString();
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
        public string Answer1 { 
            get => answer1;
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
        private void saveQuestion()
        {
            string[] answers = new string[4] { answer1, answer2, answer3, answer4 };

            test.GetQuestion(current).UpdateData(question, answers, correct);
        }
        private void loadQuestion()
        {
            Question = test.GetQuestion(current).QuestionContent;
            Answer1 = test.GetQuestion(current).Answers[0];
            Answer2 = test.GetQuestion(current).Answers[1];
            Answer3 = test.GetQuestion(current).Answers[2];
            Answer4 = test.GetQuestion(current).Answers[3];
            RadioButton1 = false;
            RadioButton2 = false;
            RadioButton3 = false;
            RadioButton4 = false;
            switch (test.GetQuestion(current).CorrectAnswer)
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
        public string TestTitle
        {
            get => test.Title;
            set
            {
                test.Title = value;
                onPropertyChanged(nameof(TestTitle));
            }
        }
        //Question number
        private int current = 0;
        public string QuestionNumber { get => $"{current+1}/{test.GetNumberOfQuestions()}";}
        #endregion

        #region Navigation and delete
        private ICommand delete;
        public ICommand Delete
        {
            get
            {
                return delete ?? (delete = new RelayCommand(p =>
                {
                    test.RemoveQuestion(current);
                    if (current > test.GetNumberOfQuestions()-1)
                        current--;
                    
                    loadQuestion();
                    onPropertyChanged(nameof(QuestionNumber));

                }, p =>
                {
                    if (test.GetNumberOfQuestions() == 0) return false;
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
                    saveQuestion();
                    current--;
                    loadQuestion();
                    onPropertyChanged(nameof(QuestionNumber));
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
                    saveQuestion();
                    current++;
                    if (current > test.GetNumberOfQuestions() - 1)
                    {
                        test.AddQuestion(new Model.Question());
                    }
                    loadQuestion();
                    onPropertyChanged(nameof(QuestionNumber));
                }, p => true));
            }
        }
        #endregion
    }
}
