namespace Models.Classes
{
	public class ToDoItem
    {
		public string Id { get; set; }

		public string Title { get; set; }

		public GenreModel Genre { get; set; }

		public object Image { get; set; }
	}
}
