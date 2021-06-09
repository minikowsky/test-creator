using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Test_Creator.ViewModel.BaseClass;

namespace Test_Creator.ViewModel
{
    class MainViewModel : BaseViewModel, ICloseable
    {
        
        private ICommand createTest;
        public ICommand CreateTest
        {
            get
            {
                return createTest ?? (createTest = new RelayCommand(
                    p => { 
                        var creatorWindow = new View.CreatorWindow();
                        creatorWindow.Show();
                        this.CloseWindow();
                    },
                    p => true));
            }
        }
        private ICommand editTest;
        public ICommand EditTest
        {
            get
            {
                return editTest ?? (editTest = new RelayCommand(
                    p => {
                        var fileDialog = new OpenFileDialog();
                        fileDialog.DefaultExt = ".png";
                        fileDialog.Filter = "Text Files (*.txt)|*.txt";
                        Nullable<bool> result = fileDialog.ShowDialog();
                        if (result is true)
                        {
                            string text = File.ReadAllText(fileDialog.FileName);
                            if (isFileCorrect(text))
                            {
                                var creatorWindow = new View.CreatorWindow();
                                //creatorWindow.creatorViewModel.TestFile = text;
                                creatorWindow.creatorViewModel.LoadTestToEdit(text);
                                creatorWindow.Show();
                                this.CloseWindow();
                            }
                            else
                            {
                                MessageBox.Show("File is incorrect!");
                            }
                        }
                    },
                    p => true));
            }
        }
        private bool isFileCorrect(string text)
        {
            string[] lines = Regex.Split(text,"[\r\n]+");
            if ((lines.Length - 1) % 6 != 0) return false;
            for (int i = 1; i < lines.Length; i+= 6)
            {
                int correctAnswers = 0;
                if (lines[i].Length < 2 || lines[i][lines[i].Length - 1] != '?') return false;
                for (int j = 1; j <= 4; j++)
                {
                    if (lines[i + j].Length < 3) return false;
                    string s = lines[i + j].Substring(0, 2);
                    if (s != "0|" && s != "1|") return false;
                    if (s[0] == '1') correctAnswers++;
                }
                if (correctAnswers != 1) return false;
                if (lines[i + 5] != "**********") return false;

            }
            return true;
        }
        public Action Close { get; set; }
        private void CloseWindow()
        {
            Close?.Invoke();
        }
    }
    interface ICloseable
    {
        Action Close { get; set; }
    }
}
