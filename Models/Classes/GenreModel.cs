namespace Models.Classes
{
	public class GenreModel : BaseModel
	{
		private string _name;

		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				RaisePropertyChanged();
			}
		}

		private int _id;

		public int ID
		{
			get { return _id; }
			set
			{
				_id = value;
				RaisePropertyChanged();
			}
		}


		public GenreModel(int id, string name)
		{
			ID = id;
			Name = name;
		}
	}
}
