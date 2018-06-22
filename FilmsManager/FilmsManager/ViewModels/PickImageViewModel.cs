using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using FilmsManager.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FilmsManager.ViewModels
{

	public class PickImageViewModel : BaseViewModel
	{

		public double ScreenHeight = Application.Current.MainPage.Height;
		private bool _listViewVisible = false;
		private bool _buttonsVisible = true;

		public ICommand PickImageCommand { get; set; }

		public ICommand PhotoModeCommand { get; set; }

		public ICommand GoBackCommand { get; set; }

		public ObservableCollection<PickImageModel> ImageList { get; set; }

		private readonly INavigationService _navigationService;

		public bool ListViewVisible
		{
			get => _listViewVisible;
			set
			{
				_listViewVisible = value;
				RaisePropertyChanged();
			}
		}

		public bool ButtonsVisible
		{
			get => _buttonsVisible;
			set
			{
				_buttonsVisible = value;
				RaisePropertyChanged();
			}
		}

		public PickImageViewModel() { }

		public PickImageViewModel(INavigationService navigationService)
		{
			PickImageCommand = new PickImageCommand(NavigationService);
			PhotoModeCommand = new PhotoModeCommand(this);
			GoBackCommand = new GoBackCommand(navigationService);
			_navigationService = navigationService;
			LoadImages();
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
			if (action) await _navigationService.GoBack();
			return action;
		}
	}
}
