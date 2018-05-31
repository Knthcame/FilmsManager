using FilmsManager.Services;
using FilmsManager.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        public HomePage ()
		{
			InitializeComponent ();
            BindingContext = new HomeViewModel();
			var toolbarItem = new ToolbarItem();
			toolbarItem.BindingContext = this.BindingContext;
			toolbarItem.SetBinding(MenuItem.CommandProperty, new Binding("SearchCommand"));
			toolbarItem.SetBinding(MenuItem.CommandParameterProperty, new Binding("MovieList"));
			toolbarItem.Order = ToolbarItemOrder.Primary;
			toolbarItem.Text = "Search Film";
			toolbarItem.Icon = "search.png";
			this.ToolbarItems.Add(toolbarItem);
		}
		
    }
}