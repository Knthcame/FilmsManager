using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using Models.Managers.Interfaces;
using Models.Managers;
using FilmsManager.ResxLocalization;
using FilmsManager.Resources;
using FilmsManager.Views;
using FilmsManager.Logging;
using FilmsManager.Logging.Interfaces;
using FilmsManager.Services.Interfaces;
using FilmsManager.Services;

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

			await NavigationService.NavigateAsync("NavigationPage/"+ nameof(HomePage));
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
            containerRegistry.RegisterInstance<IGenreModelManager>(new GenreModelManager());
			containerRegistry.Register<IRestService, RestService>();
            containerRegistry.Register<ICustomLogger, CustomLogger>();
            containerRegistry.Register<IUrlService, UrlService>();
            containerRegistry.Register<IHttpService, HttpService>();
            //containerRegistry.Register<IDatabase, Database>();

        }
    }
}
