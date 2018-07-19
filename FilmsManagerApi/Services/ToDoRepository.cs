using System.Collections.Generic;
using System.Linq;
using FilmsManagerApi.Services.Interfaces;
using Models;
using Models.Classes;
using Models.Constants;
using Models.Resources;

namespace FilmsManagerApi.Services
{
	public class ToDoRepository : IToDoRepository
    {
        private List<MovieItem> _toDoList;

        public ToDoRepository()
        {
            InitializeData();
        }

        public IEnumerable<MovieItem> All
        {
            get { return _toDoList; }
        }

        public bool DoesItemExist(string id) => _toDoList.Any(item => item.Id == id);

        public MovieItem Find(string id) => _toDoList.FirstOrDefault(item => item.Id == id);

        public void Insert(MovieItem item)
        {
            _toDoList.Add(item);
        }

        public void Update(MovieItem item)
        {
            var ToDoItem = Find(item.Id);
            var index = _toDoList.IndexOf(ToDoItem);
            _toDoList.RemoveAt(index);
            _toDoList.Insert(index, item);
        }

        public void Delete(string id) => _toDoList.Remove(Find(id));

        private void InitializeData()
        {
			_toDoList = new List<MovieItem>
			{
				new MovieItem ( "6bb8a868-dba1-4f1a-93b7-24ebce87e243", "Infinity war", new GenreModel(GenreKeys.SuperHeroesGenre, "Super heroes"), AppImages.InfinityWar),
                new MovieItem ( "b94afb54-a1cb-4313-8af3-b7511551b33b", "Shrek", new GenreModel(GenreKeys.HumourGenre, "Humour"), AppImages.Shrek),             
                new MovieItem ( "ecfa6f80-3671-4911-aabe-63cc442c1ecf", "Shrek 2", new GenreModel(GenreKeys.HumourGenre, "Humour"), AppImages.Shrek2)
            };
        }
    }
}