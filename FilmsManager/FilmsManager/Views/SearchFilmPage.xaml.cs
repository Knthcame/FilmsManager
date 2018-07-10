using FilmsManager.ViewModels;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
	public partial class SearchFilmPage : BasePage
	{
		public SearchFilmPage () : base()
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