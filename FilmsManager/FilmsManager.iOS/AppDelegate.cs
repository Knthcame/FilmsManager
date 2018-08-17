using FilmsManager.iOS.Logging;
using FilmsManager.iOS.ResxLocalization;
using FilmsManager.Logging;
using FilmsManager.ResxLocalization;
using Foundation;
using Prism;
using Prism.Ioc;
using UIKit;
using Xamarin.Forms;

namespace FilmsManager.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			Forms.Init();

			DependencyService.Register<Localize_iOS>();

			//var container
			var application = new App(new iOSInitializer());

            LoadApplication(application);

            return base.FinishedLaunching(app, options);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
			container.Register<ILocalize, Localize_iOS>();
            container.Register<ILogger, MyiOSLogger>();
        }
    }
}
