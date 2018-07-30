using Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsManagerApi.Services.Interfaces
{
    public interface IGenreRepository
    {
        //bool DoesItemExist(string id);
        GenreResponse All { get; }
        //GenreModel Find(string id);
        //void Insert(GenreModel item);
        //void Update(GenreModel item);
        //void Delete(string id);
    }
}
