using FilmsManager.Services.Interfaces;

namespace FilmsManager.ViewModels
{
    public class BaseViewModel : PropertyChangedImpl
    {
        INavigationService navigationService = App.NavigationService;
    }
}