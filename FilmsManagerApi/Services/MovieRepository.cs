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
        private List<MovieModel> _toDoList;

        public MovieRepository()
        {
            InitializeData();
        }

        public IEnumerable<MovieModel> All
        {
            get { return _toDoList; }
        }

        public bool DoesItemExist(int id) => _toDoList.Any(item => item.Id == id);

        public MovieModel Find(int id) => _toDoList.FirstOrDefault(item => item.Id == id);

        public void Insert(MovieModel item)
        {
            _toDoList.Add(item);
        }

        public void Update(MovieModel item)
        {
            var model = Find(item.Id);
            var index = _toDoList.IndexOf(model);
            _toDoList.RemoveAt(index);
            _toDoList.Insert(index, item);
        }

        public void Delete(int id) => _toDoList.Remove(Find(id));

        private void InitializeData()
        {
            _toDoList = new List<MovieModel>
            {
                new MovieModel ("Infinity war", new GenreModel(GenreKeys.SuperHeroesGenre, "Super heroes"), AppImages.InfinityWar),
                new MovieModel ("Shrek", new GenreModel(GenreKeys.HumourGenre, "Humour"), AppImages.Shrek),
                new MovieModel ("Shrek 2", new GenreModel(GenreKeys.HumourGenre, "Humour"), AppImages.Shrek2)
            };
        }
    }
}