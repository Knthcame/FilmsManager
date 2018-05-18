using FilmsManager.Services.Interfaces;

namespace FilmsManager.ViewModels
{
    public class BaseViewModel : PropertyChangedImpl
    {
        protected INavigationService NavigationService = App.NavigationService;
    }
}