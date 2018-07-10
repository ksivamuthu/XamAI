using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XamAI.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {        
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}