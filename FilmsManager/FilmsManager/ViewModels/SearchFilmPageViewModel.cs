using FilmsManager.Models;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
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
		private GenreModel _selectedGenre;
		private MovieModel _selectedMovie;
		private ObservableCollection<GenreModel> _genreList;
		private IList<MovieModel> _movieList;

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

		public ObservableCollection<GenreModel> GenreList
		{
			get => _genreList;
			set { SetProperty(ref _genreList, value); }
		}

		public ObservableCollection<MovieModel> FilteredMovieList
		{
			get => _filteredMovieList;
			set { SetProperty(ref _filteredMovieList, value); }
		}

		public GenreModel SelectedGenre
		{
			get => _selectedGenre;
			set
			{
				SetProperty(ref _selectedGenre, value);
				if (_selectedGenre == null)
					return;
				SearchFilmCommand.Execute(_selectedGenre.Name);
			}
		}
		public MovieModel SelectedMovie
		{
			get => _selectedMovie;
			set { SetProperty(ref _selectedMovie, value); }
		}

		public ICommand SearchFilmCommand { get; set; }

		public ICommand SwapSearchCommand { get; set; }

		public ICommand FilmDetailsCommand { get; set; }

		public SearchFilmPageViewModel(INavigationService navigationService) : base(navigationService)
		{
			SearchFilmCommand = new DelegateCommand(OnSearchFilm);
			SwapSearchCommand = new DelegateCommand(OnSwapSearch);
			FilmDetailsCommand = new DelegateCommand(OnFilmDetailAsync);
		}

		public override void OnNavigatedTo(NavigationParameters parameters)
		{
			if (parameters == null || parameters.Count == 0)
				return;

			ObservableCollection<MovieModel> movieList;
			parameters.TryGetValue("movieList", out movieList);
			if ( movieList != null )
				_movieList = movieList;
			
			FilteredMovieList = new ObservableCollection<MovieModel>(movieList);

			IList<GenreModel> genreList;
			parameters.TryGetValue("genreList", out genreList);
			if ( genreList != null )
			{
				genreList.Insert(0, new GenreModel("All"));
				GenreList = new ObservableCollection<GenreModel>(genreList);
			}
			
		}

		private async void OnFilmDetailAsync()
		{
			if (SelectedMovie == null)
				return;

			var parameters = new NavigationParameters
			{
				{ "movie", SelectedMovie }
			};
			await NavigationService.NavigateAsync("FilmDetailsPage", parameters);
		}

		private void OnSwapSearch()
		{
			switch (SearchType)
			{
				case "Title": //Title to Genre
					SearchType = "Genre";
					AlternativeType = "Title";
					PickerVisible = true;
					SearchBarVisible = false;
					SelectedGenre = null;
					break;

				case "Genre": //Genre to Title
					SearchType = "Title";
					AlternativeType = "Genre";
					PickerVisible = false;
					SearchBarVisible = true;
					TextEntry = null;
					break;
			}
			FilteredMovieList = new ObservableCollection<MovieModel>(_movieList);
		}

		private void OnSearchFilm()
		{
			switch (SearchType)
			{
				case "Title":
					if (TextEntry == null)
						return;
					FilteredMovieList = new ObservableCollection<MovieModel>(_movieList.Where(m => m.Title.Contains(TextEntry)));
					break;
				case "Genre":
					if (SelectedGenre == null)
						return;
					if ( SelectedGenre.Name == "All")
						FilteredMovieList = new ObservableCollection<MovieModel>(_movieList);
					else
						FilteredMovieList = new ObservableCollection<MovieModel>(_movieList.Where(m => m.Genre.Equals(SelectedGenre.Name)));
					break;
			}
		}

		public void OnAppearing()
		{
			SelectedMovie = null;
		}
	}
}
