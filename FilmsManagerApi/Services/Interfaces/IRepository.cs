using Models.Classes;

namespace FilmsManagerApi.Services.Interfaces
{
    public interface IRepository<TEntity, JEntity> where TEntity : IEntity
    {
        bool DoesItemExist(int id);
        JEntity All { get; }
        TEntity Find(int id);
        void Insert(TEntity item);
        void Update(TEntity item);
        void Delete(int id);
    }
}