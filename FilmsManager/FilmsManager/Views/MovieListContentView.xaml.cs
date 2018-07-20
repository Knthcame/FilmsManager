using FilmsManager.Models;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MovieListContentView
	{
		public static readonly BindableProperty MoviesSourceProperty = BindableProperty.Create(propertyName: nameof(MoviesSource), returnType: typeof(IEnumerable<MovieModel>), declaringType: typeof(MovieListContentView));

		public static readonly BindableProperty IsRefreshingMovieListProperty = BindableProperty.Create(propertyName: nameof(IsRefreshingMovieList), returnType: typeof(bool), defaultValue: false, declaringType: typeof(MovieListContentView));

		public static readonly BindableProperty DeleteCommandProperty = BindableProperty.Create(propertyName: nameof(DeleteCommand), returnType: typeof(ICommand), declaringType: typeof(MovieListContentView));

		public static readonly BindableProperty RefreshCommandProperty = BindableProperty.Create(propertyName: nameof(RefreshCommand), returnType: typeof(ICommand), declaringType: typeof(MovieListContentView));

		public static readonly BindableProperty ShowDetailsCommandProperty = BindableProperty.Create(propertyName: nameof(ShowDetailsCommand), returnType: typeof(ICommand), declaringType: typeof(MovieListContentView));

		public IEnumerable<MovieModel> MoviesSource
		{
			get => (IEnumerable<MovieModel>) GetValue(MoviesSourceProperty);
			set => SetValue(MoviesSourceProperty, value);
		}

		public bool IsRefreshingMovieList
		{
			get => (bool) GetValue(IsRefreshingMovieListProperty);
			set => SetValue(IsRefreshingMovieListProperty, value);
		}

		public ICommand RefreshCommand
		{
			get => (ICommand) GetValue(RefreshCommandProperty);
			set => SetValue(RefreshCommandProperty, value);
		}

		public ICommand DeleteCommand
		{
			get => (ICommand) GetValue(DeleteCommandProperty);
			set => SetValue(DeleteCommandProperty, value);
		}

		public ICommand ShowDetailsCommand
		{
			get => (ICommand) GetValue(ShowDetailsCommandProperty);
			set => SetValue(ShowDetailsCommandProperty, value);
		}

		public MovieListContentView ()
		{
			InitializeComponent ();
		}
	}
}