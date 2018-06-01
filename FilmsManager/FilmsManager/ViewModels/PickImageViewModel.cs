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
        private PickImageModel _selectedImage;

        public ICommand PickImageCommand { get; set; }

        public ObservableCollection<PickImageModel> ImageList { get; set; }

		private readonly INavigationService _navigationService;

        public PickImageModel SelectedImage
        {
            get => _selectedImage;
            set
            {
                _selectedImage = value;
                RaisePropertyChanged();

                if(_selectedImage != null)
                {
                    NavigationService.GoBack();
                }
            }
        }

        public PickImageViewModel(INavigationService navigationService)
        {
            PickImageCommand = new PickImageCommand(NavigationService);
			_navigationService = NavigationService;
            LoadImages();
        }

        private void LoadImages() => ImageList = new ObservableCollection<PickImageModel>()
        {
            new PickImageModel()
            {
                Image = "Shrek.png"
            },
            new PickImageModel()
            {
                Image = "Shrek2.png"
            },
            new PickImageModel()
            {
                Image = "Shrek3.png"
            },
			new PickImageModel()
			{
				Image = "infinity_war.png"
			},
			new PickImageModel()
			{
				Image = "HarryPotter.png"
			},
			new PickImageModel()
			{
				Image = "LOTR.png"
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
