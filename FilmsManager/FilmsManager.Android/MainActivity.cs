using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.OS;
using Xamarin.Forms;
using FilmsManager.Services.Interfaces;
using FilmsManager.Droid.Helpers;
using Acr.UserDialogs;

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

			UserDialogs.Init(this);

			RegisterHelpers();

            LoadApplication(new App());
        }

		private void RegisterHelpers()
		{
			DependencyService.Register<INotificationHelper, NotificationHelper>();
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
}

