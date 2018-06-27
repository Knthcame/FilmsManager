﻿using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FilmsManager.ViewModels.Commands
{
    public class AddCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly INavigationService _navigationService;
        private readonly AddFilmViewModel _viewModel;
        private ObservableCollection<MovieModel> _movieList;

        public AddCommand(INavigationService navigationService, AddFilmViewModel viewModel, ObservableCollection<MovieModel> MovieList)
        {
            _navigationService = navigationService;
            _viewModel = viewModel;
            _movieList = MovieList;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (_viewModel.MovieTitle == null | _viewModel.MovieGenre == null)
            {
				bool action = await DependencyService.Get<INotificationHelper>().ShowDialog("Missing entries", " Not all values have been inserted. Abort adding film?", "Yes, abort", "No,stay");
				if (action) await _navigationService.GoBackAsync();
            }
			else if (_viewModel.MovieImage as string == "icon.png")
			{
				bool action = await DependencyService.Get<INotificationHelper>().ShowDialog("Default image", "Are you sure you want to go with the default image?", "Yes", "No, i want to choose one");
				if (action)
				{
					_movieList.Add(new MovieModel(_viewModel.MovieTitle, _viewModel.MovieGenre, _viewModel.MovieImage));
					await _navigationService.GoBackAsync();
				}
			}
			else
			{
				_movieList.Add(new MovieModel(_viewModel.MovieTitle, _viewModel.MovieGenre, _viewModel.MovieImage));
				await _navigationService.GoBackAsync();
			}
            
        }
    }
}
