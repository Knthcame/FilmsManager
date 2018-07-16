using FilmsManager.Constants;
using FilmsManager.Resources;

namespace FilmsManager.Models
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


		public GenreModel(int id)
		{
			ID = id;
			switch (id)
			{
				case GenreKeys.ActionGenre:
					Name = AppResources.ActionGenre;
					break;
				case GenreKeys.DramaGenre:
					Name = AppResources.DramaGenre;
					break;
				case GenreKeys.FantasyGenre:
					Name = AppResources.FantasyGenre;
					break;
				case GenreKeys.HumourGenre:
					Name = AppResources.HumourGenre;
					break;
				case GenreKeys.ScienceFictionGenre:
					Name = AppResources.ScienceFictionGenre;
					break;
				case GenreKeys.SuperHeroesGenre:
					Name = AppResources.SuperHeroesGenre;
					break;
				case GenreKeys.TerrorGenre:
					Name = AppResources.TerrorGenre;
					break;
				case GenreKeys.AllGenres:
					Name = AppResources.AllGenres;
					break;
			}
		}
	}
}
