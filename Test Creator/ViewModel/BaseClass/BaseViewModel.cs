using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Creator.ViewModel.BaseClass
{
    // this interface is responsible for notifying view about the changes from model
    class BaseViewModel : INotifyPropertyChanged
    {
        //this is delegate
        public event PropertyChangedEventHandler PropertyChanged;

        // this method let the view know that data from viewmodel layer has changed
        public void onPropertyChanged(params string[] properties)
        {
            if (PropertyChanged != null)
                foreach (var property in properties)
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
