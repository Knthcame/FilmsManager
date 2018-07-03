using FilmsManager.ViewModels;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PickImagePage : BasePage
	{
		public PickImagePage () : base()
		{
			InitializeComponent ();
		}

		protected override bool OnBackButtonPressed()
		{
			var bindingContext = BindingContext as PickImagePageViewModel;
			bindingContext?.OnBackButtonPressedAsync();
			return true;
		}
	}
}