using System.Collections.Generic;
using System.Linq;
using FilmsManagerApi.Services.Interfaces;
using Models.Classes;
using Models.Constants;
using Models.Resources;

namespace FilmsManagerApi.Services
{
    public class MovieRepository : IRepository<MovieModel, IEnumerable<MovieModel>>
    {
        private List<MovieModel> _movieList;

        public MovieRepository()
        {
            InitializeData();
        }

        public IEnumerable<MovieModel> All
        {
            get { return _movieList; }
        }

        public bool DoesItemExist(int id) => _movieList.Any(item => item.Id == id);

        public MovieModel Find(int id) => _movieList.FirstOrDefault(item => item.Id == id);

        public void Insert(MovieModel item)
        {
            _movieList.Add(item);
        }

        public void Update(MovieModel item)
        {
            var model = Find(item.Id);
            var index = _movieList.IndexOf(model);
            _movieList.RemoveAt(index);
            _movieList.Insert(index, item);
        }

        public void Delete(int id) => _movieList.Remove(Find(id));

        private void InitializeData()
        {
            _movieList = new List<MovieModel>
            {
                new MovieModel ("Infinity war", new GenreModel(GenreKeys.SuperHeroesGenre, "Super heroes"), AppImages.InfinityWar),
                new MovieModel ("Shrek", new GenreModel(GenreKeys.HumourGenre, "Humour"), AppImages.Shrek),
                new MovieModel ("Shrek 2", new GenreModel(GenreKeys.HumourGenre, "Humour"), AppImages.Shrek2)
            };
        }
    }
}