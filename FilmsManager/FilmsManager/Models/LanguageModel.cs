using Models.Classes;
using Prism.Mvvm;
using SQLite;

namespace FilmsManager.Models
{
	public class LanguageModel : BindableBase, IEntity
	{
		private string _language;
		private string _abreviation;
		private string _flag;
        
		public string Language
		{
			get { return _language; }
			set { SetProperty(ref _language, value); }
		}

		public string Abreviation
		{
			get { return _abreviation; }
			set { SetProperty(ref _abreviation, value); }
		}
        
        [Ignore]
		public string Flag
		{
			get => _flag;
			set { SetProperty (ref _flag, value); }
		}

        public int Id { get; set; }

        public LanguageModel() { }

		public LanguageModel(string language, string abreviation, string flag)
		{
			Language = language;
			Abreviation = abreviation;
			Flag = flag;
		}
	}
}
