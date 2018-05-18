using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FilmsManager.ViewModels
{
    class AddFilmViewModel : BaseViewModel
    {
        private string _movieImage = "icon.png";
        private string _movieGenre;
        private string _movieTitle;

        public ICommand AddCommand { get; set; }
        public ICommand OpenGalleryCommand { get; set; }

        public string MovieTitle
        {
            get => _movieTitle;
            set
            {
                _movieTitle = value;
                RaisePropertyChanged();
            }
        }

        public string MovieGenre
        {
            get => _movieGenre;
            set
            {
                _movieGenre = value;
                RaisePropertyChanged();
            }
        }

        public string MovieImage
        {
            get => _movieImage;
            set
            {
                _movieImage = value;
                RaisePropertyChanged();
            }
        }


        public AddFilmViewModel(ObservableCollection<MovieModel> movieList )
        {
            AddCommand = new AddCommand(NavigationService, this, movieList);
            OpenGalleryCommand = new OpenGalleryCommand(NavigationService);
        }
    }

    internal class AddCommand : ICommand
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
                //await page.DisplayAlert("Error!",
                //"Not all values have been inserted",
                //"OK");
                return;
            }
            _movieList.Add(new MovieModel(_viewModel.MovieTitle, _viewModel.MovieGenre, _viewModel.MovieImage));
            await _navigationService.GoBack();
        }
    }

    internal class OpenGalleryCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly INavigationService _navigationService;

        public OpenGalleryCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await _navigationService.NavigateAsync("PickImagePage");
        }
    }
}
