using FilmsManager.Models;
using FilmsManager.ViewModels;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        static ObservableCollection<MovieModel> movieList = new ObservableCollection<MovieModel>();
        public HomePage ()
		{
			InitializeComponent ();
            BindingContext = new HomeViewModel();
            MovieView.ItemsSource = movieList;
            //movieList.Add(item: new MovieModel { Title = "Placeholder", Genre = "Placeholder", Image=ImageSource.FromFile("icon.png") });
        }

        async public void OnAddButtonPressed()
        {
            await Navigation.PushAsync(new AddFilmPage());
        }

        public static void AddMovie(MovieModel addingMovie)
        {
            movieList.Add(addingMovie);
        }
	}
}