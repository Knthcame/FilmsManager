using FilmsManager.Managers.Interfaces;
using FilmsManager.Resources;
using Models.Classes;
using Models.Resources;
using Prism.Navigation;

namespace FilmsManager.ViewModels
{
    public class FilmDetailsPageViewModel : BaseViewModel
	{
		private MovieModel _movie;

		public string BackgroundImage { get; set; } = AppImages.BackgroundImageHome;

		public MovieModel Movie
		{
			get => _movie;
			set { SetProperty(ref _movie, value); }
		}

		public FilmDetailsPageViewModel(INavigationService navigationService, IHttpManager httpManager) : base(navigationService, httpManager)
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
