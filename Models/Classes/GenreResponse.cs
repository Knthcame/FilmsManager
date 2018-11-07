using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace Models.Classes
{
    [Table(nameof(GenreResponse))]
    public class GenreResponse : IEntity
    {
        [JsonProperty("en-EN")]
        [Ignore]
        public IList<GenreModel> English { get; set; }

        public string EnglishGenresBlobbed { get; set; }

        [JsonProperty("es-ES")]
        [Ignore]
        public IList<GenreModel> Spanish { get; set; }

        public string SpanishGenresBlobbed { get; set; }

        public int Id { get; set; } = 1;
    }
}
