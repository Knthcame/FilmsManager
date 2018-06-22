﻿using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using FilmsManager.ViewModels.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FilmsManager.ViewModels
{
	public class AddFilmViewModel : BaseViewModel
	{
		private object _movieImage = "icon.png";
		private string _movieGenre;
		private string _movieTitle;
		private GenreModel _selectedGenre;

		public ICommand AddCommand { get; set; }
		public ICommand OpenGalleryCommand { get; set; }

		public IList<GenreModel> GenreList { get; set; } = HomeViewModel.GenreList;

		public GenreModel SelectedGenre
		{
			get => _selectedGenre;
			set
			{
				_selectedGenre = value;
				_movieGenre = _selectedGenre.Name;
			}
		}

		public string MovieTitle
		{
			get => _movieTitle;
			set
			{
				_movieTitle = value;
				RaisePropertyChanged();
			}
		}

		public string MovieGenre
		{
			get => _movieGenre;
			set
			{
				_movieGenre = value;
				RaisePropertyChanged();
			}
		}

		public object MovieImage
		{
			get => _movieImage;
			set
			{
				_movieImage = value;
				RaisePropertyChanged();
			}
		}


		public AddFilmViewModel(ObservableCollection<MovieModel> movieList)
		{
			AddCommand = new AddCommand(NavigationService, this, movieList);
			OpenGalleryCommand = new OpenGalleryCommand(NavigationService);
			MessagingCenter.Subscribe<PickImageCommand, object>(this, "PickImage", (s, a) => MovieImage = a);
		}

		public virtual async Task<bool> OnBackButtonPressedAsync()
		{
			bool action = await DependencyService.Get<INotificationHelper>().ShowDialog("Abort addition?", "Are you sure you want to cancel adding a movie?", "Yes, abort", "No, stay");
			if (action) await NavigationService.GoBack();
			return action;
		}

	}
}
