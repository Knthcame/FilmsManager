using FilmsManager.Services;
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
		}

		protected override void OnAppearing()
		{
			var bindingContext = BindingContext as HomeViewModel;
			bindingContext?.OnAppearing();
			base.OnAppearing();
		}
	}
}