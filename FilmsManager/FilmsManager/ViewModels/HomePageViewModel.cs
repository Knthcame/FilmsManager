using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using Models.Resources;
using Prism.Events;
using Xamarin.Forms;
using Models.ApiServices.Interfaces;
using Models.Managers.Interfaces;
using Models.Classes;
using FilmsManager.Resources;
using FilmsManager.Views;
using Nito.AsyncEx;
using FilmsManager.Events;

namespace FilmsManager.ViewModels
{
	public class HomePageViewModel : MovieListContentViewModel
	{
        #region properties
        private string _titleColumn;

		private string _imageColumn;

		private string _addText;

		private FileImageSource _flag;

		private string _languageAbreviation;

		private string _genreColumn;

		private bool _isRefreshingMovieList = false;

		public string SearchToolbarIcon { get; set; } = AppImages.MagnifyingGlass;

		public ICommand NavigateCommand { get; set; }

		public ICommand SearchCommand { get; set; }

		public ICommand LanguageOptionsCommand { get; set; }

		private readonly IEventAggregator _eventAggregator;

		public string LanguageAbreviation
		{
			get { return _languageAbreviation; }
			set { SetProperty(ref _languageAbreviation, value); }
		}

		public FileImageSource Flag
		{
			get { return _flag; }
			set { SetProperty(ref _flag, value); }
		}

		public string AddText
		{
			get { return _addText; }
			set { SetProperty(ref _addText, value); }
		}

		public string ImageColumn
		{
			get { return _imageColumn; }
			set { SetProperty(ref _imageColumn, value); }
		}

		public string TitleColumn
		{
			get { return _titleColumn; }
			set { SetProperty(ref _titleColumn, value); }
		}

		public string GenreColumn
		{
			get { return _genreColumn; }
			set { SetProperty(ref _genreColumn, value); }
		}

		public bool IsRefreshingMovieList
		{
			get => _isRefreshingMovieList;
			set { SetProperty(ref _isRefreshingMovieList, value); }
		}
        #endregion properties

        public HomePageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IGenreModelManager genreModelManager, IRestService restService) : base(navigationService, restService, genreModelManager)
		{
			_eventAggregator = eventAggregator;
			_eventAggregator.GetEvent<SelectLanguageEvent>().Subscribe(async () => await LoadResourcesAsync());
			_eventAggregator.GetEvent<AddFilmEvent>().Subscribe(async () => await RetrieveMovieListAsync());
			NavigateCommand = new DelegateCommand(async () => await OnNavigateAsync());
			SearchCommand = new DelegateCommand(async () => await OnSearchAsync());
			LanguageOptionsCommand = new DelegateCommand(async () => await OnLanguageOptionsAsync());
			InitializationNotifier = NotifyTaskCompletion.Create(LoadResourcesAsync());
		}

		protected override async Task RefreshMovieListAsync()
		{
			await base.RefreshMovieListAsync();
			IsRefreshingMovieList = false;
		}

		public async Task LoadResourcesAsync()
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
			await UpdateMovieListLanguageAsync();
		}

		public async Task UpdateMovieListLanguageAsync()
		{
			foreach (MovieModel movie in MovieList)
			{
				movie.Genre = _genreModelManager.FindByID(movie.Genre.Id);
				await _restService.SaveToDoItemAsync(new MovieModel(movie.Id, movie.Title, movie.Genre, movie.Image), false);
			}
			await RetrieveMovieListAsync();
		}

		private async Task OnLanguageOptionsAsync()
		{
			await NavigationService.NavigateAsync(nameof(LanguageSelectionPage), useModalNavigation: true);
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

	}
}