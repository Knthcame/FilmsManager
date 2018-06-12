using FilmsManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FilmsManager.ViewModels.Commands
{
	public class SwapSearchCommand : ICommand
	{
		private SearchFilmViewModel _viewModel { get; set; }

		public SwapSearchCommand(SearchFilmViewModel viewModel)
		{
			_viewModel = viewModel;
		}
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			switch (_viewModel.SearchType)
			{
				case "Title":
					_viewModel.SearchType = "Genre";
					_viewModel.AlternativeType = "Title";
					_viewModel.PickerVisible = true;
					_viewModel.SearchBarVisible = false;
					break;

				case "Genre":
					_viewModel.SearchType = "Title";
					_viewModel.AlternativeType = "Genre";
					_viewModel.PickerVisible = false;
					_viewModel.SearchBarVisible = true;
					break;
			}
			_viewModel.FilteredMovieList.Clear();
		}
	}
}
