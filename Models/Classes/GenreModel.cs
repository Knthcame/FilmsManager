namespace Models.Classes
{
	public class GenreModel : BaseModel
    { 
		public string Name { get; set; }

		public int Id { get; set; }

        public string Culture { get; set; }

		public GenreModel(int id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}
