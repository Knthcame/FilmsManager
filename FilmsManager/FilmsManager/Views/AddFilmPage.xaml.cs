using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddFilmPage : ContentPage
	{
        Movie AddingMovie;

        public AddFilmPage ()
		{
			InitializeComponent ();
            AddingMovie = new Movie();
		}

        public async Task OnCheckButtonPressed()
        {
            if(AddingMovie.MovieTitle==null | AddingMovie.MovieGenre == null)
            {
                await DisplayAlert("Error!",
                "Not all values have been inserted",
                "OK");
            }
        }

        void OnTitleAdded()
        {
            AddingMovie.MovieTitle = Title.Text;
        }

        void OnGenreAdded()
        {
            AddingMovie.MovieGenre = Title.Text;
        }
    }
}