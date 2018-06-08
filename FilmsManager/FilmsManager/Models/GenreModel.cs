using System;
using System.Collections.Generic;
using System.Text;

namespace FilmsManager.Models
{
	public class GenreModel : BaseModel
	{
		private string _name;

		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				RaisePropertyChanged();
			}
		}

			public GenreModel(string name)
		{
			_name = name;
		}
	}
}
