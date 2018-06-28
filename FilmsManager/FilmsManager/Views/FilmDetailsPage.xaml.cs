using FilmsManager.Models;
using FilmsManager.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace FilmsManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilmDetailsPage : ContentPage
	{
		public FilmDetailsPage ()
		{
			InitializeComponent ();

			On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
		}

		protected override bool OnBackButtonPressed()
		{
			var bindingContext = BindingContext as FilmDetailsPageViewModel;
			bindingContext?.OnBackButtonPressedAsync();
			return true;
		}
	}
}