using FilmsManager.Models;
using System;
using System.Windows.Input;

namespace FilmsManager.ViewModels
{
    class AddFilmViewModel
    {
        public ICommand AddCommand { get; set; }
        public string MovieTitle;
        public string MovieGenre;
        public string MovieImage;

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
            new MovieModel();
        }
    }
}
