using Models.Resources;
using Models.Classes;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using FilmsManager.Resources;
using FilmsManager.Models;
using FilmsManager.Views;
using Models.ApiServices.Interfaces;
using FilmsManager.Events;
using System;
using Prism.Logging;
using FilmsManager.Logging.Interfaces;

namespace FilmsManager.ViewModels
{
    public class AddFilmPageViewModel : BaseViewModel
	{
        #region Properties
        private IList<GenreModel> _genreList;
		private string _defaultMovieImage = AppImages.Movie;
		private object _movieImage;
		private string _movieTitle;
		private GenreModel _selectedGenre;
		private bool _missingTitle;
		private bool _missingGenre;
        private bool _isAddingMovie = false;
		private Color _chooseFilmButtonBorderColor = Color.Black;
        private object _addingMovieLock = new Object();
        private readonly ICustomLogger _logger;

		public string BackgroundImage { get; set; } = AppImages.BackgroundImageAddFilm;

		public string CheckButonIcon { get; set; } = AppImages.Check;

		public ICommand AddCommand { get; set; }
		public ICommand OpenGalleryCommand { get; set; }

		public IList<GenreModel> GenreList
		{
			get => _genreList;
			set { SetProperty(ref _genreList, value); }
		}

		private readonly IPageDialogService _pageDialogService;

		private readonly IEventAggregator _eventAggregator;

		private readonly IRestService _restService;

		public string MovieTitle
		{
			get => _movieTitle;
			set
			{
				SetProperty(ref _movieTitle, value);
				MissingTitle = false;
			}
		}

		public GenreModel SelectedGenre
		{
			get => _selectedGenre;
			set
			{
				SetProperty(ref _selectedGenre, value);
				MissingGenre = false;
			}
		}

		public object MovieImage
		{
			get => _movieImage;
			set { SetProperty(ref _movieImage, value); }
		}

		public bool MissingTitle
		{
			get => _missingTitle;
			set { SetProperty(ref _missingTitle, value); }
		}

		public bool MissingGenre
		{
			get => _missingGenre;
			set { SetProperty(ref _missingGenre, value); }
		}

		public Color ChooseFilmButtonBorderColor
		{
			get => _chooseFilmButtonBorderColor;
			set { SetProperty(ref _chooseFilmButtonBorderColor, value); }
		}
        #endregion

        public AddFilmPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService, IRestService restService, ICustomLogger logger) : base(navigationService)
		{
			Title = AppResources.AddFilmPageTitle;
			_movieImage = _defaultMovieImage;
			_eventAggregator = eventAggregator;
			_restService = restService;
            _logger = logger;
			AddCommand = new DelegateCommand(async () => await OnAddAsync());
			OpenGalleryCommand = new DelegateCommand(async () => await OnOpenGalleryAsync());
			_eventAggregator.GetEvent<PickImageEvent>().Subscribe(OnPickImage);
			_pageDialogService = pageDialogService;
		}

		public override void OnNavigatedTo(NavigationParameters parameters)
		{
			if (parameters == null || parameters.Count == 0)
				return;

			GenreList = GetNavigationParameter(parameters, "genreList", GenreList) as ObservableCollection<GenreModel>;
		}

		private void OnPickImage(PickImageModel imageModel)
		{
            if (imageModel == null)
            {
                _logger.Log("Picked image is null", Category.Exception, Priority.High);
                return;
            }

			ChooseFilmButtonBorderColor = Color.Black;
			MovieImage = imageModel.ImageName;
            _logger.Log($"Image succesfully picked {MovieImage}", Category.Info, Priority.Low);
		}

		private async Task OnOpenGalleryAsync()
		{
            _logger.Log("Opening Gallery to choose an image", Category.Info, Priority.Low);
			await NavigationService.NavigateAsync(nameof(PickImagePage), useModalNavigation: true);
		}

		private async Task OnAddAsync()
		{
            lock (_addingMovieLock)
            {
                if (_isAddingMovie)
                    return;

                _isAddingMovie = true;
            }

            _logger.Log("Add film button pressed", Category.Info, Priority.Low);

            if (MovieTitle == null | SelectedGenre == null)
            {
                bool action = await _pageDialogService.DisplayAlertAsync(AppResources.MissingEntriesTitle, AppResources.MissingEntriesMessage, AppResources.MissingEntriesOkButton, AppResources.MissingEntriesCancelButton);
                if (action)
                    await NavigationService.GoBackAsync();
                else
                {
                    if (MovieTitle == null)
                        MissingTitle = true;
                    if (SelectedGenre == null)
                        MissingGenre = true;
                    
                    _logger.Log("Missing inputs", Category.Warn, Priority.Medium);
                }
            }
            else if (MovieImage as string == _defaultMovieImage)
            {
                bool action = await _pageDialogService.DisplayAlertAsync(AppResources.AddFilmDefaultImageTitle, AppResources.AddFilmDefaultImageMessage, AppResources.AddFilmDefaultImageOkButton, AppResources.AddFilmDefaultImageCancelButton);
                if (action)
                {
                    await SaveNewFilm();
                }
                else ChooseFilmButtonBorderColor = Color.Red;
            }
            else
            {
                await SaveNewFilm();
            }
            _isAddingMovie = false;
		}

		private async Task SaveNewFilm()
		{
            MovieModel item = new MovieModel(null, MovieTitle, SelectedGenre, MovieImage);
            _logger.Log($"Added new film:", item, Category.Info, Priority.Medium);
			await _restService.SaveEntityAsync<MovieModel>(item, true);
			_eventAggregator.GetEvent<AddFilmEvent>().Publish();
			await NavigationService.GoBackAsync();
		}

		public override async Task<bool> OnBackButtonPressedAsync()
		{
			bool action = await _pageDialogService.DisplayAlertAsync(AppResources.AddFilmAbortTitle, AppResources.AddFilmAbortMessage, AppResources.AddFilmAbortOkButton, AppResources.AddFilmAbortCancelButton);
			if (action) await NavigationService.GoBackAsync();
			return action;
		}

	}
}
