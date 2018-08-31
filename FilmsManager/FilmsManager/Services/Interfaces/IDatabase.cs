using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Classes;

namespace FilmsManager.Services.Interfaces
{
    public interface IDatabase
    {
        Task<TEntity> FindAsync<TEntity>(int id) 
            where TEntity : IEntity, new();

        Task<TResponse> FindAllAsync<TEntity, TResponse>() 
            where TEntity : new() where TResponse : class, new();

        Task<bool> AddOrUpdateAsync<TEntity>(IEnumerable<TEntity> entities) 
            where TEntity : IEntity, new();

        Task<bool> AddOrUpdateAsync<TEntity>(TEntity entity) 
            where TEntity : IEntity, new();

        Task<bool> RemoveAsync<TEntity>(TEntity entity) 
            where TEntity : new();

        Task<bool> RemoveAllAsync<TEntity>() 
            where TEntity : new();
    }
}
