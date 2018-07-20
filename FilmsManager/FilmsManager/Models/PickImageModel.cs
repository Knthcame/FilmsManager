using Prism.Mvvm;
using Xamarin.Forms;

namespace FilmsManager.Models
{
	public class PickImageModel : BindableBase 
	{
		private object _imageName;

		public object ImageName
		{
			get => _imageName;
			set { SetProperty(ref _imageName, value); }
		}

		public double ScreenHeight { get; set; } = Application.Current.MainPage.Height;

		public double ScreenWidth { get; set; } = Application.Current.MainPage.Width;
	}
}
