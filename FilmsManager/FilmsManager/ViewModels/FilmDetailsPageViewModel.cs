using FilmsManager.Models;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

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

		public FilmDetailsPageViewModel(INavigationService navigationService, MovieModel movie):base(navigationService)
		{
			Movie = movie;
		}

		public virtual async void OnBackButtonPressedAsync()
		{
			await NavigationService.GoBackAsync();
		}
	}
}
