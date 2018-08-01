using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using FilmsManager.Extensions;
using FilmsManager.ResxLocalization;
using FilmsManager.Views;
using Models.ApiServices.Interfaces;
using Models.Classes;
using Models.Constants;
using Models.Managers.Interfaces;
using Models.Resources;
using Nito.AsyncEx;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace FilmsManager.ViewModels
{
    public class MovieListContentViewModel : BaseViewModel
    {
        #region Properties

        protected MovieModel _selectedMovie;
        private GenreResponse GenreResponse;
        private ObservableCollection<MovieModel> _movieList = new ObservableCollection<MovieModel>();

        public string BackgroundImage { get; set; } = AppImages.BackgroundImageHome;

        public ObservableCollection<MovieModel> MovieList
        {
            get => _movieList;
            set { SetProperty( ref _movieList, value); }
        }

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
            try
            {
                _restService = restService;
                _genreModelManager = genreModelManager;
                DeleteFilmCommand = new DelegateCommand<MovieModel>(async (movie) => await OnDeleteFilmAsync(movie));
                ShowDetailsCommand = new DelegateCommand<MovieModel>(async (movie) => await OnShowDetailAsync(movie));
                RefreshCommand = new DelegateCommand(async () => await RefreshMovieListAsync());
                SelectItemCommand = new DelegateCommand(OnSelectItem);
                InitializationNotifier = NotifyTaskCompletion.Create(RetrieveMovieListAsync());
                InitializationNotifier = NotifyTaskCompletion.Create(GetGenresAsync());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("              ERROR {0}", ex.Message);
            }
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
            var movies = await _restService.RefreshDataAsync<MovieModel, IList<MovieModel>>();
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

        public async Task GetGenresAsync()
        {
            GenreResponse = await _restService.RefreshDataAsync<GenreModel, GenreResponse>();
            RefreshGenreList();
            return;
        }

        public void RefreshGenreList()
        {
            string culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo().EnglishName;
            GenresCultureDictionary.GenresCulture.TryGetValue(culture, out IList<GenreModel> genres);
            if (genres == null)
                return;
            GenreList = new ObservableCollection<GenreModel>(genres);
        }

        public override void OnAppearing()
        {
            SelectedMovie = null;
        }
    }
}
