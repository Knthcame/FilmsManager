using System;
using FilmsManagerApi.Services.Interfaces;
using Models.Classes;

namespace FilmsManagerApi.Services
{
    public class GenreRepository : IRepository<GenreModel, GenreResponse>
    {
        public string Culture = "en-EN";

        public GenreResponse All => new GenreResponse();

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
