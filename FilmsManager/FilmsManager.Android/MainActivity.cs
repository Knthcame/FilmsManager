using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.OS;
using Xamarin.Forms;
using Prism;
using Prism.Ioc;
using FilmsManager.Droid.ResxLocalization;
using FilmsManager.ResxLocalization;

namespace FilmsManager.Droid
{
	[Activity(Label = "FilmsManager", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

			Views.AddFilmPage.AndroidAction = () =>
			{
				Android.Support.V7.Widget.Toolbar toolbar = this.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
				SetSupportActionBar(toolbar);
			};

			base.OnCreate(bundle);

			Forms.Init(this, bundle);

			DependencyService.Register<Localize_Android>();

			LoadApplication(new App(new AndroidInitializer()));
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if (item.ItemId == 16908332)
			{
				base.OnBackPressed();
				return true;
			}
			else return base.OnOptionsItemSelected(item);
				
		}
	}

	public class AndroidInitializer : IPlatformInitializer
	{
		public void RegisterTypes(IContainerRegistry container)
		{
			container.Register<ILocalize, Localize_Android>();
		}
	}
}

