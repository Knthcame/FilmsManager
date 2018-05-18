using FilmsManager.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PickImagePage : ContentPage
	{
		public PickImagePage ()
		{
			InitializeComponent ();
            BindingContext = new PickImageViewModel();
		}
	}
}