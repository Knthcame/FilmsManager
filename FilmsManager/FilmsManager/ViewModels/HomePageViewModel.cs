﻿using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using Models.Resources;
using Prism.Events;
using Xamarin.Forms;
using Models.ApiServices.Interfaces;
using Models.Managers.Interfaces;
using FilmsManager.Resources;
using FilmsManager.Views;
using FilmsManager.Events;
using Prism.Services;

namespace FilmsManager.ViewModels
{
    public class HomePageViewModel : MovieListContentViewModel
	{
        #region properties
        private string _titleColumn;

		private string _imageColumn;

		private string _addText;

		private FileImageSource _flag;

		private string _languageAbreviation;

		private string _genreColumn;

		public string SearchToolbarIcon { get; set; } = AppImages.MagnifyingGlass;

		public ICommand NavigateCommand { get; set; }

		public ICommand SearchCommand { get; set; }

		public ICommand LanguageOptionsCommand { get; set; }

        private readonly IPageDialogService _pageDialogService;

        private readonly IEventAggregator _eventAggregator;

		public string LanguageAbreviation
		{
			get { return _languageAbreviation; }
			set { SetProperty(ref _languageAbreviation, value); }
		}

		public FileImageSource Flag
		{
			get { return _flag; }
			set { SetProperty(ref _flag, value); }
		}

		public string AddText
		{
			get { return _addText; }
			set { SetProperty(ref _addText, value); }
		}

		public string ImageColumn
		{
			get { return _imageColumn; }
			set { SetProperty(ref _imageColumn, value); }
		}

		public string TitleColumn
		{
			get { return _titleColumn; }
			set { SetProperty(ref _titleColumn, value); }
		}

		public string GenreColumn
		{
			get { return _genreColumn; }
			set { SetProperty(ref _genreColumn, value); }
		}

        #endregion properties

        public HomePageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IGenreModelManager genreModelManager, IRestService restService, IPageDialogService pageDialogService) : base(navigationService, restService, genreModelManager)
		{
            _pageDialogService = pageDialogService;
			_eventAggregator = eventAggregator;
			_eventAggregator.GetEvent<SelectLanguageEvent>().Subscribe(UpdatePageLanguage);
			_eventAggregator.GetEvent<AddFilmEvent>().Subscribe(async () => await RefreshMovieListAsync());
            _eventAggregator.GetEvent<ConnectionErrorEvent>().Subscribe(async () => await NotifyConnectionErrorAsync());
			NavigateCommand = new DelegateCommand(async () => await OnNavigateAsync());
			SearchCommand = new DelegateCommand(async () => await OnSearchAsync());
			LanguageOptionsCommand = new DelegateCommand(async () => await OnLanguageOptionsAsync());
			LoadResources();
        }

        private async Task NotifyConnectionErrorAsync()
        {
            await _pageDialogService.DisplayAlertAsync("Connection error", "Could not connect with the server at this time", "ok");
            IsRefreshingMovieList = false;
            IsMovieListEmpty = true;
        }

        protected override async Task RefreshMovieListAsync()
		{
			await base.RefreshMovieListAsync();
			IsRefreshingMovieList = false;
		}

		public void LoadResources()
		{
			Title = AppResources.HomePageTitle;
			ImageColumn = AppResources.ImageColumn;
			GenreColumn = AppResources.GenreColumn;
			TitleColumn = AppResources.TitleColumn;
			AddText = AppResources.HomePageAddButton;
            Labeltext = AppResources.NoMoviesLabeltext;
			switch (Device.RuntimePlatform)
			{
				case Device.Android:
					Flag = ImageSource.FromFile(AppResources.Flag) as FileImageSource;
					break;
				case Device.iOS:
					LanguageAbreviation = AppResources.LanguageAbreviation;
					break;
			}
            RefreshGenreList();
		}

        private void UpdatePageLanguage()
        {
            LoadResources();
            UpdateMovieListLanguage();
            return;
        }

		private async Task OnLanguageOptionsAsync()
		{
			await NavigationService.NavigateAsync(nameof(LanguageSelectionPage), useModalNavigation: true);
		}

		private async Task OnSearchAsync()
		{
            if (IsMovieListEmpty)
            {
                await _pageDialogService.DisplayAlertAsync("Error", "There are no movies to search for", "ok");
                return;
            }
                
			await NavigationService.NavigateAsync(nameof(SearchFilmPage));
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