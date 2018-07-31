using Models.Classes;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieListContentView
    {
        public static readonly BindableProperty MoviesSourceProperty = BindableProperty.Create(propertyName: nameof(MoviesSource), returnType: typeof(IEnumerable<MovieModel>), defaultValue: null, declaringType: typeof(MovieListContentView), defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty IsRefreshingMovieListProperty = BindableProperty.Create(propertyName: nameof(IsRefreshingMovieList), returnType: typeof(bool), defaultValue: false, declaringType: typeof(MovieListContentView));

        public static readonly BindableProperty DeleteCommandProperty = BindableProperty.Create(propertyName: nameof(DeleteCommand), returnType: typeof(ICommand), defaultValue: null, declaringType: typeof(MovieListContentView));

        public static readonly BindableProperty RefreshCommandProperty = BindableProperty.Create(propertyName: nameof(RefreshCommand), returnType: typeof(ICommand), defaultValue: null, declaringType: typeof(MovieListContentView));

        public static readonly BindableProperty ShowDetailsCommandProperty = BindableProperty.Create(propertyName: nameof(ShowDetailsCommand), returnType: typeof(ICommand), defaultValue: null, declaringType: typeof(MovieListContentView));

        public static readonly BindableProperty IsPullToRefreshEnabledProperty = BindableProperty.Create(propertyName: nameof(IsPullToRefreshEnabled), returnType: typeof(bool), defaultValue: false, declaringType: typeof(MovieListContentView));

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(propertyName: nameof(SelectedItem), returnType: typeof(object), defaultValue: null, declaringType: typeof(MovieListContentView));

        public static readonly BindableProperty SelectItemCommandProperty = BindableProperty.Create(propertyName: nameof(SelectItemCommand), returnType: typeof(ICommand), defaultValue: null, declaringType: typeof(MovieListContentView));

        public IEnumerable<MovieModel> MoviesSource
        {
            get => (IEnumerable<MovieModel>)GetValue(MoviesSourceProperty);
            set => SetValue(MoviesSourceProperty, value);
        }

        public bool IsPullToRefreshEnabled
        {
            get => (bool)GetValue(IsPullToRefreshEnabledProperty);
            set => SetValue(IsPullToRefreshEnabledProperty, value);
        }

        public bool IsRefreshingMovieList
        {
            get => (bool)GetValue(IsRefreshingMovieListProperty);
            set => SetValue(IsRefreshingMovieListProperty, value);
        }

        public ICommand RefreshCommand
        {
            get => (ICommand)GetValue(RefreshCommandProperty);
            set => SetValue(RefreshCommandProperty, value);
        }

        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public ICommand ShowDetailsCommand
        {
            get => (ICommand)GetValue(ShowDetailsCommandProperty);
            set => SetValue(ShowDetailsCommandProperty, value);
        }

        public ICommand SelectItemCommand
        {
            get => (ICommand)GetValue(SelectItemCommandProperty);
            set => SetValue(SelectItemCommandProperty, value);
        }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
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