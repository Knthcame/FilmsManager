using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using FilmsManager.Models;
using FilmsManager.Resources;
using FilmsManager.ResxLocalization;
using Prism.Commands;
using Prism.Navigation;

namespace FilmsManager.ViewModels
{
	public class LanguageSelectionPageViewModel : BaseViewModel
	{
		public DelegateCommand SelectLanguageCommand { get; set; }

		private LanguageModel _selectedLanguage;

		public IList<LanguageModel> LanguageList { get; set; } = new ObservableCollection<LanguageModel>()
		{
			new LanguageModel("English", "en", "uk.png"),
			new LanguageModel("Spanish", "es", "spain.png")
		};
		public LanguageModel SelectedLanguage
		{
			get => _selectedLanguage;
			set { SetProperty( ref _selectedLanguage, value); }
		}

		public LanguageSelectionPageViewModel(INavigationService navigationService) : base(navigationService)
		{
			SelectLanguageCommand = new DelegateCommand(async() => await OnSelectLanguageAsync());
		}

		private async Task OnSelectLanguageAsync()
		{
			if (SelectedLanguage == null)
				return;

			var ci = new CultureInfo(SelectedLanguage.Abreviation);
			AppResources.Culture = ci;
			Xamarin.Forms.DependencyService.Get<ILocalize>().SetCurrentCultureInfo(ci);
			App.RefreshMainPage();
			await NavigationService.GoBackAsync();
		}
	}
}
