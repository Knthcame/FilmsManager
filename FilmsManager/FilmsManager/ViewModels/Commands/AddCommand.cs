using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FilmsManager.ViewModels.Commands
{
    public class AddCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly INavigationService _navigationService;
        private readonly AddFilmViewModel _viewModel;
        private ObservableCollection<MovieModel> _movieList;

        public AddCommand(INavigationService navigationService, AddFilmViewModel viewModel, ObservableCollection<MovieModel> MovieList)
        {
            _navigationService = navigationService;
            _viewModel = viewModel;
            _movieList = MovieList;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (_viewModel.MovieTitle == null | _viewModel.MovieGenre == null)
            {
				MessagingCenter.Send<AddCommand>(this, "Incomplete");
                return;
            }
            _movieList.Add(new MovieModel(_viewModel.MovieTitle, _viewModel.MovieGenre, _viewModel.MovieImage));
            await _navigationService.GoBack();
        }
    }
}
