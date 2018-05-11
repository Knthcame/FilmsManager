using FilmsManager.ViewModels;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        ObservableCollection<Movie> movieList = new ObservableCollection<Movie>();
        public HomePage ()
		{
			InitializeComponent ();
            BindingContext = new HomeViewModel();
            MovieView.ItemsSource = movieList;
            movieList.Add(item: new Movie { MovieTitle = "Infinity war", MovieGenre = "Marvel", MovieIcon=ImageSource.FromFile("icon.png") });
        }
	}
}