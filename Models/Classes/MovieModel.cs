using SQLite;

namespace Models.Classes
{
    [Table(nameof(MovieModel))]
    public class MovieModel : BaseModel
    {
        public string Title { get; set; }
        
        public string GenreBlobbed { get; set; }

        [Ignore]
        public GenreModel Genre { get; set; }

        public string Image { get; set; }

        public MovieModel(string title, GenreModel genre, string image, int id = default(int))
        {
            Title = title;
            Genre = genre;
            Image = image;
            Id = id;
        }

        public MovieModel() { }
    }
}
