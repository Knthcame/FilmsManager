using FilmsManager.Models;
using FilmsManager.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddFilmPage : ContentPage
	{
        MovieModel AddingMovie = new MovieModel();

        public AddFilmPage()
		{
			InitializeComponent();
            BindingContext = new AddFilmViewModel();
            AddingMovie.Image = "icon.png";
		}

        public async Task OnCheckButtonPressed()
        {
            if(AddingMovie.Title==null | AddingMovie.Genre == null)
            {
                await DisplayAlert("Error!",
                "Not all values have been inserted",
                "OK");
                return;
            }
            HomeViewModel.AddMovie(AddingMovie);
            await Navigation.PopAsync();
        }

        void OnTitleAdded()
        {
            //AddingMovie.Title = Title.Text;
        }

        void OnGenreAdded()
        {
            //AddingMovie.Genre = Genre.Text;
        }

        void OnImageAdded()
        {

        }
    }
}