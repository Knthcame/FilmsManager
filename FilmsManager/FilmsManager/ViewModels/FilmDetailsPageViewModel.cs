using FilmsManager.Models;
using FilmsManager.Resources;
using Prism.Navigation;

namespace FilmsManager.ViewModels
{
	public class FilmDetailsPageViewModel : BaseViewModel
	{
		private MovieModel _movie;

		public MovieModel Movie
		{
			get => _movie;
			set { SetProperty(ref _movie, value); }
		}

		public FilmDetailsPageViewModel(INavigationService navigationService) : base(navigationService)
		{
			Title = AppResources.FilmDetailsPageTitle;
		}

		public override void OnNavigatedTo(NavigationParameters parameters)
		{
			if (parameters == null)
				return;

			parameters.TryGetValue("movie", out MovieModel movie);
			Movie = movie;
		}
	}
}
