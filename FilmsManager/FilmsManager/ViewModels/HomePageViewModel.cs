using FilmsManager.Models;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FilmsManager.ViewModels
{
	public class HomePageViewModel : BaseViewModel
	{
		private MovieModel _selectedMovie;

		public string BackgroundImage { get; set; } = "Back6.jpg";

		public ObservableCollection<MovieModel> MovieList { get; set; } = new ObservableCollection<MovieModel> {
			new MovieModel("Shrek","Humour", "Shrek.jpg"),
			new MovieModel("Shrek 2", "Humour", "Shrek2.jpg"),
			new MovieModel("Infinity war", "Super Heroes", "infinity_war.jpg")
		};

		public static ObservableCollection<GenreModel> GenreList { get; set; } = new ObservableCollection<GenreModel>()
		{
			new GenreModel("Fantasy"),
			new GenreModel("Action"),
			new GenreModel("Drama"),
			new GenreModel("Humour"),
			new GenreModel("Terror"),
			new GenreModel("ScieneFiction"),
			new GenreModel("Super Heroes")
		};

		public ICommand NavigateCommand { get; set; }

		public ICommand SearchCommand { get; set; }

		public ICommand FilmDetailsCommand { get; set; }

		public MovieModel SelectedMovie
		{
			get => _selectedMovie;
			set { SetProperty(ref _selectedMovie, value); }
		}

		public HomePageViewModel(INavigationService navigationService) : base(navigationService)
		{
			NavigateCommand = new DelegateCommand(OnNavigateAsync);
			SearchCommand = new DelegateCommand(OnSearchAsync);
			FilmDetailsCommand = new DelegateCommand<MovieModel>(OnFilmDetailAsync);
		}

		private async void OnFilmDetailAsync(MovieModel movie)
		{
			if (movie == null)
				return;
			var parameters = new NavigationParameters
			{
				{ "movie", movie }
			};
			await NavigationService.NavigateAsync("FilmDetailsPage", parameters);
		}

		private async void OnSearchAsync()
		{
			var parameters = new NavigationParameters
			{
				{ "movieList", MovieList }
			};
			await NavigationService.NavigateAsync("SearchFilmPage", parameters);
		}

		private async void OnNavigateAsync()
		{
			var parameters = new NavigationParameters
			{
				{ "movieList", MovieList }
			};
			await NavigationService.NavigateAsync("AddFilmPage", parameters);
		}

		public void OnAppearing()
		{
			SelectedMovie = null;
		}


	}
}