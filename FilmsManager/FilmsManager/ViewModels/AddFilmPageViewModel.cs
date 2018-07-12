﻿using FilmsManager.Events;
using FilmsManager.Models;
using FilmsManager.Resources;
using FilmsManager.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FilmsManager.ViewModels
{
	public class AddFilmPageViewModel : BaseViewModel
	{
		private IList<GenreModel> _genreList;
		private string _defaultMovieImage = AppImages.Movie;
		private object _movieImage;
		private string _movieTitle;
		private GenreModel _selectedGenre;

		public string BackgroundImage { get; set; } = AppImages.BackgroundImageAddFilm;

		public string CheckButonIcon { get; set; } = AppImages.Check;

		public ICommand AddCommand { get; set; }
		public ICommand OpenGalleryCommand { get; set; }

		public IList<GenreModel> GenreList
		{
			get => _genreList;
			set { SetProperty(ref _genreList, value); }
		}

		public IList<MovieModel> MovieList { get; set; }

		private readonly IPageDialogService _pageDialogService;

		private readonly IEventAggregator _eventAggregator;

		

		public string MovieTitle
		{
			get => _movieTitle;
			set { SetProperty(ref _movieTitle, value); }
		}

		public GenreModel SelectedGenre
		{
			get => _selectedGenre;
			set { SetProperty(ref _selectedGenre, value); }
		}

		public object MovieImage
		{
			get => _movieImage;
			set { SetProperty(ref _movieImage, value); }
		}

		public AddFilmPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService) : base(navigationService)
		{
			Title = AppResources.AddFilmPageTitle;
			_movieImage = _defaultMovieImage;
			_eventAggregator = eventAggregator;
			AddCommand = new DelegateCommand(async () => await OnAddAsync());
			OpenGalleryCommand = new DelegateCommand(async () => await OnOpenGalleryAsync());
			_eventAggregator.GetEvent<PickImageEvent>().Subscribe(OnPickImage);
			_pageDialogService = pageDialogService;
		}

		public override void OnNavigatedTo(NavigationParameters parameters)
		{
			if (parameters == null || parameters.Count==0)
				return;

			MovieList = GetNavigationParameter(parameters, "movieList", MovieList) as ObservableCollection<MovieModel>;

			GenreList = GetNavigationParameter(parameters, "genreList", GenreList) as ObservableCollection<GenreModel>;
		}

		private void OnPickImage(PickImageModel imageModel)
		{
			MovieImage = imageModel?.ImageName;
		}

		private async Task OnOpenGalleryAsync()
		{
			await NavigationService.NavigateAsync(nameof(PickImagePage), useModalNavigation: true);
		}

		private async Task OnAddAsync()
		{
			if (MovieTitle == null | SelectedGenre == null)
			{
				bool action = await _pageDialogService.DisplayAlertAsync(AppResources.MissingEntriesTitle, AppResources.MissingEntriesMessage, AppResources.MissingEntriesOkButton, AppResources.MissingEntriesCancelButton)	;
				if (action) await NavigationService.GoBackAsync();
			}
			else if (MovieImage as string == _defaultMovieImage)
			{
				bool action = await _pageDialogService.DisplayAlertAsync(AppResources.AddFilmDefaultImageTitle, AppResources.AddFilmDefaultImageMessage, AppResources.AddFilmDefaultImageOkButton, AppResources.AddFilmDefaultImageCancelButton);
				if (action)
				{
					MovieList.Add(new MovieModel(MovieTitle, SelectedGenre, MovieImage));
					await NavigationService.GoBackAsync();
				}
			}
			else
			{
				MovieList.Add(new MovieModel(MovieTitle, SelectedGenre, MovieImage));
				await NavigationService.GoBackAsync();
			}

		}

		public override async Task<bool> OnBackButtonPressedAsync()
		{
			bool action = await _pageDialogService.DisplayAlertAsync(AppResources.AddFilmAbortTitle, AppResources.AddFilmAbortMessage, AppResources.AddFilmAbortOkButton, AppResources.AddFilmAbortCancelButton);
			if (action) await NavigationService.GoBackAsync();
			return action;
		}

	}
}
