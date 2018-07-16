using System.Collections.Generic;
using System.Linq;
using FilmsManagerApi.Services.Interfaces;
using Models;

namespace FilmsManagerApi.Services
{
	public class ToDoRepository : IToDoRepository
    {
        private List<ToDoItem> _toDoList;

        public ToDoRepository()
        {
            InitializeData();
        }

        public IEnumerable<ToDoItem> All
        {
            get { return _toDoList; }
        }

        public bool DoesItemExist(string id) => _toDoList.Any(item => item.Id == id);

        public ToDoItem Find(string id) => _toDoList.FirstOrDefault(item => item.Id == id);

        public void Insert(ToDoItem item)
        {
            _toDoList.Add(item);
        }

        public void Update(ToDoItem item)
        {
            var ToDoItem = Find(item.Id);
            var index = _toDoList.IndexOf(ToDoItem);
            _toDoList.RemoveAt(index);
            _toDoList.Insert(index, item);
        }

        public void Delete(string id) => _toDoList.Remove(Find(id));

        private void InitializeData()
        {
            _toDoList = new List<ToDoItem>
            {
                new ToDoItem
                {
                    Id = "6bb8a868-dba1-4f1a-93b7-24ebce87e243",
                    Title = "Learn app development",
                    Genre = "Attend Xamarin University",
                    Image = "learning_app.png"
                },
                new ToDoItem
                {
                    Id = "b94afb54-a1cb-4313-8af3-b7511551b33b",
                    Title = "Develop apps",
                    Genre = "Use Visual Studio",
                    Image = "developing_app.png"
                },
                new ToDoItem
                {
                    Id = "ecfa6f80-3671-4911-aabe-63cc442c1ecf",
                    Title = "Publish apps",
                    Genre = "All app stores",
                    Image = "publishing_app.png"
                }
            };
        }
    }
}