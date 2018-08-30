using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Models.Classes
{
    [Table(nameof(MovieModel))]
    public class MovieModel : BaseModel
    {
        public string Title { get; set; }
        
        public string GenreBlobbed { get; set; }

        [TextBlob(nameof(GenreBlobbed))]
        public GenreModel Genre { get; set; }

        public string Image { get; set; }

        public MovieModel(string title, GenreModel genre, string image)
        {
            Title = title;
            Genre = genre;
            Image = image;
        }

        public MovieModel() { }
    }
}
