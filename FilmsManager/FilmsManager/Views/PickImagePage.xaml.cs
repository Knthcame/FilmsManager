using FilmsManager.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace FilmsManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PickImagePage : ContentPage
	{
		public PickImagePage ()
		{
			InitializeComponent ();

			On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
		}

		protected override bool OnBackButtonPressed()
		{
			var bindingContext = BindingContext as PickImagePageViewModel;
			bindingContext?.OnBackButtonPressedAsync();
			return true;
		}
	}
}