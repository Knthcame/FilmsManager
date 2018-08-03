using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using FilmsManager.Extensions;
using FilmsManager.ResxLocalization;
using FilmsManager.Views;
using Models.ApiServices.Interfaces;
using Models.Classes;
using Models.Managers.Interfaces;
using Models.Resources;
using Nito.AsyncEx;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace FilmsManager.ViewModels
{
    public class MovieListContentViewModel : BaseViewModel
    {
        #region Properties

        protected MovieModel _selectedMovie;

        protected GenreResponse GenreResponse;

        protected ObservableCollection<MovieModel> _movieList = new ObservableCollection<MovieModel>();

        protected ObservableCollection<GenreModel> _genreList = new ObservableCollection<GenreModel>();

        protected bool _isRefreshingMovieList = false;

        protected Task MyTask;
        private bool _connectionError = false;

        public bool IsRefreshingMovieList
        {
            get => _isRefreshingMovieList;
            set { SetProperty(ref _isRefreshingMovieList, value); }
        }

        public string BackgroundImage { get; set; } = AppImages.BackgroundImageHome;

        public ObservableCollection<MovieModel> MovieList
        {
            get => _movieList;
            set { SetProperty(ref _movieList, value); }
        }

        public ObservableCollection<GenreModel> GenreList
        {
            get => _genreList;
            set { SetProperty(ref _genreList, value); }
        }

        protected readonly IRestService _restService;

        protected readonly IGenreModelManager _genreModelManager;

        public ICommand DeleteFilmCommand { get; set; }

        public ICommand RefreshCommand { get; set; }

        public ICommand ShowDetailsCommand { get; set; }

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
            IsRefreshingMovieList = true;
            InitializationNotifier = NotifyTaskCompletion.Create(RefreshMovieListAsync());
            InitializationNotifier = NotifyTaskCompletion.Create(GetGenresAsync());
        }

        protected virtual async Task OnDeleteFilmAsync(MovieModel movie)
        {
            if (movie == null)
                return;

            await _restService.DeleteToDoItemAsync<MovieModel>(movie.Id);
            await RetrieveMovieListAsync();
        }

        protected virtual async Task RetrieveMovieListAsync()
        {
            var movies = await _restService.RefreshDataAsync<MovieModel, IList<MovieModel>>();
            MovieList.Clear();
            MovieList.AddRange(movies);
            IsRefreshingMovieList = false;
            return;
        }

        protected virtual async Task OnShowDetailAsync(MovieModel movie)
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

            while (IsRefreshingMovieList)
                await Task.Delay(100);

            IsRefreshingMovieList = false;
            UpdateMovieListLanguage();
            return;
        }

        public virtual async Task GetGenresAsync()
        {
            GenreResponse = await _restService.RefreshDataAsync<GenreModel, GenreResponse>();
            RefreshGenreList();
            return;
        }

        public virtual void RefreshGenreList()
        {
            var genres = GetGenresInChosenAppLanguage();
            if (genres == null)
                return;
            GenreList = new ObservableCollection<GenreModel>(genres);
        }

        public virtual IList<GenreModel> GetGenresInChosenAppLanguage()
        {
            string culture = Xamarin.Forms.DependencyService.Get<ILocalize>().GetCurrentCultureInfo().EnglishName;
            GenresCultureDictionary.GenresCulture.TryGetValue(culture, out IList<GenreModel> genres);
            return genres;
        }

        public virtual void UpdateMovieListLanguage()
        {
            var movies = new List<MovieModel>();
            foreach (MovieModel movie in MovieList)
            {
                if (GenreList.GetGenre(movie.Genre.Id, out GenreModel genre))
                {
                    movie.Genre = genre;
                }
                movies.Add(new MovieModel(movie.Id, movie.Title, movie.Genre, movie.Image));
            }
            MovieList.Clear();
            MovieList.AddRange(movies);
            return;
        }
    }
}
