using FilmsManager.Services;
using FilmsManager.Services.Interfaces;
using FilmsManager.Views;
using Xamarin.Forms;

namespace FilmsManager
{
    public partial class App : Application
	{
        public static INavigationService NavigationService = new CustomNavigationService();

        public App ()
		{
			InitializeComponent();
			MainPage = new NavigationPage(new HomePage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
