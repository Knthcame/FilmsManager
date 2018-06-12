using FilmsManager.Models;
using FilmsManager.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FilmsManager.ViewModels
{
	public class HomeViewModel : BaseViewModel
	{
        public ObservableCollection<MovieModel> MovieList { get; set; } = new ObservableCollection<MovieModel> {
			new MovieModel("Shrek","Humour", "Shrek.png"),
			new MovieModel("Shrek 2", "Humour", "Shrek2.png"),
			new MovieModel("Infinity war", "Super Heroes", "infinity_war.png")
		};

		public static ObservableCollection<GenreModel> GenreList { get; set; } = new ObservableCollection<GenreModel>()
		{
			new GenreModel("Choose genre:"),
			new GenreModel("Fantasy"),
			new GenreModel("Action"),
			new GenreModel("Drama"),
			new GenreModel("Humour"),
			new GenreModel("Terror"),
			new GenreModel("ScieneFiction"),
			new GenreModel("Super Heroes")
		};

		public ICommand NavigateCommand { get; set; }
		public ICommand SearchCommand { get; set; }

        public HomeViewModel ()
		{
            NavigateCommand = new NavigateCommand(NavigationService, MovieList);
			SearchCommand = new SearchCommand(NavigationService);
        }
    }
}