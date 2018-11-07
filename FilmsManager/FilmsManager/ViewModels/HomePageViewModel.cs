using System.Threading.Tasks;
using System.Windows.Input;
using FilmsManager.Constants;
using FilmsManager.Events;
using FilmsManager.Logging.Interfaces;
using FilmsManager.Managers.Interfaces;
using FilmsManager.Models;
using FilmsManager.Resources;
using FilmsManager.ResxLocalization;
using FilmsManager.Views;
using Models.Managers.Interfaces;
using Models.Resources;
using Nito.AsyncEx;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

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

        private readonly IEventAggregator _eventAggregator;
		public string SearchToolbarIcon { get; set; } = AppImages.MagnifyingGlass;

		public ICommand AddFilmCommand { get; set; }

		public ICommand SearchCommand { get; set; }

		public ICommand LanguageOptionsCommand { get; set; }

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

        #endregion properties

        public HomePageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IGenreModelManager genreModelManager, IHttpManager httpManager, IPageDialogService pageDialogService, ICustomLogger logger) 
            : base(navigationService, httpManager, genreModelManager, pageDialogService, logger)
		{
			_eventAggregator = eventAggregator;
			_eventAggregator.GetEvent<AddFilmEvent>().Subscribe(async () => await RefreshMovieListAsync());
            _eventAggregator.GetEvent<ConnectionErrorEvent>().Subscribe(async () => await NotifyConnectionErrorAsync());
            _eventAggregator.GetEvent<SelectLanguageEvent>().Subscribe(async(language) => await InitializeHomePageAsync(language));
            InitializationNotifier = NotifyTaskCompletion.Create(async() => await InitializeHomePageAsync());
			AddFilmCommand = new DelegateCommand(async () => await OnAddButtonClickedAsync());
			SearchCommand = new DelegateCommand(async () => await OnSearchAsync());
			LanguageOptionsCommand = new DelegateCommand(async () => await OnLanguageOptionsAsync());
        }

        private async Task InitializeHomePageAsync(LanguageModel language = null)
        {
            IsRefreshingMovieList = true;
            await InitializeAppLanguageAsync(language);
            LoadResources();
            GetGenresAsync();
            RefreshMovieListAsync();
        }

        private async Task NotifyConnectionErrorAsync()
        {
            await _pageDialogService.DisplayAlertAsync(AppResources.ConnectionErrorTitle, AppResources.ConnectionErrorMessage, AppResources.ConnectionErrorOkButton);
            IsRefreshingMovieList = false;
            IsMovieListEmpty = true;
        }

		public void LoadResources()
		{
			Title = AppResources.HomePageTitle;
			ImageColumn = AppResources.ImageColumn;
			GenreColumn = AppResources.GenreColumn;
			TitleColumn = AppResources.TitleColumn;
			AddText = AppResources.HomePageAddButton;
            Labeltext = AppResources.NoMoviesLabeltext;
			switch (Device.RuntimePlatform)
			{
				case Device.Android:
					Flag = ImageSource.FromFile(AppResources.Flag) as FileImageSource;
					break;
				case Device.iOS:
					LanguageAbreviation = AppResources.LanguageAbreviation;
					break;
			}
            RefreshGenreList();
		}

        private async Task InitializeAppLanguageAsync(LanguageModel language = null)
        {
            if (language == null)
                language = await _httpManager.RefreshDataAsync<LanguageModel, LanguageModel>();

            if (language == null)
            {
                var culture = Xamarin.Forms.DependencyService.Get<ILocalize>().GetMobileCultureInfo();
                if (!LanguageConstants.SupportedCultures.TryGetValue(culture.Name, out language))
                    language = LanguageConstants.DefaultLanguage;
            }
            await SetAppLanguageAsync(language);
        }

		private async Task OnLanguageOptionsAsync()
		{
			await NavigationService.NavigateAsync(nameof(LanguageSelectionPage), useModalNavigation: true);
		}

		private async Task OnSearchAsync()
		{
            if (IsMovieListEmpty)
            {
                await _pageDialogService.DisplayAlertAsync(AppResources.SearchEmptyListTitle, AppResources.SearchEmptyListMessage, AppResources.SearchEmptyListCancelButton);
                return;
            }
            var parameters = new NavigationParameters()
            {
                {NavigationConstants.MovieList, MovieList },
                {NavigationConstants.GenreList, GenreList }
            };    
			await NavigationService.NavigateAsync(nameof(SearchFilmPage), parameters);
		}

		private async Task OnAddButtonClickedAsync()
		{
			var parameters = new NavigationParameters
			{
				{ NavigationConstants.MovieList, MovieList },
				{ NavigationConstants.GenreList, GenreList }
			};
			await NavigationService.NavigateAsync(nameof(AddFilmPage), parameters);
		}

        protected override async Task RefreshMovieListAsync()
        {
            IsRefreshingMovieList = true;
            await base.RefreshMovieListAsync();
            IsRefreshingMovieList = false;
        }
    }
}