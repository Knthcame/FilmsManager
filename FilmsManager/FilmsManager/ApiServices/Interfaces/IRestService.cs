using Models.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.ApiServices.Interfaces
{
	public interface IRestService
    {
		Task<List<ToDoItem>> RefreshDataAsync();

		Task SaveToDoItemAsync(ToDoItem item, bool isNewItem);

		Task DeleteToDoItemAsync(string id);
	}
}
