using FilmsManager.Models;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using FilmsManager.Resources;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FilmsManager.ViewModels
{
	public class HomePageViewModel : BaseViewModel
	{
		private MovieModel _selectedMovie;

		public string BackgroundImage { get; set; } = "Back6.jpg";

		public IList<MovieModel> MovieList { get; set; } = new ObservableCollection<MovieModel> {
			new MovieModel("Shrek",AppResources.HumourGenre, "Shrek.jpg"),
			new MovieModel("Shrek 2", AppResources.HumourGenre, "Shrek2.jpg"),
			new MovieModel("Infinity war", AppResources.SuperHeroesGenre, "infinity_war.jpg")
		};

		public IList<GenreModel> GenreList { get; set; } = new ObservableCollection<GenreModel>()
		{
			new GenreModel(AppResources.FantasyGenre),
			new GenreModel(AppResources.TerrorGenre),
			new GenreModel(AppResources.DramaGenre),
			new GenreModel(AppResources.HumourGenre),
			new GenreModel(AppResources.ScienceFictionGenre),
			new GenreModel(AppResources.TerrorGenre),
			new GenreModel(AppResources.SuperHeroesGenre)
		};

		public ICommand NavigateCommand { get; set; }

		public ICommand SearchCommand { get; set; }

		public ICommand FilmDetailsCommand { get; set; }

		public ICommand LanguageOptionsCommand { get; set; }

		public MovieModel SelectedMovie
		{
			get => _selectedMovie;
			set { SetProperty(ref _selectedMovie, value); }
		}

		public string Flag { get; set; } = null;

		public HomePageViewModel(INavigationService navigationService) : base(navigationService)
		{
			if(Device.RuntimePlatform == Device.Android)
			{
				Flag = AppResources.Flag;
			}
			NavigateCommand = new DelegateCommand(async () => await OnNavigateAsync());
			SearchCommand = new DelegateCommand(async () => await OnSearchAsync());
			FilmDetailsCommand = new DelegateCommand(async () => await OnFilmDetailAsync());
			LanguageOptionsCommand = new DelegateCommand(async () => await OnLanguageOptionsAsync());
		}

		private async Task OnLanguageOptionsAsync()
		{
			await NavigationService.NavigateAsync("LanguageSelectionPage", useModalNavigation: true);
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