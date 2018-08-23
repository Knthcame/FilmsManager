using Models.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Services.Interfaces
{
	public interface IRestService
    {
        Task<TResponse> RefreshDataAsync<TEntity, TResponse>() where TEntity : IEntity;

        Task SaveEntityAsync<TEntity>(TEntity entity, bool isNewItem) where TEntity : IEntity;

		Task DeleteEntityAsync<TEntity>(string id) where TEntity : IEntity;
	}
}
