﻿using System.Globalization;
using System.Threading;
using FilmsManager.Constants;
using FilmsManager.Droid.ResxLocalization;
using FilmsManager.ResxLocalization;
using Xamarin.Forms;

[assembly: Dependency(typeof(Localize_Android))]


namespace FilmsManager.Droid.ResxLocalization
{
	public class Localize_Android : ILocalize
	{
		private CultureInfo _cultureInfo;

		public Localize_Android()
		{
			_cultureInfo = GetMobileCultureInfo();
		}

		public void SetCurrentCultureInfo(CultureInfo ci)
		{
			_cultureInfo = ci;
			TranslateExtension.CultureInfo = ci;
			SetLocale(ci);
		}

		public CultureInfo GetCurrentCultureInfo()
		{
			return _cultureInfo;
		}

		public void SetLocale(CultureInfo ci)
		{
			Thread.CurrentThread.CurrentCulture = ci;
			Thread.CurrentThread.CurrentUICulture = ci;
		}
		public CultureInfo GetMobileCultureInfo()
		{
			var netLanguage = LanguageConstants.DefaultCulture;
			var androidLocale = Java.Util.Locale.Default;
			netLanguage = AndroidToDotnetLanguage(androidLocale.ToString().Replace("_", "-"));
			// this gets called a lot - try/catch can be expensive so consider caching or something
			System.Globalization.CultureInfo ci = null;
			try
			{
				ci = new CultureInfo(netLanguage);
			}
			catch (CultureNotFoundException e1)
			{
				// iOS locale not valid .NET culture (eg. "en-ES" : English in Spain)
				// fallback to first characters, in this case "en"
				try
				{
					var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
					ci = new CultureInfo(fallback);
				}
				catch (CultureNotFoundException e2)
				{
					// iOS language not valid .NET culture, falling back to English
					ci = new CultureInfo(LanguageConstants.DefaultCulture);
				}
			}
			return ci;
		}
		string AndroidToDotnetLanguage(string androidLanguage)
		{
			var netLanguage = androidLanguage;
			//certain languages need to be converted to CultureInfo equivalent
			switch (androidLanguage)
			{
				case "ms-BN":   // "Malaysian (Brunei)" not supported .NET culture
				case "ms-MY":   // "Malaysian (Malaysia)" not supported .NET culture
				case "ms-SG":   // "Malaysian (Singapore)" not supported .NET culture
					netLanguage = "ms"; // closest supported
					break;
				case "in-ID":  // "Indonesian (Indonesia)" has different code in  .NET
					netLanguage = "id-ID"; // correct code for .NET
					break;
				case "gsw-CH":  // "Schwiizertüütsch (Swiss German)" not supported .NET culture
					netLanguage = "de-CH"; // closest supported
					break;
					// add more application-specific cases here (if required)
					// ONLY use cultures that have been tested and known to work
			}
			return new PlatformCulture(netLanguage).ToString();
		}
		string ToDotnetFallbackLanguage(PlatformCulture platCulture)
		{
			var netLanguage = platCulture.LanguageCode; // use the first part of the identifier (two chars, usually);
			switch (platCulture.LanguageCode)
			{
				case "gsw":
					netLanguage = "de-CH"; // equivalent to German (Switzerland) for this app
					break;
					// add more application-specific cases here (if required)
					// ONLY use cultures that have been tested and known to work
			}
			return netLanguage;
		}
	}
}