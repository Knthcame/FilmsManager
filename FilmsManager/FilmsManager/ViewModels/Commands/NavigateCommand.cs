using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FilmsManager.ViewModels.Commands
{
    public class NavigateCommand : ICommand
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
            _navigationService.NavigateAsync("AddFilmPage");
        }
    }
}