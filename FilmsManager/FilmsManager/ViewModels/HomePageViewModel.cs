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
using FilmsManager.Models;
using FilmsManager.Resources;
using FilmsManager.Views;
using Nito.AsyncEx;
using FilmsManager.Events;

namespace FilmsManager.ViewModels
{
	public class HomePageViewModel : MovieListContentViewModel
	{

		public string SearchToolbarIcon { get; set; } = AppImages.MagnifyingGlass;

		public ICommand NavigateCommand { get; set; }

		public ICommand SearchCommand { get; set; }

		public ICommand LanguageOptionsCommand { get; set; }

		private readonly IEventAggregator _eventAggregator;

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
		private bool _listViewIsRefreshing =false;

		public string GenreColumn
		{
			get { return _genreColumn; }
			set { SetProperty(ref _genreColumn, value); }
		}

		public bool ListViewIsRefreshing
		{
			get => _listViewIsRefreshing;
			set { SetProperty(ref _listViewIsRefreshing, value); }
		}

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
			ListViewIsRefreshing = false;
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
				movie.Genre = _genreModelManager.FindByID(movie.Genre.ID);
				await _restService.SaveToDoItemAsync(new MovieItem(movie.Id, movie.Title, movie.Genre, movie.Image), false);
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