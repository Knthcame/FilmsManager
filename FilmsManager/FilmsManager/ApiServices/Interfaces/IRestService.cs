using Models.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.ApiServices.Interfaces
{
	public interface IRestService
    {
        Task<JEntity> RefreshDataAsync<TEntity, JEntity>() where TEntity : IEntity;

        Task SaveToDoItemAsync<TEntity>(MovieModel item, bool isNewItem) where TEntity : IEntity;

		Task DeleteToDoItemAsync<TEntity>(string id) where TEntity : IEntity;
	}
}
