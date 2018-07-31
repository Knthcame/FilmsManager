using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using FilmsManager.Extensions;
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
        #region Properties

        protected MovieModel _selectedMovie;

		public string BackgroundImage { get; set; } = AppImages.BackgroundImageHome;

		public ObservableCollection<MovieModel> MovieList { get; set; } = new ObservableCollection<MovieModel>();

		public ObservableCollection<GenreModel> GenreList { get; set; } = new ObservableCollection<GenreModel>();

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

        public ICommand SelectItemCommand { get; set; }

        public INotifyTaskCompletion InitializationNotifier { get; protected set; }

		public Task Initialization => InitializationNotifier.Task;

        #endregion Properties

        public MovieListContentViewModel(INavigationService navigationService, IRestService restService, IGenreModelManager genreModelManager) : base(navigationService)
		{
			_restService = restService;
			_genreModelManager = genreModelManager;
			DeleteFilmCommand = new DelegateCommand<MovieModel>(async (movie) => await OnDeleteFilmAsync(movie));
			ShowDetailsCommand = new DelegateCommand<MovieModel>(async (movie) => await OnShowDetailAsync(movie));
			RefreshCommand = new DelegateCommand(async () => await RefreshMovieListAsync());
            SelectItemCommand = new DelegateCommand(OnSelectItem);
			InitializationNotifier = NotifyTaskCompletion.Create(RetrieveMovieListAsync());
            InitializationNotifier = NotifyTaskCompletion.Create(GetGenreListAsync());
		}

        private void OnSelectItem()
        {
            SelectedMovie = null;
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
			var movies = await _restService.RefreshDataAsync<MovieModel>();
			MovieList.Clear();
			MovieList.AddRange(movies);
		}

		protected async Task OnShowDetailAsync(MovieModel movie)
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

		public async Task GetGenreListAsync()
		{
            GenreList = await _restService.RefreshDataAsync<GenreModel>() as ObservableCollection<GenreModel>;
            return;
		}

		public override void OnAppearing()
		{
			SelectedMovie = null;
		}
	}
}
