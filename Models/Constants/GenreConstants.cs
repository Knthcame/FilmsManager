using Models.Classes;
using System.Collections.Generic;

namespace Models.Constants
{
    public static class GenreConstants
    {
        public static GenreResponse DefaultGenres { get; } = new GenreResponse()
        {
            English = new List<GenreModel>()
            {
                new GenreModel(GenreKeys.ActionGenre, "Action"),
                new GenreModel(GenreKeys.DramaGenre, "Drama"),
                new GenreModel(GenreKeys.FantasyGenre, "Fantasy"),
                new GenreModel(GenreKeys.HumourGenre, "Humour"),
                new GenreModel(GenreKeys.ScienceFictionGenre, "Science fiction"),
                new GenreModel(GenreKeys.SuperHeroesGenre, "Super heroes"),
                new GenreModel(GenreKeys.TerrorGenre, "Terror")
            },
            Spanish = new List<GenreModel>()
            {
                new GenreModel(GenreKeys.ActionGenre, "Acción"),
                new GenreModel(GenreKeys.DramaGenre, "Drama"),
                new GenreModel(GenreKeys.FantasyGenre, "Fantasía"),
                new GenreModel(GenreKeys.HumourGenre, "Humor"),
                new GenreModel(GenreKeys.ScienceFictionGenre, "Ciencia ficción"),
                new GenreModel(GenreKeys.SuperHeroesGenre, "Super héroes"),
                new GenreModel(GenreKeys.TerrorGenre, "Miedo")
            }
        };
    }
}
