using System.Collections.Generic;
using System.Threading.Tasks;
using FilmsManager.Constants;
using FilmsManager.Events;
using FilmsManager.Managers.Interfaces;
using FilmsManager.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;

namespace FilmsManager.ViewModels
{
    public class LanguageSelectionPageViewModel : BaseViewModel
	{
		private LanguageModel _selectedLanguage;

        private readonly IEventAggregator _eventAggregator;

        public DelegateCommand SelectLanguageCommand { get; set; }

        public DelegateCommand GoBackCommand { get; set; }

        public IList<LanguageModel> LanguageList { get; set; } = LanguageConstants.SupportedLanguageModels;

		public LanguageModel SelectedLanguage
		{
			get => _selectedLanguage;
			set { SetProperty( ref _selectedLanguage, value); }
		}

		public LanguageSelectionPageViewModel(INavigationService navigationService, IHttpManager httpManager, IEventAggregator eventAggregator) : base(navigationService, httpManager)
		{
            _eventAggregator = eventAggregator;
			SelectLanguageCommand = new DelegateCommand(async() => await OnSelectLanguageAsync());
            GoBackCommand = new DelegateCommand(async() => await OnBackButtonPressedAsync());
		}

		private async Task OnSelectLanguageAsync()
		{
			if (SelectedLanguage == null)
				return;

            _eventAggregator.GetEvent<SelectLanguageEvent>().Publish(SelectedLanguage);
			await NavigationService.GoBackAsync();
		}
    }
}
