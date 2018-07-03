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
    }
}
