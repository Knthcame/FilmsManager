using Models.Constants;
using Models.Managers.Interfaces;
using Models.Resources;
using Models.Classes;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FilmsManager.Models;
using FilmsManager.Resources;
using FilmsManager.Views;

namespace FilmsManager.ViewModels
{
	public class SearchFilmPageViewModel : BaseViewModel
	{
		private const int TITLE = 0;
		private const int GENRE = 1;
		private string _textEntry;
		private ObservableCollection<MovieModel> _filteredMovieList = new ObservableCollection<MovieModel>();
		private string _searchTypeButtonText = AppResources.SearchTypeButtonText + AppResources.GenreColumn;
		private int _searchType = TITLE;
		private bool _searchBarVisible = true;
		private bool _pickerVisible = false;
		private GenreModel _selectedGenre;
		private MovieModel _selectedMovie;
		private ObservableCollection<GenreModel> _genreList;
		private IList<MovieModel> _movieList;

		public string BackgroundImage { get; set; } = AppImages.BackgroundImageHome;

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

		public string SearchTypeButtonText
		{
			get => _searchTypeButtonText;
			set { SetProperty(ref _searchTypeButtonText, value); }
		}

		public int SearchType
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

		private readonly IGenreModelManager _genreModelManager;

		public SearchFilmPageViewModel(INavigationService navigationService, IGenreModelManager genreModelManager) : base(navigationService)
		{
			_genreModelManager = genreModelManager;
			Title = AppResources.SearchFilmPageTitle;
			SearchFilmCommand = new DelegateCommand(OnSearchFilm);
			SwapSearchCommand = new DelegateCommand(OnSwapSearch);
			FilmDetailsCommand = new DelegateCommand(async () => await OnFilmDetailAsync());
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
			if(parameters.TryGetValue("genreList", out genreList))
			{
				GenreModel[] genres = new GenreModel[genreList.Count];
				genreList.CopyTo(genres, 0);
				GenreList = new ObservableCollection<GenreModel>(genres);
				GenreList.Insert(0, _genreModelManager.FindByID(GenreKeys.AllGenres));
			}
			
		}

		private async Task OnFilmDetailAsync()
		{
			if (SelectedMovie == null)
				return;

			var parameters = new NavigationParameters
			{
				{ "movie", SelectedMovie }
			};
			await NavigationService.NavigateAsync(nameof(FilmDetailsPage), parameters);
		}

		private void OnSwapSearch()
		{
			switch (SearchType)
			{
				case TITLE: //Title to Genre
					SearchType = GENRE;
					SearchTypeButtonText = AppResources.SearchTypeButtonText + AppResources.TitleColumn;
					PickerVisible = true;
					SearchBarVisible = false;
					SelectedGenre = null;
					break;

				case GENRE: //Genre to Title
					SearchType = TITLE;
					SearchTypeButtonText = AppResources.SearchTypeButtonText + AppResources.GenreColumn;
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
				case TITLE:
					if (TextEntry != null) 
						FilteredMovieList = new ObservableCollection<MovieModel>(_movieList.Where(m => m.Title.Contains(TextEntry)));
					break;
				case GENRE:
					if (SelectedGenre != null)
					{
						if (SelectedGenre.ID == GenreKeys.AllGenres)
							FilteredMovieList = new ObservableCollection<MovieModel>(_movieList);
						else
							FilteredMovieList = new ObservableCollection<MovieModel>(_movieList.Where(movie => movie.Genre.Name.Equals(SelectedGenre.Name)));
					}
					break;
			}
		}

		public override void OnAppearing()
		{
			SelectedMovie = null;
		}
	}
}
