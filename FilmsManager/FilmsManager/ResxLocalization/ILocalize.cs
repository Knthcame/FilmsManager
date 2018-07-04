using System.Globalization;

namespace FilmsManager.ResxLocalization
{
	public interface ILocalize
	{
		CultureInfo GetCurrentCultureInfo();

		CultureInfo GetMobileCultureInfo();

		void SetLocale(CultureInfo ci);

		void SetCurrentCultureInfo(CultureInfo ci);
	}
}
