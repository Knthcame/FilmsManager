using Prism.Mvvm;
using Prism.Navigation;

namespace FilmsManager.ViewModels
{
	public class BaseViewModel : BindableBase, INavigationAware, IDestructible
	{
		protected INavigationService NavigationService { get; private set; }

		private string _title;
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}


		public BaseViewModel(INavigationService navigationService)
		{
			NavigationService = navigationService;
		}

		public virtual void OnNavigatedFrom(NavigationParameters parameters)
		{

		}

		public virtual void OnNavigatedTo(NavigationParameters parameters)
		{

		}

		public virtual void OnNavigatingTo(NavigationParameters parameters)
		{

		}

		public virtual void Destroy()
		{

		}

		public virtual void OnAppearing() { }

		public virtual async System.Threading.Tasks.Task<bool> OnBackButtonPressedAsync()
		{
			await NavigationService.GoBackAsync();
			return true;
		}
	}
}