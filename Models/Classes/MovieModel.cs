using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Models.Classes
{
    public class MovieModel : BaseModel
    {
        public string Title { get; set; }

        [ForeignKey(typeof(GenreModel))]
        public int GenreId { get; set; }

        [ManyToOne]
        public GenreModel Genre { get; set; }

        [Ignore]
        public object Image { get; set; }

        public MovieModel(string title, GenreModel genre, object image)
        {
            Title = title;
            Genre = genre;
            Image = image;
        }

        public MovieModel() { }
    }
}
