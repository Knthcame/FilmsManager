namespace Models.Classes
{
	public class GenreModel : BaseModel
	{
		private string _name;

		public string Name
		{
			get => _name;
			set { SetProperty(ref _name, value); }
		}

		private int _id;

		public int ID
		{
			get { return _id; }
			set { SetProperty(ref _id, value); }
		}


		public GenreModel(int id, string name)
		{
			ID = id;
			Name = name;
		}
	}
}
