using FilmsManager.Models;
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

		public FilmDetailsPageViewModel(INavigationService navigationService) : base(navigationService) { }

		public virtual async void OnBackButtonPressedAsync()
		{
			await NavigationService.GoBackAsync();
		}

		public override void OnNavigatedTo(NavigationParameters parameters)
		{
			if (parameters == null)
				return;

			MovieModel movie;
			parameters.TryGetValue("movie", out movie);
			Movie = movie;
		}
	}
}
