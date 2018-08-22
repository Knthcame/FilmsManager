using Prism.Mvvm;
using Prism.Navigation;
using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FilmsManager.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware, IDestructible
	{
		protected INavigationService NavigationService { get; private set; }

        protected SQLiteConnection sqlConnection;

		private string _title;

		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}


		
		public BaseViewModel(INavigationService navigationService)
		{
			NavigationService = navigationService;

            var sqliteFilename = "MyDatabase.db3";
            string libraryPath;

            if (Device.RuntimePlatform==Device.Android)
                libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //Just use whatever directory SpecialFolder.Personal returns
            
            else
            {
                // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
                // (they don't want non-user-generated data in Documents)
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder instead
            }
            
            var path = Path.Combine(libraryPath, sqliteFilename);

            sqlConnection = new SQLiteConnection(path);
		}

		public virtual void OnNavigatedFrom(NavigationParameters parameters) { }

		public virtual void OnNavigatedTo(NavigationParameters parameters) { }

		public virtual void OnNavigatingTo(NavigationParameters parameters) { }

		public virtual void Destroy() { }

		public virtual void OnAppearing() { }

		public virtual async Task<bool> OnBackButtonPressedAsync()
		{
			return await NavigationService.GoBackAsync();
		}

		/// <summary>
		/// Tries to get the parameter specified in parameterID and returns it. 
		/// </summary>
		/// <param name="parameters">Parameters passed by the NavigationService</param>
		/// <param name="parameterID">Identification for the desired parameter</param>
		/// <param name="outProperty">If this is specified, this will be the output of the function when the parameterID is not found. It is intented that this is the same property the output will be assigned to</param>
		/// <returns></returns>
		public virtual object GetNavigationParameter(NavigationParameters parameters, string parameterID, object outProperty = null)
		{
			if(parameters.TryGetValue(parameterID, out object aux))
				return aux;
			else
				return outProperty;
		}
	}
}