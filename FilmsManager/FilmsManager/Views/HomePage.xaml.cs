using FilmsManager.ViewModels;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : BasePage
	{
        public HomePage() : base()
		{
			InitializeComponent ();
		}

		protected override void OnAppearing()
		{
			var bindingContext = BindingContext as HomePageViewModel;
			bindingContext?.OnAppearing();
			base.OnAppearing();
		}
	}
}