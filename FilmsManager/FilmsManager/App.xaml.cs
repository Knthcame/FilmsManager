using Prism;
using Prism.Ioc;
using FilmsManager.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using FilmsManager.Resources;
using FilmsManager.ResxLocalization;
using System.Globalization;
using Unity;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FilmsManager
{
	public partial class App : PrismApplication
    {
		/// <summary>
		/// The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
		/// This imposes a limitation in which the App class must have a default constructor. 
		/// App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
		/// </summary>
        public App() : this(null) { }

		public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

			if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
			{
				var ci = DependencyService.Get<ILocalize>().GetMobileCultureInfo();
				DependencyService.Get<ILocalize>().SetCurrentCultureInfo(ci); // set the Thread for locale-aware methods
				AppResources.Culture = ci; // set the RESX for resource localization
			}

			await NavigationService.NavigateAsync("NavigationPage/HomePage");
        }

		public static void RefreshMainPage()
		{
			Current.MainPage = new NavigationPage(new HomePage());
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<AddFilmPage>();
			containerRegistry.RegisterForNavigation<PickImagePage>();
			containerRegistry.RegisterForNavigation<SearchFilmPage>();
			containerRegistry.RegisterForNavigation<FilmDetailsPage>();
			containerRegistry.RegisterForNavigation<HomePage>();
			containerRegistry.RegisterForNavigation<LanguageSelectionPage>();
		}
    }
}
