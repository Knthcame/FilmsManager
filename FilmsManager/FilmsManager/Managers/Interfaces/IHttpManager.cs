using System.Threading.Tasks;
using Models.Classes;

namespace FilmsManager.Managers.Interfaces
{
    public interface IHttpManager
    {
        Task<TResponse> RefreshDataAsync<TEntity, TResponse>() 
            where TEntity : class, IEntity, new() 
            where TResponse : class, new();

        Task SaveEntityAsync<TEntity>(TEntity entity, bool isNewItem) 
            where TEntity : class, IEntity, new();

        Task DeleteEntityAsync<TEntity>(TEntity entity) 
            where TEntity : IEntity, new();

        Task<bool> IsApiReachableAsync<TEntity>();

        GenreResponse GetDefaultGenres();
    }
}
