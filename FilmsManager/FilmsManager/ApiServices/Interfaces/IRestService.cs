using Models.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.ApiServices.Interfaces
{
	public interface IRestService
    {
        Task<JEntity> RefreshDataAsync<TEntity, JEntity>() where TEntity : IEntity;

        Task SaveToDoItemAsync(MovieModel item, bool isNewItem);

		Task DeleteToDoItemAsync(string id);
	}
}
