using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Models.Classes
{
    public class GenresCultureDictionary : Dictionary<string, IList<GenreModel>>
    {
        public static Dictionary<string, IList<GenreModel>> GenresCulture = new Dictionary<string, IList<GenreModel>>()
        {
            {"English", new GenreResponse().English },
            {"Spanish", new GenreResponse().Spanish }

        };
    }
}
