using FilmsManager.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FilmsManager.ViewModels.Commands
{
	public class SearchFilmCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;
		private readonly SearchFilmViewModel _viewModel;

		public SearchFilmCommand(SearchFilmViewModel viewModel)
		{
			_viewModel = viewModel;
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			string text = (string) parameter;
			switch (_viewModel.SearchType)
			{
				case "Title":
					_viewModel.FilteredMovieList = new ObservableCollection<MovieModel>(_viewModel.MovieList.Where(m => m.Title.Contains(text)));
			break;
				case "Genre":
					_viewModel.FilteredMovieList = new ObservableCollection<MovieModel>(_viewModel.MovieList.Where(m => m.Genre.Equals(text)));
			break;
			}	
		}
	}
}