using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FilmsManager.ViewModels
{
    public class PropertyChangedImpl : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
