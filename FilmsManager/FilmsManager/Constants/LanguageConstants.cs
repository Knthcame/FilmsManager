using FilmsManager.Models;
using Models.Resources;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FilmsManager.Constants
{
    public static class LanguageConstants
    {
        private static LanguageModel _englishLanguage = new LanguageModel("English", "en", AppImages.UKFlag);

        private static LanguageModel _spanishLanguage = new LanguageModel("Spanish", "es", AppImages.SpainFlag);

        public const string DefaultCulture = "en-EN";

        public static LanguageModel DefaultLanguage = _englishLanguage;

        public static Dictionary<string, LanguageModel> SupportedCultures = new Dictionary<string, LanguageModel>()
        {
            { "en", _englishLanguage},
            { "es", _spanishLanguage}
        };

        public static IList<LanguageModel> SupportedLanguageModels = new ObservableCollection<LanguageModel>()
        {
            _englishLanguage,
            _spanishLanguage
        };
    }
}
