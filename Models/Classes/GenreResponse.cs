using Models.Constants;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Models.Classes
{
    public class GenreResponse : IEntity
    {
        [JsonProperty("en-EN")]
        public IList<GenreModel> English { get; set; } = new List<GenreModel>()
        {
            new GenreModel(GenreKeys.ActionGenre, "Action"),
            new GenreModel(GenreKeys.DramaGenre, "Drama"),
            new GenreModel(GenreKeys.FantasyGenre, "Fantasy"),
            new GenreModel(GenreKeys.HumourGenre, "Humour"),
            new GenreModel(GenreKeys.ScienceFictionGenre, "Science fiction"),
            new GenreModel(GenreKeys.SuperHeroesGenre, "Super heroes"),
            new GenreModel(GenreKeys.TerrorGenre, "Terror")
        };

        [JsonProperty("es-ES")]
        public IList<GenreModel> Spanish { get; set; } = new List<GenreModel>()
        {
            new GenreModel(GenreKeys.ActionGenre, "Acción"),
            new GenreModel(GenreKeys.DramaGenre, "Drama"),
            new GenreModel(GenreKeys.FantasyGenre, "Fantasía"),
            new GenreModel(GenreKeys.HumourGenre, "Humor"),
            new GenreModel(GenreKeys.ScienceFictionGenre, "Ciencia ficción"),
            new GenreModel(GenreKeys.SuperHeroesGenre, "Super héroes"),
            new GenreModel(GenreKeys.TerrorGenre, "Miedo")
        };
        public string Id { get; set; } = null;
    }
}
