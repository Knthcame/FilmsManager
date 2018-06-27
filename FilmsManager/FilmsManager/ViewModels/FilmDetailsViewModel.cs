using FilmsManager.Models;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmsManager.ViewModels
{
	public class FilmDetailsViewModel : BaseViewModel
	{
		private MovieModel _movie;

		public MovieModel Movie
		{
			get => _movie;
			set
			{
				_movie = value;
				RaisePropertyChanged();
			}
		}

		public FilmDetailsViewModel(INavigationService navigationService, MovieModel movie):base(navigationService)
		{
			Movie = movie;
		}

		public virtual async void OnBackButtonPressedAsync()
		{
			await NavigationService.GoBackAsync();
		}
	}
}
