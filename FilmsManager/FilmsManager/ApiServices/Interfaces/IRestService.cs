using Models.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.ApiServices.Interfaces
{
	public interface IRestService
    {
		Task<List<MovieModel>> RefreshDataAsync();

		Task SaveToDoItemAsync(MovieModel item, bool isNewItem);

		Task DeleteToDoItemAsync(string id);
	}
}
