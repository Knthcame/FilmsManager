using FilmsManager.Events;
using FilmsManager.Models;
using FilmsManager.Resources;
using Plugin.Media;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FilmsManager.ViewModels
{
	public class PickImagePageViewModel : BaseViewModel
	{

		public double ScreenHeight = Application.Current.MainPage.Height;
		private bool _listViewVisible = false;
		private bool _buttonsVisible = true;

		IEventAggregator _eventAggregator;

		IPageDialogService _pageDialogService;

		private PickImageModel _selectedImage;

		public ICommand PickImageCommand { get; set; }

		public ICommand PreloadedPhotoCommand { get; set; }

		public ICommand GoBackCommand { get; set; }

		public ICommand MobileGalleryCommand { get; set; }

		public ObservableCollection<PickImageModel> ImageList { get; set; }

		public PickImageModel SelectedImage
		{
			get => _selectedImage;
			set { SetProperty(ref _selectedImage, value); }
		}

		public bool ListViewVisible
		{
			get => _listViewVisible;
			set { SetProperty(ref _listViewVisible, value); }
		}

		public bool ButtonsVisible
		{
			get => _buttonsVisible;
			set { SetProperty(ref _buttonsVisible, value); }
		}

		public PickImagePageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService) : base(navigationService)
		{
			Title = AppResources.PickImagePageTitle;
			_eventAggregator = eventAggregator;
			_pageDialogService = pageDialogService;
			PickImageCommand = new DelegateCommand(async () => await OnPickImageAsync());
			PreloadedPhotoCommand = new DelegateCommand(OnPreloadedPhoto);
			MobileGalleryCommand = new DelegateCommand(async () => await PickGalleryPhotoAsync());
			GoBackCommand = new DelegateCommand(async () => await OnGoBackAsync());
			LoadImages();
		}

		private async Task OnGoBackAsync()
		{
			await OnBackButtonPressedAsync();
		}

		private void OnPreloadedPhoto()
		{
			ListViewVisible = true;
			ButtonsVisible = false;
		}

		private async Task PickGalleryPhotoAsync()
		{
			if (!CrossMedia.Current.IsPickPhotoSupported)
				return;

			var photo = await CrossMedia.Current.PickPhotoAsync();
			PickImageModel model = new PickImageModel
			{
				ImageName = FileImageSource.FromStream(() =>
				{
					var stream = photo.GetStream();
					return stream;
				})
			};
			_eventAggregator.GetEvent<PickImageEvent>().Publish(model);
			await NavigationService.GoBackAsync();
		}

		private async Task OnPickImageAsync()
		{
			if (SelectedImage == null)
				return;

			_eventAggregator.GetEvent<PickImageEvent>().Publish(SelectedImage);
			await NavigationService.GoBackAsync();
		}

		private void LoadImages() => ImageList = new ObservableCollection<PickImageModel>()
		{
			new PickImageModel()
			{
				ImageName = "Shrek.jpg"
			},
			new PickImageModel()
			{
				ImageName = "Shrek2.jpg"
			},
			new PickImageModel()
			{
				ImageName = "Shrek3.jpg"
			},
			new PickImageModel()
			{
				ImageName = "infinity_war.jpg"
			},
			new PickImageModel()
			{
				ImageName = "HarryPotter.jpg"
			},
			new PickImageModel()
			{
				ImageName = "LOTR.jpg"
			}
		};
		public override async Task<bool> OnBackButtonPressedAsync()
		{
			bool action = await _pageDialogService.DisplayAlertAsync(AppResources.AbortPickImageTitle, AppResources.AbortPickImageMessage, AppResources.AbortPickImageOkText, AppResources.AbortPickImageCancelText);
			if (action) await NavigationService.GoBackAsync();
			return action;
		}
	}
}
