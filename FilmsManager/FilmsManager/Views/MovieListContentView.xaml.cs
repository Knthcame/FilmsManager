using Models.Classes;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieListContentView : BaseContentView
    {
        public static readonly BindableProperty MoviesSourceProperty = 
            BindableProperty.Create(propertyName: nameof(MoviesSource), returnType: typeof(IEnumerable<MovieModel>), defaultValue: null, declaringType: typeof(MovieListContentView));

        public static readonly BindableProperty IsRefreshingMovieListProperty = 
            BindableProperty.Create(propertyName: nameof(IsRefreshingMovieList), returnType: typeof(bool), defaultValue: false, declaringType: typeof(MovieListContentView));

        public static readonly BindableProperty DeleteCommandProperty = 
            BindableProperty.Create(propertyName: nameof(DeleteCommand), returnType: typeof(ICommand), defaultValue: null, declaringType: typeof(MovieListContentView));

        public static readonly BindableProperty RefreshCommandProperty = 
            BindableProperty.Create(propertyName: nameof(RefreshCommand), returnType: typeof(ICommand), defaultValue: null, declaringType: typeof(MovieListContentView));

        public static readonly BindableProperty ShowDetailsCommandProperty = 
            BindableProperty.Create(propertyName: nameof(ShowDetailsCommand), returnType: typeof(ICommand), defaultValue: null, declaringType: typeof(MovieListContentView));

        public static readonly BindableProperty IsPullToRefreshEnabledProperty = 
            BindableProperty.Create(propertyName: nameof(IsPullToRefreshEnabled), returnType: typeof(bool), defaultValue: false, declaringType: typeof(MovieListContentView));

        public static readonly BindableProperty IsListViewVisibleProperty =
            BindableProperty.Create(propertyName: nameof(IsListViewVisible), returnType: typeof(bool), defaultValue: true, declaringType: typeof(MovieListContentView));

        public static readonly BindableProperty IsLabelVisibleProperty =
            BindableProperty.Create(propertyName: nameof(IsLabelVisible), returnType: typeof(bool), defaultValue: false, declaringType: typeof(MovieListContentView));

        public static readonly BindableProperty LabelTextProperty =
            BindableProperty.Create(propertyName: nameof(LabelText), returnType: typeof(string), defaultValue: null, declaringType: typeof(MovieListContentView));

        public IEnumerable<MovieModel> MoviesSource
        {
            get => (IEnumerable<MovieModel>)GetValue(MoviesSourceProperty);
            set => SetValue(MoviesSourceProperty, value);
        }

        public string LabelText
        {
            get => (string) GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        public bool IsPullToRefreshEnabled
        {
            get => (bool) GetValue(IsPullToRefreshEnabledProperty);
            set => SetValue(IsPullToRefreshEnabledProperty, value);
        }

        public bool IsRefreshingMovieList
        {
            get => (bool) GetValue(IsRefreshingMovieListProperty);
            set => SetValue(IsRefreshingMovieListProperty, value);
        }
        public bool IsListViewVisible
        {
            get => (bool) GetValue(IsListViewVisibleProperty);
            set => SetValue(IsListViewVisibleProperty, value);
        }

        public bool IsLabelVisible
        {
            get => (bool) GetValue(IsLabelVisibleProperty);
            set => SetValue(IsLabelVisibleProperty, value);
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

        public MovieListContentView()
        {
            try
            {
                InitializeComponent();
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("              ERROR {0}", ex.Message);
            }
        }
    }
}