using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmsManager.ApiServices.Interfaces
{
	public interface IRestService
    {
		Task<List<ToDoItem>> RefreshDataAsync();

		Task SaveToDoItemAsync(ToDoItem item, bool isNewItem);

		Task DeleteToDoItemAsync(string id);
	}
}
