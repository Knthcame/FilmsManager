using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FilmsManager.Models;
using FilmsManager.Views;
using Models.ApiServices.Interfaces;
using Models.Classes;
using Models.Constants;
using Models.Managers.Interfaces;
using Models.Resources;
using Nito.AsyncEx;
using Prism.Commands;
using Prism.Navigation;

namespace FilmsManager.ViewModels
{
	public class MovieListContentViewModel : BaseViewModel
	{
		private MovieModel _selectedMovie;

		public string BackgroundImage { get; set; } = AppImages.BackgroundImageHome;

		public ObservableCollection<MovieModel> MovieList = new ObservableCollection<MovieModel>();

		public ObservableCollection<GenreModel> GenreList = new ObservableCollection<GenreModel>();

		public MovieModel SelectedMovie
		{
			get => _selectedMovie;
			set { SetProperty(ref _selectedMovie, value); }
		}

		protected readonly IRestService _restService;

		protected readonly IGenreModelManager _genreModelManager;

		public ICommand DeleteFilmCommand { get; set; }

		public ICommand RefreshCommand { get; set; }

		public ICommand ShowDetailsCommand { get; set; }

		public INotifyTaskCompletion InitializationNotifier { get; protected set; }

		public Task Initialization => InitializationNotifier.Task;

		public MovieListContentViewModel(INavigationService navigationService, IRestService restService, IGenreModelManager genreModelManager) : base(navigationService)
		{
			_restService = restService;
			_genreModelManager = genreModelManager;
			DeleteFilmCommand = new DelegateCommand<MovieModel>(async (movie) => await OnDeleteFilmAsync(movie));
			ShowDetailsCommand = new DelegateCommand<MovieModel>(async (movie) => await OnFilmDetailAsync(movie));
			RefreshCommand = new DelegateCommand(async () => await RefreshMovieListAsync());
			InitializationNotifier = NotifyTaskCompletion.Create(RetrieveMovieListAsync());
			GenreList = GenerateGenreList();
		}

		protected async Task OnDeleteFilmAsync(MovieModel movie)
		{
			if (movie == null)
				return;

			await _restService.DeleteToDoItemAsync(movie.Id);
			await RetrieveMovieListAsync();
		}

		protected async Task RetrieveMovieListAsync()
		{
			var list = await _restService.RefreshDataAsync();
			var movies = new ObservableCollection<MovieModel>();
			foreach (MovieItem item in list)
			{
				movies.Add(new MovieModel(item.Id, item.Title, item.Genre, item.Image));
			}
			MovieList = new ObservableCollection<MovieModel>(movies.OrderBy(m => m.Title));
		}

		protected async Task OnFilmDetailAsync(MovieModel movie)
		{
			if (movie == null)
				return;
			var parameters = new NavigationParameters
			{
				{ "movie", movie }
			};
			await NavigationService.NavigateAsync(nameof(FilmDetailsPage), parameters);
		}

		protected virtual async Task RefreshMovieListAsync()
		{
			await RetrieveMovieListAsync();
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

		public override void OnAppearing()
		{
			SelectedMovie = null;
		}
	}
}
