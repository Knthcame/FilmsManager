using FilmsManager.Models;
using FilmsManager.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilmDetailsPage : ContentPage
	{
		public FilmDetailsPage ()
		{
			InitializeComponent ();
		}

		protected override bool OnBackButtonPressed()
		{
			var bindingContext = BindingContext as FilmDetailsPageViewModel;
			bindingContext?.OnBackButtonPressedAsync();
			return true;
		}
	}
}