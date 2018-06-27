using Plugin.Media;
using Xamarin.Forms;

namespace FilmsManager.Models
{
	public class PickImageModel : BaseModel
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
