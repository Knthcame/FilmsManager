using Models.Classes;

namespace Models.Managers.Interfaces
{
    public interface IGenreModelManager
    {
        GenreModel FindByID(int id);
    }
}
