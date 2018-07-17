using Models.Classes;

namespace FilmsManager.Models
{
	public class LanguageModel : BaseModel
	{
		private string _abreviation;

		public string Abreviation
		{
			get { return _abreviation; }
			set { SetProperty(ref _abreviation, value); }
		}


		private string _language;
		private string _flag;

		public string Language
		{
			get { return _language; }
			set { SetProperty(ref _language, value); }
		}

		public LanguageModel(string language, string abreviation, string flag)
		{
			Language = language;
			Abreviation = abreviation;
			Flag = flag;
		}

		public string Flag
		{
			get => _flag;
			set { SetProperty (ref _flag, value); }
		}
	}
}
