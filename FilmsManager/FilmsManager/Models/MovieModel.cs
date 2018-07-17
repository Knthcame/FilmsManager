using Models.Classes;
using Xamarin.Forms;

namespace FilmsManager.Models
{
    public class MovieModel : BaseModel
    {
        private string _title;
        private GenreModel _genre;
        private object _image;

        public MovieModel(string title, GenreModel genre, object image)
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

        public GenreModel Genre
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
