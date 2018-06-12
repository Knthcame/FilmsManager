using FilmsManager.Models;
using FilmsManager.ViewModels.Commands;
using System.Collections.ObjectModel;

namespace FilmsManager.ViewModels
{
	public class SearchFilmViewModel : BaseViewModel
	{
		private string _textEntry;
		private ObservableCollection<MovieModel> _filteredMovieList = new ObservableCollection<MovieModel>();
		private string _alternativeType = "Genre";
		private string _searchType = "Title";
		private bool _searchBarVisible = true;
		private bool _pickerVisible = false;
		private object _selectedGenre;

		public bool PickerVisible
		{
			get => _pickerVisible;
			set
			{
				_pickerVisible = value;
				RaisePropertyChanged();
			}
		}

		public bool SearchBarVisible
		{
			get => _searchBarVisible;
			set
			{
				_searchBarVisible = value;
				RaisePropertyChanged();
			}
		}

		public string AlternativeType
		{
			get => _alternativeType;
			set
			{
				_alternativeType = value;
				RaisePropertyChanged();
			}
		}

		public string SearchType
		{
			get => _searchType;
			set
			{
				_searchType = value;
				RaisePropertyChanged();
			}
		}

		public string TextEntry
		{
			get => _textEntry;
			set
			{
				_textEntry = value;
				RaisePropertyChanged();
			}
		}

		public ObservableCollection<GenreModel> GenreList { get; set; } = HomeViewModel.GenreList;
		public ObservableCollection<MovieModel> MovieList { get; set; }

		public ObservableCollection<MovieModel> FilteredMovieList
		{
			get => _filteredMovieList; set
			{
				_filteredMovieList = value;
				RaisePropertyChanged();
			}
		}

		public object SelectedGenre
		{
			get => _selectedGenre;
			set
			{
				GenreModel aux = value as GenreModel;
				_selectedGenre = aux.Name;
				SearchFilmCommand.Execute(_selectedGenre);
			}
		}

		public SearchFilmCommand SearchFilmCommand { get; set; }

		public SwapSearchCommand SwapSearchCommand { get; set; }

		public SearchFilmViewModel(ObservableCollection<MovieModel> movieList)
		{
			MovieList = movieList;
			SearchFilmCommand = new SearchFilmCommand(this);
			SwapSearchCommand = new SwapSearchCommand(this);
		}
	}
}
