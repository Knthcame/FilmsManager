using Acr.UserDialogs;
using FilmsManager.Services;
using FilmsManager.Services.Interfaces;
using Plugin.Media;
using Xamarin.Forms;

namespace FilmsManager
{
    public partial class App : Application
	{
        public static INavigationService NavigationService = new CustomNavigationService();

        public App ()
		{
			InitializeComponent();
            NavigationService.Configure("HomePage", typeof(Views.HomePage));
            NavigationService.Configure("AddFilmPage", typeof(Views.AddFilmPage));
            NavigationService.Configure("PickImagePage", typeof(Views.PickImagePage));
			NavigationService.Configure("SearchFilmPage", typeof(Views.SearchFilmPage));
			NavigationService.Configure("FilmDetailsPage", typeof(Views.FilmDetailsPage));
			var mainPage = ((CustomNavigationService)NavigationService).SetRootPage("HomePage");

            MainPage = mainPage;

			CrossMedia.Current.Initialize();
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
