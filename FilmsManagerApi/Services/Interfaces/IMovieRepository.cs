using System.Collections.Generic;
using Models.Classes;

namespace FilmsManagerApi.Services.Interfaces
{
	public interface IMovieRepository
    {
        bool DoesItemExist(string id);
        IEnumerable<MovieModel> All { get; }
        MovieModel Find(string id);
        void Insert(MovieModel item);
        void Update(MovieModel item);
        void Delete(string id);
    }
}