using System.Collections.Generic;
using System.Threading.Tasks;
using FilmsManager.Constants;
using FilmsManager.Managers.Interfaces;
using FilmsManager.Models;
using Prism.Commands;
using Prism.Navigation;

namespace FilmsManager.ViewModels
{
    public class LanguageSelectionPageViewModel : BaseViewModel
	{
		private LanguageModel _selectedLanguage;

        public DelegateCommand SelectLanguageCommand { get; set; }

        public DelegateCommand GoBackCommand { get; set; }

        public IList<LanguageModel> LanguageList { get; set; } = LanguageConstants.SupportedLanguageModels;

		public LanguageModel SelectedLanguage
		{
			get => _selectedLanguage;
			set { SetProperty( ref _selectedLanguage, value); }
		}

		public LanguageSelectionPageViewModel(INavigationService navigationService, IHttpManager httpManager) : base(navigationService, httpManager)
		{
			SelectLanguageCommand = new DelegateCommand(async() => await OnSelectLanguageAsync());
            GoBackCommand = new DelegateCommand(async() => await OnBackButtonPressedAsync());
		}

		private async Task OnSelectLanguageAsync()
		{
			if (SelectedLanguage == null)
				return;
            
			SetAppLanguage(SelectedLanguage);
			await NavigationService.GoBackAsync();
		}
    }
}
