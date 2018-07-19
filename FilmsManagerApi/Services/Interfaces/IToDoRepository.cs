using System.Collections.Generic;
using Models.Classes;

namespace FilmsManagerApi.Services.Interfaces
{
	public interface IToDoRepository
    {
        bool DoesItemExist(string id);
        IEnumerable<MovieItem> All { get; }
        MovieItem Find(string id);
        void Insert(MovieItem item);
        void Update(MovieItem item);
        void Delete(string id);
    }
}