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
using Test_Creator.Model;
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
                            if (TestFile.IsFileCorrect(text))
                            {
                                var creatorWindow = new View.CreatorWindow();
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
