using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmsManagerApi.Services.Interfaces;
using Models.Classes;

namespace FilmsManagerApi.Services
{
    public class GenreRepository : IGenreRepository
    {
        private readonly GenreResponse _genreResponse = new GenreResponse();

        public GenreResponse All
        {
            get { return _genreResponse; }
        }
    }
}
