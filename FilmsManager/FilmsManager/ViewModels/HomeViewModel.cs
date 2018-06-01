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
			new MovieModel("Shrek","xD", "Shrek.png"),
			new MovieModel("Shrek 2", "xD", "Shrek2.png"),
			new MovieModel("Infinity war", "Superheroes", "infinity_war.png")
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