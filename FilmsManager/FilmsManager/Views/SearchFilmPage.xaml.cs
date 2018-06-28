using FilmsManager.Models;
using FilmsManager.ViewModels;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchFilmPage : ContentPage
	{
		public SearchFilmPage ()
		{
			InitializeComponent ();
		}

		protected override void OnAppearing()
		{
			var bindingContext = BindingContext as SearchFilmPageViewModel;
			bindingContext?.OnAppearing();
			base.OnAppearing();
		}
	}
}