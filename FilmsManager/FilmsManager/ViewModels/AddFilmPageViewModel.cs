using FilmsManager.Events;
using FilmsManager.Models;
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
		private string _defaultMovieImage = "movie.jpg";
		private object _movieImage;
		private string _movieGenre;
		private string _movieTitle;
		private GenreModel _selectedGenre;

		public string BackgroundImage { get; set; } = "Back3.jpg";

		public ICommand AddCommand { get; set; }
		public ICommand OpenGalleryCommand { get; set; }

		public IList<GenreModel> GenreList
		{
			get => _genreList;
			set { SetProperty(ref _genreList, value); }
		}

		public IList<MovieModel> MovieList { get; set; }

		IPageDialogService _pageDialogService;

		IEventAggregator _eventAggregator;

		public GenreModel SelectedGenre
		{
			get => _selectedGenre;
			set
			{
				_selectedGenre = value;
				_movieGenre = _selectedGenre?.Name;
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


		public AddFilmPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService) : base(navigationService)
		{
			_movieImage = _defaultMovieImage;
			_eventAggregator = eventAggregator;
			AddCommand = new DelegateCommand(OnAddAsync);
			OpenGalleryCommand = new DelegateCommand(OnOpenGalleryAsync);
			_eventAggregator.GetEvent<PickImageEvent>().Subscribe(OnPickImage);
			_pageDialogService = pageDialogService;
		}

		public override void OnNavigatedTo(NavigationParameters parameters)
		{
			if (parameters == null)
				return;

			var movieList = new ObservableCollection<MovieModel>();
			parameters.TryGetValue("movieList", out movieList);
			MovieList = movieList;

			var genreList = new ObservableCollection<GenreModel>();
			parameters.TryGetValue("genreList", out genreList);
			GenreList = genreList;
		}

		private void OnPickImage(PickImageModel imageModel)
		{
			MovieImage = imageModel.ImageName;
		}

		private async void OnOpenGalleryAsync()
		{
			await NavigationService.NavigateAsync("PickImagePage", useModalNavigation: true);
		}

		private async void OnAddAsync()
		{
			if (MovieTitle == null | MovieGenre == null)
			{
				bool action = await _pageDialogService.DisplayAlertAsync("Missing entries", " Not all values have been inserted. Abort adding film?", "Yes, abort", "No,stay");
				if (action) await NavigationService.GoBackAsync();
			}
			else if (MovieImage as string == _defaultMovieImage)
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
