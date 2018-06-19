using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using System;
using System.Windows.Input;

namespace FilmsManager.ViewModels.Commands
{
	public class FilmDetailsCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		private readonly INavigationService _navigationService;

		public FilmDetailsCommand(INavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			MovieModel movie = parameter as MovieModel;
			_navigationService.NavigateAsync("FilmDetailsPage", movie);
		}
	}
}
