using FilmsManager.ViewModels;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
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