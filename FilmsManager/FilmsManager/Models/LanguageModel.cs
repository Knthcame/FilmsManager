namespace FilmsManager.Models
{
	public class LanguageModel : BaseModel
    {
		private string _abreviation;

		public string Abreviation
		{
			get { return _abreviation; }
			set { SetProperty( ref _abreviation, value); }
		}


		private string _language;

		public string Language
		{
			get { return _language; }
			set { SetProperty( ref _language, value); }
		}

		public LanguageModel(string language, string abreviation)
		{
			Language = language;
			Abreviation = abreviation;
		}
	}
}
