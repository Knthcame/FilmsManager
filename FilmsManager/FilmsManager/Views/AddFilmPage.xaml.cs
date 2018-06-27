using FilmsManager.Models;
using FilmsManager.ViewModels;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddFilmPage : ContentPage
	{
		public static Action AndroidAction { get; set; }

		public AddFilmPage(ObservableCollection<MovieModel> MovieList)
		{
			InitializeComponent();
        }

		protected override bool OnBackButtonPressed()
		{
			var bindingContext = BindingContext as AddFilmPageViewModel;
			bindingContext?.OnBackButtonPressedAsync();
			return true;
		}

		public bool NeedOverrideSoftBackButton { get; set; } = true;

		protected override void OnAppearing()
		{
			if (Device.RuntimePlatform == Device.Android)
			{
				AndroidAction.Invoke();
			}
		}
	}
}