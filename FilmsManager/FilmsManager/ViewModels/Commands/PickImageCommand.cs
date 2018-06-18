using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace FilmsManager.ViewModels.Commands
{
    public class PickImageCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly INavigationService _navigationService;
        private string _image;

        public PickImageCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
			PickImageModel model =(PickImageModel) parameter;
			_image = model.ImageName;
			MessagingCenter.Send(this, "PickImage", _image);
			_navigationService.GoBack();
        }
    }
}
