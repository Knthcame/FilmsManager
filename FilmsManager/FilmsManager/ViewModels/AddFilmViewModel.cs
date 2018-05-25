using FilmsManager.Models;
using FilmsManager.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FilmsManager.ViewModels
{
    public class AddFilmViewModel : BaseViewModel
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


        public AddFilmViewModel(ObservableCollection<MovieModel> movieList)
        {
            AddCommand = new AddCommand(NavigationService, this, movieList);
            OpenGalleryCommand = new OpenGalleryCommand(NavigationService);
            MessagingCenter.Subscribe<PickImageCommand, string>(this, "PickImage", (s, a) => MovieImage = a);
        }
    }
}
