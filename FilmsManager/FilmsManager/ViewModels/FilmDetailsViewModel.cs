using FilmsManager.Models;
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

		public FilmDetailsViewModel(MovieModel movie)
		{
			Movie = movie;
		}

		public virtual async void OnBackButtonPressedAsync()
		{
			await NavigationService.GoBack();
		}
	}
}
