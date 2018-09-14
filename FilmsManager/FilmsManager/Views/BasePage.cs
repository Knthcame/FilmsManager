using FilmsManager.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace FilmsManager.Views
{
	public class BasePage : ContentPage
    {
		public BasePage() : base()
		{
			On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
		}

		protected override void OnAppearing()
		{
			var bindingContext = BindingContext as BaseViewModel;
			bindingContext?.OnAppearingAsync();
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
