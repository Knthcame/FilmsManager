using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using FilmsManager.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddFilmPage : ContentPage
	{

        public AddFilmPage(INavigationService navigationService)
		{
			InitializeComponent();
            BindingContext = new AddFilmViewModel(navigationService, this);
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