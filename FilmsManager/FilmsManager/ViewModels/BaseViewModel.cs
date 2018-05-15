using FilmsManager.Services.Interfaces;

namespace FilmsManager.ViewModels
{
    public class BaseViewModel : PropertyChangedImpl
    {
        protected INavigationService _navigationService;

        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}