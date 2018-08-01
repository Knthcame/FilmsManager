using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmsManagerApi.Services.Interfaces;
using Models.Classes;

namespace FilmsManagerApi.Services
{
    public class GenreRepository : IRepository<GenreModel, GenreResponse>
    {
        public string Culture = "en-EN";

        public GenreResponse All => new GenreResponse();

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public bool DoesItemExist(string id)
        {
            throw new NotImplementedException();
        }

        public GenreModel Find(string id)
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
