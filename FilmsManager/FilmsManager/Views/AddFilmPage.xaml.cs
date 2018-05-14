using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddFilmPage : ContentPage
	{
        Movie AddingMovie = new Movie();

        public AddFilmPage ()
		{
			InitializeComponent();
            AddingMovie.MovieIcon = "icon.png";
		}

        public async Task OnCheckButtonPressed()
        {
            if(AddingMovie.MovieTitle==null | AddingMovie.MovieGenre == null)
            {
                await DisplayAlert("Error!",
                "Not all values have been inserted",
                "OK");
                return;
            }
            HomePage.AddMovie(AddingMovie);
            await Navigation.PopAsync();
        }

        void OnTitleAdded()
        {
            AddingMovie.MovieTitle = Title.Text;
        }

        void OnGenreAdded()
        {
            AddingMovie.MovieGenre = Genre.Text;
        }

        void OnImageAdded()
        {

        }
    }
}