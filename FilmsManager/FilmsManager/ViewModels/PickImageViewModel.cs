using FilmsManager.Models;
using FilmsManager.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FilmsManager.ViewModels
{

    public class PickImageViewModel : BaseViewModel
    {
        private PickImageModel _selectedImage;

        public ICommand PickImageCommand { get; set; }

        public ObservableCollection<PickImageModel> ImageList { get; set; }

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

        public PickImageViewModel()
        {
            PickImageCommand = new PickImageCommand(NavigationService);
            LoadImages();
        }

        private void LoadImages() => ImageList = new ObservableCollection<PickImageModel>()
        {
            new PickImageModel()
            {
                Image = "icon.png"
            },
            new PickImageModel()
            {
                Image = "Check3.png"
            },
            new PickImageModel()
            {
                Image = "Check.png"
            },
            new PickImageModel()
            {
                Image = "Check2.png"
            },
            new PickImageModel()
            {
                Image = "SearchIcon.png"
            },
            new PickImageModel()
            {
                Image = "SearchIcon2.png"
            }
        };
    }
}
