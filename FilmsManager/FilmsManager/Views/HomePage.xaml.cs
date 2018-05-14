using FilmsManager.ViewModels;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        static ObservableCollection<Movie> movieList = new ObservableCollection<Movie>();
        public HomePage ()
		{
			InitializeComponent ();
            BindingContext = new HomeViewModel();
            MovieView.ItemsSource = movieList;
            //movieList.Add(item: new Movie { MovieTitle = "Placeholder", MovieGenre = "Placeholder", MovieIcon=ImageSource.FromFile("icon.png") });
        }

        async public void OnAddButtonPressed()
        {
            await Navigation.PushAsync(new AddFilmPage());
        }

        public static void AddMovie(Movie addingMovie)
        {
            movieList.Add(addingMovie);
        }
	}
}