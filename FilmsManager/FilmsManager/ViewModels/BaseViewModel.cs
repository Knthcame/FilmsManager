using FilmsManager.Managers.Interfaces;
using FilmsManager.Models;
using FilmsManager.Resources;
using FilmsManager.ResxLocalization;
using Prism.Mvvm;
using Prism.Navigation;
using System.Globalization;
using System.Threading.Tasks;

namespace FilmsManager.ViewModels
{
    public abstract class BaseViewModel : BindableBase, INavigationAware, IDestructible
	{
		protected INavigationService NavigationService { get; private set; }

		protected string _title;
        protected readonly IHttpManager _httpManager;

        public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}
		
		public BaseViewModel(INavigationService navigationService, IHttpManager httpManager)
		{
			NavigationService = navigationService;
            _httpManager = httpManager;
		}

		public virtual void OnNavigatedFrom(NavigationParameters parameters) { }

		public virtual void OnNavigatedTo(NavigationParameters parameters) { }

		public virtual void OnNavigatingTo(NavigationParameters parameters) { }

		public virtual void Destroy() { }

		public virtual void OnAppearingAsync() { }

		public virtual async Task<bool> OnBackButtonPressedAsync()
		{
			return await NavigationService.GoBackAsync();
		}

		/// <summary>
		/// Tries to get the parameter specified in parameterID and returns it. 
		/// </summary>
		/// <param name="parameters">Parameters passed by the NavigationService</param>
		/// <param name="parameterID">Identification for the desired parameter</param>
		/// <param name="outProperty">If this is specified, this will be the output of the function when the parameterID is not found. It is intented that this is the same property the output will be assigned to</param>
		/// <returns></returns>
		public virtual object GetNavigationParameter(NavigationParameters parameters, string parameterID, object outProperty = null)
		{
			if(parameters.TryGetValue(parameterID, out object aux))
				return aux;
			else
				return outProperty;
		}

        protected void SetAppLanguage(LanguageModel language)
        {
            _httpManager.SaveEntityAsync(language, false);
            var ci = new CultureInfo(language.Abreviation);
            AppResources.Culture = ci;
            Xamarin.Forms.DependencyService.Get<ILocalize>().SetCurrentCultureInfo(ci);
        }
    }
}