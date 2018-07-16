using FilmsManager.Resources;
using FilmsManager.ResxLocalization;
using FilmsManager.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace FilmsManager.Views
{
	public class BasePage : ContentPage
    {
		public BasePage() : base()
		{
			//var ci = localize.GetCurrentCultureInfo();
			//AppResources.Culture = ci; // set the RESX for resource localization
			//localize.SetLocale(ci); // set the Thread for locale-aware methods

			On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
		}

		protected override void OnAppearing()
		{
			var bindingContext = BindingContext as BaseViewModel;
			bindingContext?.OnAppearing();
			base.OnAppearing();
		}

		protected override bool OnBackButtonPressed()
		{
			if (this.GetType().Name == nameof(HomePage))
			{
				return base.OnBackButtonPressed();
			}

			var bindingContext = BindingContext as BaseViewModel;
			bindingContext?.OnBackButtonPressedAsync();
			return true;
		}


	}
}
