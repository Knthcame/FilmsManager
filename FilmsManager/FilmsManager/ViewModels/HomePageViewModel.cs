using FilmsManager.Models;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using FilmsManager.Resources;
using System.Globalization;
using Xamarin.Forms;
using FilmsManager.ResxLocalization;
using Prism.Ioc;

namespace FilmsManager.ViewModels
{
	public class HomePageViewModel : BaseViewModel
	{
		private MovieModel _selectedMovie;
		private LanguageModel _selectedLanguage = new LanguageModel("English", "en");

		public string BackgroundImage { get; set; } = "Back6.jpg";

		public ObservableCollection<MovieModel> MovieList { get; set; } = new ObservableCollection<MovieModel> {
			new MovieModel("Shrek","Humour", "Shrek.jpg"),
			new MovieModel("Shrek 2", "Humour", "Shrek2.jpg"),
			new MovieModel("Infinity war", "Super Heroes", "infinity_war.jpg")
		};

		public ObservableCollection<GenreModel> GenreList { get; set; } = new ObservableCollection<GenreModel>()
		{
			new GenreModel("Fantasy"),
			new GenreModel("Action"),
			new GenreModel("Drama"),
			new GenreModel("Humour"),
			new GenreModel("ScieneFiction"),
			new GenreModel("Terror"),
			new GenreModel("Super Heroes")
		};

		public ObservableCollection<LanguageModel> LanguageList { get; set; } = new ObservableCollection<LanguageModel>()
		{
			new LanguageModel("English", "en"),
			new LanguageModel("Spanish", "es")
		};

		public ICommand NavigateCommand { get; set; }

		public ICommand SearchCommand { get; set; }

		public ICommand FilmDetailsCommand { get; set; }

		public ICommand SelectLanguageCommand { get; set; }

		public MovieModel SelectedMovie
		{
			get => _selectedMovie;
			set { SetProperty(ref _selectedMovie, value); }
		}

		public LanguageModel SelectedLanguage
		{
			get => _selectedLanguage;
			set { SetProperty(ref _selectedLanguage, value); }
		}
		public HomePageViewModel(INavigationService navigationService) : base(navigationService)
		{
			Title = AppResources.HomePageTitle;
			NavigateCommand = new DelegateCommand(async () => await OnNavigateAsync());
			SearchCommand = new DelegateCommand(async () => await OnSearchAsync());
			FilmDetailsCommand = new DelegateCommand(async () => await OnFilmDetailAsync());
			SelectLanguageCommand = new DelegateCommand(OnSelectLanguage);
		}

		private void OnSelectLanguage()
		{
			if (_selectedLanguage == null)
				return;
			var ci = new CultureInfo(_selectedLanguage.Abreviation);
			AppResources.Culture = ci;
			DependencyService.Get<ILocalize>().SetCurrentCultureInfo(ci);
			App.CreateNewMainPage();
		}

		private async Task OnFilmDetailAsync()
		{
			if (SelectedMovie == null)
				return;
			var parameters = new NavigationParameters
			{
				{ "movie", SelectedMovie }
			};
			await NavigationService.NavigateAsync("FilmDetailsPage", parameters);
		}

		private async Task OnSearchAsync()
		{
			var parameters = new NavigationParameters
			{
				{ "movieList", MovieList },
				{ "genreList", GenreList }
			};
			await NavigationService.NavigateAsync("SearchFilmPage", parameters);
		}

		private async Task OnNavigateAsync()
		{
			var parameters = new NavigationParameters
			{
				{ "movieList", MovieList },
				{ "genreList", GenreList }
			};
			await NavigationService.NavigateAsync("AddFilmPage", parameters);
		}

		public override void OnAppearing()
		{
			SelectedMovie = null;
		}


	}
}