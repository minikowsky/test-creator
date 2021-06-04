using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Creator.ViewModel
{
    using BaseClass;
    using System.Windows.Input;

    class CreatorViewModel : BaseViewModel
    {
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

        //Title
        private string testTitle;
        public string TestTitle { get => testTitle; 
            set {
                testTitle = value;
                onPropertyChanged(nameof(TestTitle)); 
            } }
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
            return false;
        }
        private string testToText()
        {
            //TODO:
            return "";
        }
        //Question control



        //Question number
        private int current = 0, total = 0;
        public string QuestionNumber { get => $"{current+1}/{total+1}";}
        //Delete button
        private ICommand delete;
        public ICommand Delete
        {
            get
            {
                return delete ?? (delete = new RelayCommand(p => 
                {
                    Console.WriteLine("delete"); 

                },p =>
                {
                    if (total == 0) return false;
                    return true;
                }));
            }
        }
        //Navigation buttons
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
                    Console.WriteLine("next");
                    current++;
                    if (current > total) total++;
                    onPropertyChanged(nameof(QuestionNumber));
                }, p => true));
            }
        }
    }
}
