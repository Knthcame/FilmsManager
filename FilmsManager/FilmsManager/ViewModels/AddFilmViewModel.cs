using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace FilmsManager.ViewModels
{
    class AddFilmViewModel : BaseViewModel
    {
        public ICommand AddCommand { get; set; }
        public string MovieTitle;
        public string MovieGenre;
        public string MovieImage;
        ContentPage page;

        public AddFilmViewModel(INavigationService navigationService, ContentPage page) : base (navigationService)
        {
            AddCommand = new AddCommand(navigationService, page, MovieTitle, MovieGenre, MovieImage);
            this.page = page;
        }

    }

    internal class AddCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        INavigationService _navigationService;
        public string MovieTitle;
        public string MovieGenre;
        public string MovieImage;
        ContentPage page;

        public AddCommand(INavigationService navigationService, ContentPage page, string Title, string Genre, string Image)
        {
            _navigationService = navigationService;
            MovieTitle = Title;
            MovieGenre = Genre;
            MovieImage = Image;
            this.page = page;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (MovieTitle == null | MovieGenre == null)
            {
                await page.DisplayAlert("Error!",
                "Not all values have been inserted",
                "OK");
                return;
            }
            new MovieModel(MovieTitle, MovieGenre, MovieImage);
            await _navigationService.GoBack();
        }
    }
}
