using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Models.Resources;
using System.Collections.Generic;
using Prism.Events;
using Models.Events;
using Xamarin.Forms;
using Models.Constants;
using Models.ApiServices;
using Models.ApiServices.Interfaces;
using System.Linq;
using Models.Managers.Interfaces;
using Models.Classes;
using FilmsManager.Models;
using FilmsManager.Resources;
using FilmsManager.Views;
using Nito.AsyncEx;

namespace FilmsManager.ViewModels
{
	public class HomePageViewModel : BaseViewModel
	{
		private MovieModel _selectedMovie;

		private readonly IRestService _restService = new RestService();

		public string BackgroundImage { get; set; } = AppImages.BackgroundImageHome;

		public string SearchToolbarIcon { get; set; } = AppImages.MagnifyingGlass;

		private IList<MovieModel> _movieList = new ObservableCollection<MovieModel>();

		public IList<MovieModel> MovieList
		{
			get => _movieList;
			set { SetProperty(ref _movieList, value); }
		}
		public IList<GenreModel> GenreList { get; set; }

		public ICommand NavigateCommand { get; set; }

		public ICommand SearchCommand { get; set; }

		public ICommand FilmDetailsCommand { get; set; }

		public ICommand LanguageOptionsCommand { get; set; }

		private readonly IEventAggregator _eventAggregator;

		private readonly IGenreModelManager _genreModelManager;

		public MovieModel SelectedMovie
		{
			get => _selectedMovie;
			set { SetProperty(ref _selectedMovie, value); }
		}

		private string _languageAbreviation;

		public string LanguageAbreviation
		{
			get { return _languageAbreviation; }
			set { SetProperty(ref _languageAbreviation, value); }
		}

		private FileImageSource _flag;

		public FileImageSource Flag
		{
			get { return _flag; }
			set { SetProperty(ref _flag, value); }
		}

		private string _addText;
		public string AddText
		{
			get { return _addText; }
			set { SetProperty(ref _addText, value); }
		}

		private string _imageColumn;

		public string ImageColumn
		{
			get { return _imageColumn; }
			set { SetProperty(ref _imageColumn, value); }
		}

		private string _titleColumn;

		public string TitleColumn
		{
			get { return _titleColumn; }
			set { SetProperty(ref _titleColumn, value); }
		}

		private string _genreColumn;

		public string GenreColumn
		{
			get { return _genreColumn; }
			set { SetProperty(ref _genreColumn, value); }
		}

		public INotifyTaskCompletion InitializationNotifier { get; private set; }

		public Task Initialization => InitializationNotifier.Task;

		public HomePageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IGenreModelManager genreModelManager) : base(navigationService)
		{
			_genreModelManager = genreModelManager;
			_eventAggregator = eventAggregator;
			_eventAggregator.GetEvent<SelectLanguageEvent>().Subscribe(LoadResources);
			NavigateCommand = new DelegateCommand(async () => await OnNavigateAsync());
			SearchCommand = new DelegateCommand(async () => await OnSearchAsync());
			FilmDetailsCommand = new DelegateCommand(async () => await OnFilmDetailAsync());
			LanguageOptionsCommand = new DelegateCommand(async () => await OnLanguageOptionsAsync());
			InitializationNotifier = NotifyTaskCompletion.Create(RetrieveMovieListAsync());
			MovieList.OrderBy(m => m.Title);
			LoadResources();
		}

		public async Task RetrieveMovieListAsync()
		{
			var list = await _restService.RefreshDataAsync();
			var movies = new ObservableCollection<MovieModel>();
			foreach (ToDoItem item in list)
			{
				movies.Add(new MovieModel(item.Title, item.Genre, item.Image));
			}
			MovieList = new ObservableCollection<MovieModel>(movies);
		}

		public void LoadResources()
		{
			Title = AppResources.HomePageTitle;
			ImageColumn = AppResources.ImageColumn;
			GenreColumn = AppResources.GenreColumn;
			TitleColumn = AppResources.TitleColumn;
			AddText = AppResources.HomePageAddButton;
			switch (Device.RuntimePlatform)
			{
				case Device.Android:
					Flag = ImageSource.FromFile(AppResources.Flag) as FileImageSource;
					break;
				case Device.iOS:
					LanguageAbreviation = AppResources.LanguageAbreviation;
					break;
			}
			GenreList = GenerateGenreList();
			RefreshMovieList();
		}

		public ObservableCollection<GenreModel> GenerateGenreList()
		{
			return new ObservableCollection<GenreModel>()
			{
				_genreModelManager.FindByID(GenreKeys.FantasyGenre),
				_genreModelManager.FindByID(GenreKeys.TerrorGenre),
				_genreModelManager.FindByID(GenreKeys.DramaGenre),
				_genreModelManager.FindByID(GenreKeys.HumourGenre),
				_genreModelManager.FindByID(GenreKeys.ScienceFictionGenre),
				_genreModelManager.FindByID(GenreKeys.ActionGenre),
				_genreModelManager.FindByID(GenreKeys.SuperHeroesGenre)
			};
		}

		public void RefreshMovieList()
		{
			foreach (MovieModel movie in MovieList)
			{
				movie.Genre = _genreModelManager.FindByID(movie.Genre.ID);
			}
		}

		private async Task OnLanguageOptionsAsync()
		{
			await NavigationService.NavigateAsync(nameof(LanguageSelectionPage), useModalNavigation: true);
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

		private async Task OnSearchAsync()
		{
			var parameters = new NavigationParameters
			{
				{ "movieList", MovieList },
				{ "genreList", GenreList }
			};
			await NavigationService.NavigateAsync(nameof(SearchFilmPage), parameters);
		}

		private async Task OnNavigateAsync()
		{
			var parameters = new NavigationParameters
			{
				{ "movieList", MovieList },
				{ "genreList", GenreList }
			};
			await NavigationService.NavigateAsync(nameof(AddFilmPage), parameters);
		}

		public override void OnAppearing()
		{
			SelectedMovie = null;
			MovieList = new ObservableCollection<MovieModel>(MovieList.OrderBy(m => m.Title));
		}

	}
}