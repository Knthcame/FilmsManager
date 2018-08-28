using Models.Constants;
using Models.Managers.Interfaces;
using Models.Classes;
using FilmsManager.Resources;

namespace Models.Managers
{
    public class GenreModelManager : IGenreModelManager
    {
        public GenreModel FindByID(int id)
        {
            string name = null;
            switch (id)
            {
                case GenreKeys.ActionGenre:
                    name = AppResources.ActionGenre;
                    break;
                case GenreKeys.DramaGenre:
                    name = AppResources.DramaGenre;
                    break;
                case GenreKeys.FantasyGenre:
                    name = AppResources.FantasyGenre;
                    break;
                case GenreKeys.HumourGenre:
                    name = AppResources.HumourGenre;
                    break;
                case GenreKeys.ScienceFictionGenre:
                    name = AppResources.ScienceFictionGenre;
                    break;
                case GenreKeys.SuperHeroesGenre:
                    name = AppResources.SuperHeroesGenre;
                    break;
                case GenreKeys.TerrorGenre:
                    name = AppResources.TerrorGenre;
                    break;
                case GenreKeys.AllGenres:
                    name = AppResources.AllGenres;
                    break;
            }
            return new GenreModel(id, name);
        }
    }
}
