using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddFilmPage : BasePage
	{
		public static Action AndroidAction { get; set; }

		public AddFilmPage() : base()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			if (Device.RuntimePlatform == Device.Android)
			{
				AndroidAction.Invoke();
			}
		}
	}
}