using Xamarin.Forms;

namespace FilmsManager.Models
{
    public class PickImageModel : BaseModel
    {
        private string _imageName;

        public string ImageName
        {
            get => _imageName;
            set
            {
                _imageName = value;

				Image image = new Image();
				image.Source = ImageSource.FromResource(_imageName);
				double screenWidth = Application.Current.MainPage.Width;
				double imageWidth = image.Width;
				double imageHeight = image.Height;
				CellHeight = screenWidth / imageWidth * imageHeight;
				RaisePropertyChanged();
            }
        }

		public double CellHeight;
	}
}
