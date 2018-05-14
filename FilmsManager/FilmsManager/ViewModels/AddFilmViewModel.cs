using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FilmsManager.ViewModels
{
    class AddFilmViewModel
    {
        public ICommand AddCommand { get; set; }

        public AddFilmViewModel()
        {
            AddCommand = new AddCommand();
        }

    }

    internal class AddCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            
        }
    }
}
