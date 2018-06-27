using FilmsManager.Services.Interfaces;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FilmsManager.ViewModels.Commands
{
	class GoBackCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		private readonly INavigationService _navigationService;

		public GoBackCommand(INavigationService navigationservice)
		{
			_navigationService = navigationservice;
		}
		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			_navigationService.GoBackAsync();
		}
	}
}
