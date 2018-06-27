using FilmsManager.Services.Interfaces;
using Prism.Navigation;
using System;
using System.Windows.Input;

namespace FilmsManager.ViewModels.Commands
{
    public class OpenGalleryCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly INavigationService _navigationService;

        public OpenGalleryCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _navigationService.NavigateAsync("PickImagePage");
        }
    }
}
