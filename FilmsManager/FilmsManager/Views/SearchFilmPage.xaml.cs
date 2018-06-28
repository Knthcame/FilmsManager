using FilmsManager.Models;
using FilmsManager.ViewModels;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace FilmsManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchFilmPage : ContentPage
	{
		public SearchFilmPage ()
		{
			InitializeComponent ();

			On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
		}

		protected override void OnAppearing()
		{
			var bindingContext = BindingContext as SearchFilmPageViewModel;
			bindingContext?.OnAppearing();
			base.OnAppearing();
		}
	}
}