using FilmsManager.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        public HomePage ()
		{
			InitializeComponent ();
            BindingContext = new HomeViewModel();
            //movieList.Add(item: new MovieModel { Title = "Placeholder", Genre = "Placeholder", Image=ImageSource.FromFile("icon.png") });
        }

        async public void OnAddButtonPressed()
        {
            await Navigation.PushAsync(new AddFilmPage());
        }
    }
}