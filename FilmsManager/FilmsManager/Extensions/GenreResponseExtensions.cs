using Models.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmsManager.Extensions
{
    public static  class GenreResponseExtensions
    {
        public static IList<GenreModel> GetGenresInChosenLanguage(this GenreResponse response, string culture)
        {
            if (response == null)
                return null;

            switch (culture)
            {
                case "en":
                    return response.English;
                case "es":
                    return response.Spanish;
                default:
                    return response.English;
            }
        }
    }
}
