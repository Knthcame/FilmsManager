using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using FilmsManager.ViewModels;
using FilmsManager.ViewModels.Commands;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
			BindingContext = new AddFilmViewModel(MovieList);
        }

		protected override bool OnBackButtonPressed()
		{
			var bindingContext = BindingContext as AddFilmViewModel;
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