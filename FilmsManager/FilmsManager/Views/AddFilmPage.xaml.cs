using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using FilmsManager.ViewModels;
using FilmsManager.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddFilmPage : ContentPage
	{
		public AddFilmPage(ObservableCollection<MovieModel> MovieList)
		{
			InitializeComponent();
			MessagingCenter.Subscribe<AddCommand>(this, "Incomplete", async (sender) =>
			{
				await DisplayAlert("Error!",
				"Not all values have been inserted",
				"OK");
			}
			);
            BindingContext = new AddFilmViewModel(MovieList);
            //AddingMovie.Title = Title.Text;
        }
    }
}