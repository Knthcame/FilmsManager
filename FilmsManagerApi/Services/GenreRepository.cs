using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmsManagerApi.Services.Interfaces;
using Models.Classes;

namespace FilmsManagerApi.Services
{
    public class GenreRepository : IRepository<GenreModel>
    {
        private readonly GenreResponse _genreResponse = new GenreResponse();

        public string Culture = "en-EN";

        private Dictionary<string, IList<GenreModel>> _cultures = new Dictionary<string, IList<GenreModel>>()
        {
            {"en-EN", new GenreResponse().English },
            {"es-ES", new GenreResponse().Spanish }

        };

        IEnumerable<GenreModel> IRepository<GenreModel>.All => GetGenres();

        private IEnumerable<GenreModel> GetGenres()
        {
            _cultures.TryGetValue(Culture, out IList<GenreModel> genres);
            return genres;
        }

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
