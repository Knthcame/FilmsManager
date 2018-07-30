using Models.Constants;
using Models.Managers.Interfaces;
using Models.Classes;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FilmsManager.Models;
using FilmsManager.Resources;
using FilmsManager.Enums;
using Models.ApiServices.Interfaces;
using FilmsManager.Extensions;

namespace FilmsManager.ViewModels
{
	public class SearchFilmPageViewModel : MovieListContentViewModel
	{
		private string _textEntry;
		private ObservableCollection<MovieModel> _filteredMovieList = new ObservableCollection<MovieModel>();
		private string _searchTypeButtonText = AppResources.SearchTypeButtonText + AppResources.GenreColumn;
		private int _searchType = (int)SearchTypeEnum.Title;
		private bool _searchBarVisible = true;
		private bool _pickerVisible = false;
		private GenreModel _selectedGenre;

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

		public ObservableCollection<MovieModel> FilteredMovieList { get; set; } = new ObservableCollection<MovieModel>();

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

		public ICommand SearchFilmCommand { get; set; }

		public ICommand SwapSearchCommand { get; set; }

		public SearchFilmPageViewModel(INavigationService navigationService, IGenreModelManager genreModelManager, IRestService restService) : base(navigationService, restService, genreModelManager)
		{
			Title = AppResources.SearchFilmPageTitle;
			SearchFilmCommand = new DelegateCommand(OnSearchFilm);
			SwapSearchCommand = new DelegateCommand(OnSwapSearch);
			FilteredMovieList.AddRange(MovieList);
		}

		//public override void OnNavigatedTo(NavigationParameters parameters)
		//{
		//	if (parameters == null || parameters.Count == 0)
		//		return;

		//	ObservableCollection<MovieModel> movieList;
		//	parameters.TryGetValue("movieList", out movieList);
		//	if ( movieList != null )
		//		_movieList = movieList;
			
		//	FilteredMovieList = new ObservableCollection<MovieModel>(movieList);

		//	IList<GenreModel> genreList;
		//	if(parameters.TryGetValue("genreList", out genreList))
		//	{
		//		GenreModel[] genres = new GenreModel[genreList.Count];
		//		genreList.CopyTo(genres, 0);
		//		GenreList = new ObservableCollection<GenreModel>(genres);
		//		GenreList.Insert(0, _genreModelManager.FindByID(GenreKeys.AllGenres));
		//	}
			
		//}

		private void OnSwapSearch()
		{
			switch ((SearchTypeEnum)SearchType)
			{
				case SearchTypeEnum.Title: //Title to Genre
					SearchType = (int) SearchTypeEnum.Genre;
					SearchTypeButtonText = AppResources.SearchTypeButtonText + AppResources.TitleColumn;
					PickerVisible = true;
					SearchBarVisible = false;
					SelectedGenre = null;
					break;

				case SearchTypeEnum.Genre: //Genre to Title
					SearchType = (int) SearchTypeEnum.Title;
					SearchTypeButtonText = AppResources.SearchTypeButtonText + AppResources.GenreColumn;
					PickerVisible = false;
					SearchBarVisible = true;
					TextEntry = null;
					break;
				default:
					SearchType = (int) SearchTypeEnum.Title;
					break;
			}
			FilteredMovieList = new ObservableCollection<MovieModel>(MovieList);
		}

		private void OnSearchFilm()
		{
			switch ((SearchTypeEnum) SearchType)
			{
				case SearchTypeEnum.Title:
					if (TextEntry != null) 
						FilteredMovieList = new ObservableCollection<MovieModel>(MovieList.Where(m => m.Title.Contains(TextEntry)));
					break;
				case SearchTypeEnum.Genre:
					if (SelectedGenre != null)
					{
						if (SelectedGenre.Id == GenreKeys.AllGenres)
							FilteredMovieList = new ObservableCollection<MovieModel>(MovieList);
						else
							FilteredMovieList = new ObservableCollection<MovieModel>(MovieList.Where(movie => movie.Genre.Name.Equals(SelectedGenre.Name)));
					}
					break;
			}
		}

		protected override Task RefreshMovieListAsync()
		{
			return base.RefreshMovieListAsync();
		}
	}
}
