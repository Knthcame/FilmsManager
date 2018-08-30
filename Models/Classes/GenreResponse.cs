using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace Models.Classes
{
    [Table(nameof(GenreResponse))]
    public class GenreResponse : IEntity
    {
        [JsonProperty("en-EN")]
        [TextBlob(nameof(EnglishGenresBlobbed))]
        public IList<GenreModel> English { get; set; }

        public string EnglishGenresBlobbed { get; set; }

        [JsonProperty("es-ES")]
        [TextBlob(nameof(SpanishGenresBlobbed))]
        public IList<GenreModel> Spanish { get; set; }

        public string SpanishGenresBlobbed { get; set; }

        public int Id { get; set; }
    }
}
