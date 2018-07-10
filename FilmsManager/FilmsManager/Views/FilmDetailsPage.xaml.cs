using FilmsManager.ViewModels;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
	public partial class FilmDetailsPage : BasePage
	{
		public FilmDetailsPage () : base()
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