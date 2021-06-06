using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Creator.Model;

namespace Test_Creator.ViewModel
{
    using BaseClass;
    using System.Windows.Input;

    class CreatorViewModel : BaseViewModel
    {
        private List<Model.Question> questions;
        public CreatorViewModel()
        {
            questions.Add(new Model.Question());
        }

        #region Test to edit
        // Test to edit
        private string testFile;
        public string TestFile { 
            set 
            {
                testFile = value;
                loadTestToEdit(testFile);
            } }
        private void loadTestToEdit(string tf)
        {
            //TODO:

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
                    string text = testToText();
                }, p => isCompleted()));
            }
        }
        private bool isCompleted()
        {
            //checks if all questions have answers and correctanswers   
            
            return false;
        }
        private string testToText()
        {
            //TODO:
            return "";
        }
        #endregion  

        # region Question control
        private string question, answer1, answer2, answer3, answer4;
        public string Question { get => question;
            set
            {
                question = value;
                onPropertyChanged(nameof(Question));
                saveToModel();
                
            }
        }
        public string Answer1 { get => answer1;
            set
            {
                answer1 = value;
                onPropertyChanged(nameof(Answer1));
                saveToModel();
            }
        }
        public string Answer2
        {
            get => answer2;
            set
            {
                answer2 = value;
                onPropertyChanged(nameof(Answer2));
                saveToModel();
            }
        }
        public string Answer3
        {
            get => answer3;
            set
            {
                answer3 = value;
                onPropertyChanged(nameof(Answer3));
                saveToModel();
            }
        }
        public string Answer4
        {
            get => answer4;
            set
            {
                answer4 = value;
                onPropertyChanged(nameof(Answer4));
                saveToModel();
            }
        }
        private int correct;
        private ICommand correctAnswer;
        public ICommand CorrectAnswer
        {
            get
            {
                return correctAnswer ?? (correctAnswer = new RelayCommand(
                    p => { 
                        correct = Int32.Parse(p.ToString());
                        saveToModel();
                    }, 
                    p => true));
            }
        }
        private void saveToModel()
        {
            string[] answers = new string[4] { answer1, answer2, answer3, answer4 };

            questions[current].UpdateData(question, answers, correct);
        }
        private void loadQuestion()
        {
            question = questions[current].QuestionContent;
            answer1 = questions[current].Answers[0];
            answer2 = questions[current].Answers[1];
            answer3 = questions[current].Answers[2];
            answer4 = questions[current].Answers[3];

        }
        #endregion

        #region Title and question number
        //Title
        private string testTitle;
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
        //Delete button
        private ICommand delete;
        public ICommand Delete
        {
            get
            {
                return delete ?? (delete = new RelayCommand(p =>
                {
                    Console.WriteLine("delete");

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
                    Console.WriteLine("previous");
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
                    Console.WriteLine(question);
                    Console.WriteLine(answer1);
                    Console.WriteLine(answer2);
                    Console.WriteLine(answer3);
                    Console.WriteLine(answer4);
                    Console.WriteLine(correct);
                    current++;
                    if (current > total)
                        total++;
                    else
                        loadQuestion();
                    onPropertyChanged(nameof(QuestionNumber));
                }, p => true));
            }
        }
        #endregion
    }
}
