using FilmsManager.Models;
using System;
using System.Collections.Generic;

namespace FilmsManager.ResxLocalization
{
	public class PlatformCulture
	{
		public const string DefaultCulture = "en-EN";

		public Dictionary<string, string> SupportedCultures = new Dictionary<string, string>
		{
			{ "en-EN", "en-EN"},
			{ "es-ES", "es-ES"}
		};

		public PlatformCulture(string platformCultureString)
		{
			if (String.IsNullOrEmpty(platformCultureString))
			{
				throw new ArgumentException("Expected culture identifier", "platformCultureString"); // in C# 6 use nameof(platformCultureString)
			}
			
			if (SupportedCultures.TryGetValue("platformCultureString", out platformCultureString))
				PlatformString = platformCultureString.Replace("_", "-"); // .NET expects dash, not underscore
			else PlatformString = DefaultCulture;

			var dashIndex = PlatformString.IndexOf("-", StringComparison.Ordinal);
			if (dashIndex > 0)
			{
				var parts = PlatformString.Split('-');
				LanguageCode = parts[0];
				LocaleCode = parts[1];
			}
			else
			{
				LanguageCode = PlatformString;
				LocaleCode = "";
			}
		}
		public string PlatformString { get; private set; }
		public string LanguageCode { get; private set; }
		public string LocaleCode { get; private set; }
		public override string ToString()
		{
			return PlatformString;
		}
	}
}
