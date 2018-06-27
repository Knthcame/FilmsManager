using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FilmsManager.ViewModels.Commands
{
	public class SearchCommand : ICommand
	{
		private readonly INavigationService _navigationService;

		public SearchCommand(INavigationService navigationService)
		{
			_navigationService = navigationService;
		}
		
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			//ObservableCollection<MovieModel> MovieList = parameter as ObservableCollection<MovieModel>;
			//if (MovieList != null)
			//{
			//	if (MovieList.Count == 0)
			//	{
			//		return false;
			//	}
			//	else return true;
			//}
			//else return false;
			return true;
		}

		public void Execute(object parameter)
		{
			var viewModel = parameter as HomeViewModel;
			_navigationService.NavigateAsync("SearchFilmPage");
		}
	}
}
