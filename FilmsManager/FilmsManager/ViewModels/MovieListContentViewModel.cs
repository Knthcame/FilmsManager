using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FilmsManager.Extensions;
using FilmsManager.Logging.Interfaces;
using FilmsManager.Managers.Interfaces;
using FilmsManager.Resources;
using FilmsManager.ResxLocalization;
using FilmsManager.Views;
using Models.Classes;
using Models.Managers.Interfaces;
using Models.Resources;
using Nito.AsyncEx;
using Prism.Commands;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

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

        private bool _isMovieListEmpty;

        private string _labeltext = AppResources.NoMoviesLabeltext;

        public bool IsRefreshingMovieList
        {
            get => _isRefreshingMovieList;
            set { SetProperty(ref _isRefreshingMovieList, value); }
        }

        public string BackgroundImage { get; set; } = AppImages.BackgroundImageHome;

        public string Labeltext
        {
            get => _labeltext;
            set { SetProperty(ref _labeltext, value); }
        }

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

        protected readonly IGenreModelManager _genreModelManager;

        protected readonly IPageDialogService _pageDialogService;

        protected readonly ICustomLogger _logger;

        public ICommand DeleteFilmCommand { get; set; }

        public ICommand RefreshCommand { get; set; }

        public ICommand ShowDetailsCommand { get; set; }

        public INotifyTaskCompletion InitializationNotifier { get; protected set; }

        public Task Initialization => InitializationNotifier.Task;

        public bool IsMovieListEmpty
        {
            get => _isMovieListEmpty;
            set { SetProperty(ref _isMovieListEmpty, value); }
        }

        #endregion Properties

        public MovieListContentViewModel(INavigationService navigationService, IHttpManager httpManager, IGenreModelManager genreModelManager, IPageDialogService pageDialogService, ICustomLogger logger)
            : base(navigationService, httpManager)
        {
            _genreModelManager = genreModelManager;
            _pageDialogService = pageDialogService;
            _logger = logger;
            DeleteFilmCommand = new DelegateCommand<MovieModel>(async (movie) => await OnDeleteFilmAsync(movie));
            ShowDetailsCommand = new DelegateCommand<MovieModel>(async (movie) => await OnShowDetailAsync(movie));
            RefreshCommand = new DelegateCommand(async () => await RefreshMovieListAsync());
        }

        protected virtual async Task OnDeleteFilmAsync(MovieModel movie)
        {
            _logger.Log("Deleting a film", Category.Info, Priority.Low);
            if (movie == null)
            {
                _logger.Log("Movie parameter is null. Aborted deletion", Category.Exception, Priority.High);
                return;
            }

            if (!await _pageDialogService.DisplayAlertAsync(AppResources.DeleteFilmConfirmationTitle, $"{AppResources.DeleteFilmConfirmationMessage} {movie.Title}", AppResources.DeleteFilmConfirmationOkButton, AppResources.DeleteFilmConfirmationCancelButton))
            {
                _logger.Log("User canceled the deletion", Category.Info, Priority.Medium);
                return;
            }

            await _httpManager.DeleteEntityAsync(movie);
            await RefreshMovieListAsync();

            _logger.Log($"Succesfully deleted film:", movie, Category.Info, Priority.Medium);

            if (MovieList.Count == 0)
            {
                _logger.Log("Movie list is now empty", Category.Info, Priority.Medium);
                IsMovieListEmpty = true;
            }
        }

        protected virtual async Task RetrieveMovieListAsync()
        {
            _logger.Log("Retrieving movie list from server", Category.Info, Priority.Medium);
            var movies = await _httpManager.RefreshDataAsync<MovieModel, List<MovieModel>>();
            //MovieList.Clear();
            MovieList = new ObservableCollection<MovieModel>(movies);
            //MovieList.AddRange(movies);
            UpdateMovieListLanguage();
            _logger.Log("Succesfully got movie list from server", Category.Info, Priority.Medium);
        }

        protected virtual async Task OnShowDetailAsync(MovieModel movie)
        {
            if (movie == null)
                return;

            _logger.Log($"Navigating to {movie.Title} details", Category.Info, Priority.Medium);

            var parameters = new NavigationParameters
            {
                { "movie", movie }
            };
            await NavigationService.NavigateAsync(nameof(FilmDetailsPage), parameters);
        }

        protected virtual async Task RefreshMovieListAsync()
        {
            _logger.Log("Refreshing movie list", Category.Info, Priority.Medium);
            await RetrieveMovieListAsync();

            //while (IsRefreshingMovieList)
            //    await Task.Delay(100);
         
            IsMovieListEmpty = !MovieList.Any();


            _logger.Log("Movie List succesfully refreshed", Category.Info, Priority.Medium);
            _logger.Log($"IsMovieListEmpty = {_isMovieListEmpty}", Category.Info, Priority.Medium);
        }

        public virtual async Task GetGenresAsync()
        {
            _logger.Log("Retrieving genres from server", Category.Info, Priority.Medium);
            GenreResponse = await _httpManager.RefreshDataAsync<GenreModel, GenreResponse>();
            RefreshGenreList();
            _logger.Log("Succesfully retrieved genres", Category.Info, Priority.Medium);
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
            _logger.Log("Translating genres names", Category.Info, Priority.Medium);

            string culture = Xamarin.Forms.DependencyService.Get<ILocalize>().GetCurrentCultureInfo().EnglishName;

            var genres = GenreResponse.GetGenresInChosenLanguage(culture);

            _logger.Log($"Genres translated to {culture}", Category.Info, Priority.Medium);
            return genres;
        }

        public virtual void UpdateMovieListLanguage()
        {
            _logger.Log("Updating movie list language", Category.Info, Priority.Medium);
            var movies = new List<MovieModel>();

            foreach (MovieModel movie in MovieList)
            {
                if (movie.Genre != null)
                {
                    if (GenreList.GetGenre(movie.Genre.Id, out GenreModel genre))
                    {
                        movie.Genre = genre;
                    }
                }
                movies.Add(movie);
            }
            MovieList = new ObservableCollection<MovieModel>(movies);

            _logger.Log("Movie list language updated", Category.Info, Priority.Medium);
        }
    }
}