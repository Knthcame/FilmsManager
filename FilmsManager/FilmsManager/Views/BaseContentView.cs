using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace FilmsManager.Views
{
    public class BaseContentView : ContentView, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChangedEvent;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                return;
            PropertyChangedEvent?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
