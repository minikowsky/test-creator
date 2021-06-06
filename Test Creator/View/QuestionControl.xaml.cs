using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test_Creator.View
{
    /// <summary>
    /// Interaction logic for QuestionControl.xaml
    /// </summary>
    public partial class QuestionControl : UserControl
    {
        public QuestionControl()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty QuestionProperty =
            DependencyProperty.Register(nameof(Question), typeof(string), typeof(QuestionControl));
        public string Question
        {
            get => (string)GetValue(QuestionProperty);
            set => SetValue(QuestionProperty, value);
        }

        //Answer 1
        public static readonly DependencyProperty Answer1Property =
            DependencyProperty.Register(nameof(Answer1), typeof(string), typeof(QuestionControl));
        public string Answer1
        {
            get => (string)GetValue(Answer1Property);
            set => SetValue(Answer1Property, value);
        }

        //Answer 2
        public static readonly DependencyProperty Answer2Property =
            DependencyProperty.Register(nameof(Answer2), typeof(string), typeof(QuestionControl));
        public string Answer2
        {
            get => (string)GetValue(Answer2Property);
            set => SetValue(Answer2Property, value);
        }

        //Answer 3
        public static readonly DependencyProperty Answer3Property =
            DependencyProperty.Register(nameof(Answer3), typeof(string), typeof(QuestionControl));
        public string Answer3
        {
            get => (string)GetValue(Answer3Property);
            set => SetValue(Answer3Property, value);
        }

        //Answer 4
        public static readonly DependencyProperty Answer4Property =
            DependencyProperty.Register(nameof(Answer4), typeof(string), typeof(QuestionControl));
        public string Answer4
        {
            get => (string)GetValue(Answer4Property);
            set => SetValue(Answer4Property, value);
        }

        //Radio Buttons
        public static readonly DependencyProperty RadioButton1Property =
           DependencyProperty.Register(nameof(RadioButton1), typeof(bool), typeof(QuestionControl));
        public bool RadioButton1
        {
            get => (bool)GetValue(RadioButton1Property);
            set => SetValue(RadioButton1Property, value);
        }
        public static readonly DependencyProperty RadioButton2Property =
           DependencyProperty.Register(nameof(RadioButton2), typeof(bool), typeof(QuestionControl));
        public bool RadioButton2
        {
            get => (bool)GetValue(RadioButton2Property);
            set => SetValue(RadioButton2Property, value);
        }
        public static readonly DependencyProperty RadioButton3Property =
           DependencyProperty.Register(nameof(RadioButton3), typeof(bool), typeof(QuestionControl));
        public bool RadioButton3
        {
            get => (bool)GetValue(RadioButton3Property);
            set => SetValue(RadioButton3Property, value);
        }
        public static readonly DependencyProperty RadioButton4Property =
           DependencyProperty.Register(nameof(RadioButton4), typeof(bool), typeof(QuestionControl));
        public bool RadioButton4
        {
            get => (bool)GetValue(RadioButton1Property);
            set => SetValue(RadioButton1Property, value);
        }
    }
}
