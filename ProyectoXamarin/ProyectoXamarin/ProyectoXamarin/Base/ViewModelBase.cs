using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ProyectoXamarin.Base
{
    public class ViewModelBase : INotifyPropertyChanged
    {
      
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
