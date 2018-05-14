using Xamarin.Forms;

namespace FilmsManager.Models
{
    public class MovieModel : BaseModel
    {
        private string _title;
        private string _genre;
        private string _image;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        public string Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                RaisePropertyChanged();
            }
        }
        public string Image
        {
            get => _image;
            set
            {
                _image = value;
                RaisePropertyChanged();
            }
        }
    }
}
