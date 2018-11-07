using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using FilmsManager.Droid.ResxLocalization;
using FilmsManager.Droid.Services;
using FilmsManager.ResxLocalization;
using FilmsManager.Services.Interfaces;
using Plugin.Permissions;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;

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

			Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);

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

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
		{
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}

	public class AndroidInitializer : IPlatformInitializer
	{
		public void RegisterTypes(IContainerRegistry container)
		{
			container.Register<ILocalize, Localize_Android>();
            container.Register<IDatabasePath, DatabasePath>();
		}
	}
}

