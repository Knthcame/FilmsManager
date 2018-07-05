using FilmsManager.Events;
using FilmsManager.Models;
using FilmsManager.Resources;
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

			ObservableCollection<MovieModel> movieList;
			parameters.TryGetValue("movieList", out movieList);
			if ( movieList != null )
				MovieList = movieList;

			ObservableCollection<GenreModel> genreList;
			parameters.TryGetValue("genreList", out genreList);
			if ( genreList != null )
				GenreList = genreList;
		}

		private void OnPickImage(PickImageModel imageModel)
		{
			MovieImage = imageModel.ImageName;
		}

		private async Task OnOpenGalleryAsync()
		{
			await NavigationService.NavigateAsync("PickImagePage", useModalNavigation: true);
		}

		private async Task OnAddAsync()
		{
			if (MovieTitle == null | MovieGenre == null)
			{
				bool action = await _pageDialogService.DisplayAlertAsync(AppResources.MissingEntriesTitle, AppResources.MissingEntriesMessage, AppResources.MissingEntriesOkButton, AppResources.MissingEntriesCancelButton)	;
				if (action) await NavigationService.GoBackAsync();
			}
			else if (MovieImage as string == _defaultMovieImage)
			{
				bool action = await _pageDialogService.DisplayAlertAsync(AppResources.AddFilmDefaultImageTitle, AppResources.AddFilmDefaultImageMessage, AppResources.AddFilmDefaultImageOkButton, AppResources.AddFilmDefaultImageCancelButton);
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

		public override async Task<bool> OnBackButtonPressedAsync()
		{
			bool action = await _pageDialogService.DisplayAlertAsync(AppResources.AddFilmAbortTitle, AppResources.AddFilmAbortMessage, AppResources.AddFilmAbortOkButton, AppResources.AddFilmAbortCancelButton);
			if (action) await NavigationService.GoBackAsync();
			return action;
		}

	}
}
