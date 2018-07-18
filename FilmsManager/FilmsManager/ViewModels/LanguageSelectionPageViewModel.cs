using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using FilmsManager.Events;
using FilmsManager.Models;
using FilmsManager.Resources;
using FilmsManager.ResxLocalization;
using Models.Resources;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Xamarin.Forms;

namespace FilmsManager.ViewModels
{
	public class LanguageSelectionPageViewModel : BaseViewModel
	{
		public DelegateCommand SelectLanguageCommand { get; set; }

		private LanguageModel _selectedLanguage;

		public IList<LanguageModel> LanguageList { get; set; } = new ObservableCollection<LanguageModel>()
		{
			new LanguageModel("English", "en", AppImages.UKFlag),
			new LanguageModel("Spanish", "es", AppImages.SpainFlag)
		};
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
		}

		private async Task OnSelectLanguageAsync()
		{
			if (SelectedLanguage == null)
				return;

			var ci = new CultureInfo(SelectedLanguage.Abreviation);
			AppResources.Culture = ci;
			DependencyService.Get<ILocalize>().SetCurrentCultureInfo(ci);
			_eventAggregator.GetEvent<SelectLanguageEvent>().Publish();
			await NavigationService.GoBackAsync();
		}
	}
}
