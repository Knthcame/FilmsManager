namespace Models.Classes
{
	public class MovieModel : BaseModel, IEntity
    {
		public string Id { get; set; }

		public string Title { get; set; }

		public GenreModel Genre { get; set; }

		public object Image { get; set; }

		public MovieModel(string id, string title, GenreModel genre, object image)
		{
			Id = id;
			Title = title;
			Genre = genre;
			Image = image;
		}
	}
}
