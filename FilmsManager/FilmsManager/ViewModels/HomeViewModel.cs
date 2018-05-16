using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FilmsManager.ViewModels
{
    public class HomeViewModel : BaseViewModel
	{
        public ObservableCollection<MovieModel> MovieList { get; set; } = new ObservableCollection<MovieModel>();
        public ICommand NavigateCommand { get; set; }

        public HomeViewModel ()
		{
            NavigateCommand = new NavigateCommand(MovieList);
        }

        //public void AddMovie(MovieModel addingMovie)
        //{
        //    MovieList.Add(addingMovie);
        //}
    }

    internal class NavigateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        INavigationService _navigationService = App.NavigationService;
        private ObservableCollection<MovieModel> MovieList;

        public NavigateCommand(ObservableCollection<MovieModel> movieList)
        {
            MovieList= movieList;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _navigationService.NavigateAsync("AddFilmPage", MovieList);
        }
    }
}