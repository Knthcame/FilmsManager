using System.Collections.Generic;
using Models.Classes;

namespace FilmsManagerApi.Services.Interfaces
{
	public interface IRepository<TEntity> where TEntity : IEntity //, new()
    {
        bool DoesItemExist(string id);
        IEnumerable<TEntity> All { get; }
        TEntity Find(string id);
        void Insert(TEntity item);
        void Update(TEntity item);
        void Delete(string id);
    }
}