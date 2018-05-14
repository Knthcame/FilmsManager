using Xamarin.Forms;

namespace FilmsManager.Models
{
    public class Movie : System.Object
    {
        public string MovieTitle { get; set; }
        public string MovieGenre { get; set; }
        public ImageSource MovieIcon { get; set; }
    }
}
