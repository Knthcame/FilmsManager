using FilmsManager.Events;
using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using FilmsManager.ViewModels.Commands;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FilmsManager.ViewModels
{
	public class AddFilmViewModel : BaseViewModel
	{
		private object _movieImage = "icon.png";
		private string _movieGenre;
		private string _movieTitle;
		private GenreModel _selectedGenre;

		public ICommand AddCommand { get; set; }
		public ICommand OpenGalleryCommand { get; set; }

		public IList<GenreModel> GenreList { get; set; } = HomeViewModel.GenreList;

		public IList<MovieModel> MovieList { get; set; }

		IPageDialogService _pageDialogService;

		IEventAggregator _ea;

		public GenreModel SelectedGenre
		{
			get => _selectedGenre;
			set
			{
				_selectedGenre = value;
				_movieGenre = _selectedGenre.Name;
			}
		}

		public string MovieTitle
		{
			get => _movieTitle;
			set { SetProperty(ref _movieTitle, value); }
		}

		public string MovieGenre
		{
			get => _movieGenre;
			set { SetProperty(ref _movieGenre, value); }
		}

		public object MovieImage
		{
			get => _movieImage;
			set { SetProperty(ref _movieImage, value); }
		}


		public AddFilmViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ObservableCollection<MovieModel> movieList) : base(navigationService)
		{
			AddCommand = new DelegateCommand(OnAddAsync);
			OpenGalleryCommand = new DelegateCommand(OnOpenGallery);
			_ea.GetEvent<PickImageEvent>().Subscribe(OnPickImage);
			_pageDialogService = pageDialogService;
		}

		private void OnPickImage(PickImageModel imageModel)
		{
			_movieImage = imageModel.ImageName;
		}

		private void OnOpenGallery()
		{
			NavigationService.NavigateAsync("PickImagePage");
		}

		private async void OnAddAsync()
		{
			if (MovieTitle == null | MovieGenre == null)
			{
				bool action = await _pageDialogService.DisplayAlertAsync("Missing entries", " Not all values have been inserted. Abort adding film?", "Yes, abort", "No,stay");
				if (action) await NavigationService.GoBackAsync();
			}
			else if (MovieImage as string == "icon.png")
			{
				bool action = await _pageDialogService.DisplayAlertAsync("Default image", "Are you sure you want to go with the default image?", "Yes", "No, i want to choose one");
				if (action)
				{
					MovieList.Add(new MovieModel(MovieTitle, MovieGenre, MovieImage));
					await NavigationService.GoBackAsync();
				}
			}
			else
			{
				MovieList.Add(new MovieModel(MovieTitle, MovieGenre, MovieImage));
				await NavigationService.GoBackAsync();
			}

		}

		public virtual async Task<bool> OnBackButtonPressedAsync()
		{
			bool action = await _pageDialogService.DisplayAlertAsync("Abort addition?", "Are you sure you want to cancel adding a movie?", "Yes, abort", "No, stay");
			if (action) await NavigationService.GoBackAsync();
			return action;
		}

	}
}
