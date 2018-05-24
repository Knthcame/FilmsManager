﻿using FilmsManager.Services.Interfaces;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace FilmsManager.ViewModels.Commands
{
    public class PickImageCommand : ICommand
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
            return true;
        }

        public void Execute(object parameter)
        {
            _image = (string) parameter;
            MessagingCenter.Send(this, "tick", _image);
            _navigationService.GoBack();
        }
    }
}
