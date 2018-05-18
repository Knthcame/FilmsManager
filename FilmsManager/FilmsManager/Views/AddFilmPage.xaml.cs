using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using FilmsManager.ViewModels;
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
            BindingContext = new AddFilmViewModel(MovieList);
            //AddingMovie.Title = Title.Text;
        }
    }
}