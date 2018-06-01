using FilmsManager.Models;
using FilmsManager.ViewModels.Commands;
using System.Collections.ObjectModel;

namespace FilmsManager.ViewModels
{
	public class SearchFilmViewModel : BaseViewModel
	{
		private string _textEntry;
		private ObservableCollection<MovieModel> _filteredMovieList;

		public string TextEntry
		{
			get => _textEntry;
			set
			{
				_textEntry = value;
				RaisePropertyChanged();
			}
		}
		public ObservableCollection<MovieModel> MovieList { get; set; }

		public ObservableCollection<MovieModel> FilteredMovieList
		{
			get => _filteredMovieList; set
			{
				_filteredMovieList = value;
				RaisePropertyChanged();
			}
		}

		public SearchFilmCommand SearchFilmCommand { get; set; }

		public SearchFilmViewModel(ObservableCollection<MovieModel> movieList)
		{
			MovieList = movieList;
			SearchFilmCommand = new SearchFilmCommand(this);
		}
	}
}
