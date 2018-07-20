using Models.Classes;
using Prism.Mvvm;

namespace FilmsManager.Models
{
	public class MovieModel : BindableBase
	{
		private string _title;
		private GenreModel _genre;
		private object _image;
		private string _id;

		public MovieModel(string id, string title, GenreModel genre, object image)
		{
			Id = id;
			Title = title;
			Genre = genre;
			Image = image;
		}

		public string Title
		{
			get => _title;
			set { SetProperty(ref _title, value); }
		}

		public GenreModel Genre
		{
			get => _genre;
			set { SetProperty(ref _genre, value); }
		}
		public object Image
		{
			get => _image;
			set { SetProperty(ref _image, value); }
		}

		public string Id
		{
			get => _id;
			set { SetProperty(ref _id, value); }
		}
	}
}
