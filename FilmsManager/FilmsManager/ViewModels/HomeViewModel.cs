using FilmsManager.Models;
using FilmsManager.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FilmsManager.ViewModels
{
    public class HomeViewModel : BaseViewModel
	{
        public static ObservableCollection<MovieModel> MovieList { get; set; } = new ObservableCollection<MovieModel>();
        public ICommand NavigateCommand { get; set; }

        public HomeViewModel ()
		{
            NavigateCommand = new NavigateCommand();
        }

        public static void AddMovie(MovieModel addingMovie)
        {
            MovieList.Add(addingMovie);
        }
    }

    internal class NavigateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            new HomePage().OnAddButtonPressed();
        }
    }
}