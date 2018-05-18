using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FilmsManager.ViewModels
{
    public class HomeViewModel : BaseViewModel
	{
        public ObservableCollection<MovieModel> MovieList { get; set; } = new ObservableCollection<MovieModel>();
        public ICommand NavigateCommand { get; set; }

        public HomeViewModel ()
		{
            NavigateCommand = new NavigateCommand(NavigationService, MovieList);
        }

        //public void AddMovie(MovieModel addingMovie)
        //{
        //    MovieList.Add(addingMovie);
        //}
    }

    internal class NavigateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly INavigationService _navigationService;
        private ObservableCollection<MovieModel> _movieList;

        public NavigateCommand(INavigationService navigationService, ObservableCollection<MovieModel> movieList)
        {
            _navigationService = navigationService;
            _movieList= movieList;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _navigationService.NavigateAsync("AddFilmPage", _movieList);
        }
    }
}