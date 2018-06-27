using FilmsManager.Models;
using FilmsManager.ViewModels.Commands;
using Prism.Commands;
using Prism.Navigation;
using System;
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

		public HomeViewModel(INavigationService navigationService) : base(navigationService)
		{
			NavigateCommand = new DelegateCommand(OnNavigate);
			SearchCommand = new DelegateCommand(OnSearch);
			FilmDetailsCommand = new DelegateCommand<MovieModel>(OnFilmDetail);
		}

		private void OnFilmDetail(MovieModel movie)
		{
			if (movie != null)
			{
				NavigationService.NavigateAsync("FilmDetailsPage");
			}
		}

		private void OnSearch()
		{
			throw new NotImplementedException();
		}

		private void OnNavigate()
		{
			NavigationService.NavigateAsync("AddFilmPage");
		}

		public void OnAppearing()
		{
			SelectedMovie = null;
		}


	}
}