using FilmsManager.Services.Interfaces;
using Models.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmsManager.Managers.Interfaces
{
    public interface IDatabaseManager
    {
        IDatabase Database { get; set; }

        Task<TEntity> FindAsync<TEntity>(int id) 
            where TEntity : class, IEntity, new();

        Task<TResponse> FindAllAsync<TEntity, TResponse>() 
            where TEntity : class, IEntity, new() 
            where TResponse : class, new();

        Task<bool> AddOrUpdateAsync<TEntity>(IEnumerable<TEntity> entities) 
            where TEntity : class, IEntity, new();

        Task<bool> AddOrUpdateAsync<TEntity>(TEntity entity) 
            where TEntity : class, IEntity, new();

        Task<bool> RemoveAsync<TEntity>(TEntity entity) 
            where TEntity : new();

        Task<bool> RemoveAllAsync<TEntity>() 
            where TEntity : new();
    }
}
