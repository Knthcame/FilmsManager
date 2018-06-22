using System;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace FilmsManager.Models
{
	public class PickImageModel : BaseModel
	{
		private object _imageName;

		public object ImageName
		{
			get => _imageName;
			set
			{
				_imageName = value;
				RaisePropertyChanged();
			}
		}

		public double ScreenHeight { get; set; } = Application.Current.MainPage.Height;

		public double ScreenWidth { get; set; } = Application.Current.MainPage.Width;
	}
}
