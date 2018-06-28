﻿using FilmsManager.Events;
using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using Plugin.Media;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
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
		private PickImageModel _selectedImage;

		public ICommand PickImageCommand { get; set; }

		public ICommand PhotoModeCommand { get; set; }

		public ICommand GoBackCommand { get; set; }

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

		public PickImagePageViewModel(INavigationService navigationService, IEventAggregator eventAggregator) : base(navigationService)
		{
			_eventAggregator = eventAggregator;
			PickImageCommand = new DelegateCommand(OnPickImageAsync);
			PhotoModeCommand = new DelegateCommand<string>(OnPhotoMode);
			GoBackCommand = new DelegateCommand(OnGoBack);
			LoadImages();
		}

		private void OnGoBack()
		{
			NavigationService.GoBackAsync();
		}

		private void OnPhotoMode(string mode)
		{
			switch (mode)
			{
				case "Preloaded":
					ListViewVisible = true;
					ButtonsVisible = false;
					break;

				case "Gallery":
					PickGalleryPhotoAsync();
					break;
			}
		}

		private async void PickGalleryPhotoAsync()
		{
			var photo = await CrossMedia.Current.PickPhotoAsync();
			PickImageModel model = new PickImageModel
			{
				ImageName = FileImageSource.FromStream(() =>
				{
					var stream = photo.GetStream();
					return stream;
				})
			};
			PickImageCommand.Execute(model);
		}

		private async void OnPickImageAsync()
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
		public virtual async Task<bool> OnBackButtonPressedAsync()
		{
			bool action = await DependencyService.Get<INotificationHelper>().ShowDialog("Abort image selection?", "Are you sure you want to cancel selecting a picture?", "Yes, abort", "No, stay");
			if (action) await NavigationService.GoBackAsync();
			return action;
		}
	}
}
