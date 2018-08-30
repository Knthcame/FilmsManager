using System;
using FilmsManagerApi.Services.Interfaces;
using Models.Classes;
using Models.Constants;

namespace FilmsManagerApi.Services
{
    public class GenreRepository : IRepository<GenreModel, GenreResponse>
    {
        private GenreResponse _genreLists = GenreConstants.DefaultGenres; 

        public GenreResponse All => _genreLists;

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool DoesItemExist(int id)
        {
            throw new NotImplementedException();
        }

        public GenreModel Find(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(GenreModel item)
        {
            throw new NotImplementedException();
        }

        public void Update(GenreModel item)
        {
            throw new NotImplementedException();
        }
    }
}
