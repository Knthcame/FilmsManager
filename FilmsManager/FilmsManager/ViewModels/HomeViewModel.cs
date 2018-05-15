using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FilmsManager.ViewModels
{
    public class HomeViewModel : BaseViewModel
	{
        public static ObservableCollection<MovieModel> MovieList { get; set; } = new ObservableCollection<MovieModel>();
        public ICommand NavigateCommand { get; set; }
        INavigationService _navigationService;

        public HomeViewModel (INavigationService NavigationService)
		{
            _navigationService = NavigationService;
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
            
        }
    }
}