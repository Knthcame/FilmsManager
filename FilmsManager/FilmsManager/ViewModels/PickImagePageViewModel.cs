using FilmsManager.Events;
using FilmsManager.Logging.Interfaces;
using FilmsManager.Models;
using FilmsManager.Resources;
using Models.Resources;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
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
        #region Properties

        public double ScreenHeight = Application.Current.MainPage.Height;
		private bool _listViewVisible = false;
		private bool _buttonsVisible = true;

		private readonly IEventAggregator _eventAggregator;

		private readonly IPageDialogService _pageDialogService;

        private readonly ICustomLogger _logger;

        private PickImageModel _selectedImage;

		public string PreloadedImagesCover { get; set; } = AppImages.Shrek;

		public string OpenMobileGalleryCover { get; set; } = AppImages.Icon;

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

        #endregion

        public PickImagePageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService, ICustomLogger logger) : base(navigationService)
		{
			Title = AppResources.PickImagePageTitle;
			_eventAggregator = eventAggregator;
			_pageDialogService = pageDialogService;
            _logger = logger;
			PickImageCommand = new DelegateCommand(async () => await OnPickImageAsync());
			PreloadedPhotoCommand = new DelegateCommand(OnPreloadedPhoto);
			MobileGalleryCommand = new DelegateCommand(async () => await PickGalleryPhotoAsync());
			GoBackCommand = new DelegateCommand(async () => await OnGoBackAsync());
			LoadImages();
		}

		private async Task OnGoBackAsync()
		{
            _logger.Log("User cancelled selecting a photo", Category.Info, Priority.Low);
			await OnBackButtonPressedAsync();
		}

		private void OnPreloadedPhoto()
		{
			ListViewVisible = true;
			ButtonsVisible = false;
		}

		private async Task PickGalleryPhotoAsync()
		{
            _logger.Log("Selecting image from mobile gallery", Category.Info, Priority.Low);
			Permission permission = Permission.Unknown;
			switch (Device.RuntimePlatform)
			{
				case Device.Android:
					permission = Permission.Storage;
					break;
				case Device.iOS:
					permission = Permission.Photos;
					break;
			}

			var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
			if (status!= PermissionStatus.Granted)
			{
				bool accepted = true;
				if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(permission))
				{
					accepted = await _pageDialogService.DisplayAlertAsync(AppResources.StorageAccesTitle, AppResources.StorageAccesMessage, AppResources.StorageAccesOkButton, AppResources.StorageAccesCancelButton);
				}
				if (accepted)
				{
					var results = await CrossPermissions.Current.RequestPermissionsAsync(permission);
					//Best practice to always check that the key exists
					if (results.ContainsKey(permission))
						status = results[permission];
				}
				
			}
			if (status == PermissionStatus.Denied)
            {
                _logger.Log("User denied storage permission", Category.Info, Priority.Medium);
				return;
            }

            _logger.Log("Storage permission granted", Category.Info, Priority.Medium);

			var photo = await CrossMedia.Current.PickPhotoAsync();
            PickImageModel model = new PickImageModel
            {
                ImageName = photo.Path
			};
            if (photo != null)
            {
                _logger.Log($"Image selected from gallery:", model, Category.Info, Priority.Medium);
                _eventAggregator.GetEvent<PickImageEvent>().Publish(model);
            }
            else
                _logger.Log("Image is null", Category.Exception, Priority.High);

			await NavigationService.GoBackAsync();
		}

		private async Task OnPickImageAsync()
		{
			if (SelectedImage == null)
				return;

            _logger.Log($"Selected preloaded image: {SelectedImage.ImageName}", Category.Info, Priority.Medium);
            _eventAggregator.GetEvent<PickImageEvent>().Publish(SelectedImage);
			await NavigationService.GoBackAsync();
		}

        private void LoadImages() => ImageList = PickImageGallery.DefaultGalleryImages as ObservableCollection<PickImageModel>;

		public override async Task<bool> OnBackButtonPressedAsync()
		{
			bool action = await _pageDialogService.DisplayAlertAsync(AppResources.AbortPickImageTitle, AppResources.AbortPickImageMessage, AppResources.AbortPickImageOkText, AppResources.AbortPickImageCancelText);
			if (action) await NavigationService.GoBackAsync();
			return action;
		}
	}
}
