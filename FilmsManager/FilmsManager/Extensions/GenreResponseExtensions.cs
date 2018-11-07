﻿using System.Collections.Generic;
using Models.Classes;

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
                case "English":
                    return response.English;
                case "Spanish":
                    return response.Spanish;
                default:
                    return response.English;
            }
        }
    }
}
