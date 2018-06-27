using Xamarin.Forms;

namespace FilmsManager.Models
{
    public class MovieModel : BaseModel
    {
        private string _title;
        private string _genre;
        private object _image;

        public MovieModel(string title, string genre, object image)
        {
            _title = title;
            _genre = genre;
            _image = image;
        }

        public string Title
        {
            get => _title;
            set { SetProperty(ref _title, value); }
        }

        public string Genre
        {
            get => _genre;
            set { SetProperty(ref _genre, value); }
        }
        public object Image
        {
            get => _image;
            set { SetProperty(ref _image, value); }
        }
    }
}
