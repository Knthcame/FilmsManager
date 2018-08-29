using Models.Classes;
using System.Threading.Tasks;

namespace FilmsManager.Managers.Interfaces
{
    public interface IHttpManager
    {
        Task<TResponse> RefreshDataAsync<TEntity, TResponse>() where TEntity : IEntity, new() where TResponse : class, new();

        Task SaveEntityAsync<TEntity>(TEntity entity, bool isNewItem) where TEntity : IEntity, new();

        Task DeleteEntityAsync<TEntity>(TEntity entity) where TEntity : IEntity, new();

        Task<bool> IsApiReachableAsync<TEntity>();

        GenreResponse GetDefaultGenres();
    }
}
