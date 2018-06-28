using FilmsManager.Models;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FilmsManager.ViewModels
{
	public class SearchFilmPageViewModel : BaseViewModel
	{
		private string _textEntry;
		private ObservableCollection<MovieModel> _filteredMovieList = new ObservableCollection<MovieModel>();
		private string _alternativeType = "Genre";
		private string _searchType = "Title";
		private bool _searchBarVisible = true;
		private bool _pickerVisible = false;
		private object _selectedGenre;
		private MovieModel _selectedMovie;

		public string BackgroundImage { get; set; } = "back6.jpg";

		public bool PickerVisible
		{
			get => _pickerVisible;
			set { SetProperty(ref _pickerVisible, value); }
		}

		public bool SearchBarVisible
		{
			get => _searchBarVisible;
			set { SetProperty(ref _searchBarVisible, value); }
		}

		public string AlternativeType
		{
			get => _alternativeType;
			set { SetProperty(ref _alternativeType, value); }
		}

		public string SearchType
		{
			get => _searchType;
			set { SetProperty(ref _searchType, value); }
		}

		public string TextEntry
		{
			get => _textEntry;
			set { SetProperty(ref _textEntry, value); }
		}

		public ObservableCollection<GenreModel> GenreList { get; set; } = HomePageViewModel.GenreList;
		public ObservableCollection<MovieModel> MovieList { get; set; }

		public ObservableCollection<MovieModel> FilteredMovieList
		{
			get => _filteredMovieList;
			set { SetProperty(ref _filteredMovieList, value); }
		}

		public object SelectedGenre
		{
			get => _selectedGenre;
			set
			{
				GenreModel aux = value as GenreModel;
				_selectedGenre = aux.Name;
				SearchFilmCommand.Execute(_selectedGenre);
			}
		}

		public ICommand SearchFilmCommand { get; set; }

		public ICommand SwapSearchCommand { get; set; }

		public ICommand FilmDetailsCommand { get; set; }

		public MovieModel SelectedMovie
		{
			get => _selectedMovie;
			set { SetProperty(ref _selectedMovie, value); }
		}

		public SearchFilmPageViewModel(INavigationService navigationService) : base(navigationService)
		{
			SearchFilmCommand = new DelegateCommand<string>(OnSearchFilm);
			SwapSearchCommand = new DelegateCommand(OnSwapSearch);
			FilmDetailsCommand = new DelegateCommand<MovieModel>(OnFilmDetail);
		}

		public override void OnNavigatedTo(NavigationParameters parameters)
		{
			if (parameters == null)
				return;

			ObservableCollection<MovieModel> movieList;
			parameters.TryGetValue("movieList",out movieList);
			MovieList = movieList;
		}

		private void OnFilmDetail(MovieModel movieModel)
		{
			NavigationService.NavigateAsync("FilmDetailsPage");
		}

		private void OnSwapSearch()
		{
			switch (SearchType)
			{
				case "Title":
					SearchType = "Genre";
					AlternativeType = "Title";
					PickerVisible = true;
					SearchBarVisible = false;
					break;

				case "Genre":
					SearchType = "Title";
					AlternativeType = "Genre";
					PickerVisible = false;
					SearchBarVisible = true;
					break;
			}
			FilteredMovieList.Clear();
		}

		private void OnSearchFilm(string text)
		{
			switch (SearchType)
			{
				case "Title":
					FilteredMovieList = new ObservableCollection<MovieModel>(MovieList.Where(m => m.Title.Contains(text)));
					break;
				case "Genre":
					FilteredMovieList = new ObservableCollection<MovieModel>(MovieList.Where(m => m.Genre.Equals(text)));
					break;
			}
		}

		public void OnAppearing()
		{
			SelectedMovie = null;
		}
	}
}
