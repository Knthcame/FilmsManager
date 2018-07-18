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

namespace FilmsManager.ViewModels
{
	public class AddFilmPageViewModel : BaseViewModel
	{
		private IList<GenreModel> _genreList;
		private string _defaultMovieImage = AppImages.Movie;
		private object _movieImage;
		private string _movieTitle;
		private GenreModel _selectedGenre;
		private bool _missingTitle;
		private bool _missingGenre;
		private Color _chooseFilmButtonBorderColor = Color.Black;

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

		public AddFilmPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService, IRestService restService) : base(navigationService)
		{
			Title = AppResources.AddFilmPageTitle;
			_movieImage = _defaultMovieImage;
			_eventAggregator = eventAggregator;
			_restService = restService;
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
			ChooseFilmButtonBorderColor = Color.Black;
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
				bool action = await _pageDialogService.DisplayAlertAsync(AppResources.MissingEntriesTitle, AppResources.MissingEntriesMessage, AppResources.MissingEntriesOkButton, AppResources.MissingEntriesCancelButton);
				if (action)
					await NavigationService.GoBackAsync();
				else
				{
					if (MovieTitle == null)
						MissingTitle = true;
					if (SelectedGenre == null)
						MissingGenre = true;
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
		}

		private async Task SaveNewFilm()
		{
			ToDoItem item = new ToDoItem(null, MovieTitle, SelectedGenre, MovieImage);
			await _restService.SaveToDoItemAsync(item, true);
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
