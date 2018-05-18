using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FilmsManager.ViewModels
{

    public class PickImageViewModel : BaseViewModel
    {
        private ICommand PickImageCommand { get; set; }

        public ObservableCollection<PickImageModel> ImageList { get; set; }

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

    internal class PickImageCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private INavigationService _navigationService;
        private string _image;

        public PickImageCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            _image = (string) parameter;
            _navigationService.GoBack();
        }
    }
}
