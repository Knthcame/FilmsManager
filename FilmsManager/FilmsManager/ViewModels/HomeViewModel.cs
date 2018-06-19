using FilmsManager.Models;
using FilmsManager.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FilmsManager.ViewModels
{
	public class HomeViewModel : BaseViewModel
	{
		private MovieModel _selectedMovie;

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
			set
			{
				_selectedMovie = value;
				RaisePropertyChanged();
			}
		}

		public HomeViewModel()
		{
			NavigateCommand = new NavigateCommand(NavigationService, MovieList);
			SearchCommand = new SearchCommand(NavigationService);
			FilmDetailsCommand = new FilmDetailsCommand(NavigationService);
		}

		public void OnAppearing()
		{
			SelectedMovie = null;
		}


	}
}