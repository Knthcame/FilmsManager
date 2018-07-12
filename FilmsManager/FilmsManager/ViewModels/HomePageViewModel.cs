using FilmsManager.Models;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using FilmsManager.Resources;
using System.Collections.Generic;
using Prism.Events;
using FilmsManager.Events;
using Xamarin.Forms;
using FilmsManager.Constants;
using FilmsManager.Views;

namespace FilmsManager.ViewModels
{
	public class HomePageViewModel : BaseViewModel
	{
		private MovieModel _selectedMovie;

		public string BackgroundImage { get; set; } = AppImages.BackgroundImageHome;

		public string SearchToolbarIcon { get; set; } = AppImages.MagnifyingGlass;

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

		private IList<MovieModel> _movieList;

		public string GenreColumn
		{
			get { return _genreColumn; }
			set { SetProperty(ref _genreColumn, value); }
		}

		public HomePageViewModel(INavigationService navigationService, IEventAggregator eventAggregator) : base(navigationService)
		{
			_eventAggregator = eventAggregator;
			_eventAggregator.GetEvent<SelectLanguageEvent>().Subscribe(LoadResources);
			NavigateCommand = new DelegateCommand(async () => await OnNavigateAsync());
			SearchCommand = new DelegateCommand(async () => await OnSearchAsync());
			FilmDetailsCommand = new DelegateCommand(async () => await OnFilmDetailAsync());
			LanguageOptionsCommand = new DelegateCommand(async () => await OnLanguageOptionsAsync());
			MovieList = new ObservableCollection<MovieModel> {
				new MovieModel("Shrek", new GenreModel(GenreKeys.HumourGenre), AppImages.Shrek),
				new MovieModel("Shrek 2", new GenreModel(GenreKeys.HumourGenre), AppImages.Shrek2),
				new MovieModel("Infinity war", new GenreModel(GenreKeys.SuperHeroesGenre), AppImages.InfinityWar)
			};
			LoadResources();
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
				new GenreModel(GenreKeys.FantasyGenre),
				new GenreModel(GenreKeys.TerrorGenre),
				new GenreModel(GenreKeys.DramaGenre),
				new GenreModel(GenreKeys.HumourGenre),
				new GenreModel(GenreKeys.ScienceFictionGenre),
				new GenreModel(GenreKeys.ActionGenre),
				new GenreModel(GenreKeys.SuperHeroesGenre)
			};
		}

		public void RefreshMovieList()
		{
			foreach (MovieModel movie in MovieList)
			{
				movie.Genre = new GenreModel(movie.Genre.ID);
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
		}


	}
}