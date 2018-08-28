using Models.Classes;
using System.Threading.Tasks;

namespace FilmsManager.Services.Interfaces
{
    public interface IRestService
    {
        Task<TResponse> RefreshDataAsync<TEntity, TResponse>() where TEntity : IEntity;

        Task SaveEntityAsync<TEntity>(TEntity entity, bool isNewItem) where TEntity : IEntity;

        Task DeleteEntityAsync<TEntity>(int id) where TEntity : IEntity;
    }
}
