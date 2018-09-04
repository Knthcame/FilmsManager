using System.Collections.Generic;
using System.Threading.Tasks;
using FilmsManager.Constants;
using FilmsManager.Events;
using FilmsManager.Models;
using Prism.Commands;
using Prism.Events;
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

		private readonly IEventAggregator _eventAggregator;

		public LanguageSelectionPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator) : base(navigationService)
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
