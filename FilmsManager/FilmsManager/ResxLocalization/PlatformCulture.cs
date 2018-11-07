using System;
using System.Collections.Generic;
using FilmsManager.Constants;
using FilmsManager.Models;

namespace FilmsManager.ResxLocalization
{
	public class PlatformCulture
	{
		public const string DefaultCulture = LanguageConstants.DefaultCulture;

        private Dictionary<string, LanguageModel> SupportedCultures = LanguageConstants.SupportedCultures;

		public string PlatformString { get; private set; }

		public string LanguageCode { get; private set; }

		public string LocaleCode { get; private set; }

		public PlatformCulture(string platformCultureString)
		{
			if (String.IsNullOrEmpty(platformCultureString))
			{
				throw new ArgumentException("Expected culture identifier", "platformCultureString"); // in C# 6 use nameof(platformCultureString)
			}
			
			if (SupportedCultures.TryGetValue(platformCultureString, out LanguageModel platformLanguageModel))
				PlatformString = platformLanguageModel.Abreviation.Replace("_", "-"); // .NET expects dash, not underscore
			else
                PlatformString = DefaultCulture;

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

		public override string ToString()
		{
			return PlatformString;
		}
	}
}
