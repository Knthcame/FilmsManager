namespace Models.Classes
{
	public class GenreModel : BaseModel, IEntity
    { 
		public string Name { get; set; }

		public string Id { get; set; }

		public GenreModel(string id, string name)
		{
			Id = id;
			Name = name;
		}

        public GenreModel() { }
	}
}
