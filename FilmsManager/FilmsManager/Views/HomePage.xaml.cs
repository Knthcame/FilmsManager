using System.Diagnostics;

namespace FilmsManager.Views
{
	public partial class HomePage : BasePage
	{
        public HomePage() : base()
		{
			try
			{
				InitializeComponent ();
			}
			catch (System.Exception ex)
			{
				Debug.WriteLine("              ERROR {0}", ex.Message);
			}
		}
	}
}