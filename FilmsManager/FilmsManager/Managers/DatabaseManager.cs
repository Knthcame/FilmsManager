using FilmsManager.Managers.Interfaces;
using FilmsManager.Services.Interfaces;
using Models.Classes;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using FilmsManager.Models;

namespace FilmsManager.Managers
{
    public class DatabaseManager : IDatabaseManager
    {
        public IDatabase Database { get; set; }

        public DatabaseManager(IDatabase database)
        {
            Database = database;
        }

        public async Task<TResponse> FindAllAsync<TEntity, TResponse>()
            where TEntity : class, IEntity, new()
            where TResponse : class, new()
        {
            if (typeof(TEntity) == typeof(MovieModel))
            {
                var result = new List<MovieModel>();
                var movies = await Database.FindAllAsync<TEntity, TResponse>() as List<MovieModel>;
                foreach (MovieModel movie in movies)
                {
                    result.Add(DeserializeMovie<TEntity>(movie) as MovieModel);
                }
                return result as TResponse;
            }
            else if (typeof(TResponse) == typeof(GenreResponse))
            {
                var genres = await Database.FindAllAsync<TEntity, TResponse>();
                if (genres == null)
                    return default(TResponse);

                return DeserializeGenreResponse(genres);
            }
            else if (typeof(TResponse) == typeof(LanguageModel))
                return await Database.FindAllAsync<TEntity, TResponse>();

            else
                return await Database.FindAllAsync<TEntity, TResponse>();
        }

        public async Task<TEntity> FindAsync<TEntity>(int id) 
            where TEntity : class, IEntity, new()
        {
            if (typeof(TEntity) == typeof(MovieModel))
            {
                MovieModel movie = await Database.FindAsync<TEntity>(id) as MovieModel;
                return DeserializeMovie<TEntity>(movie);
            }
            else
            {
                return await Database.FindAsync<TEntity>(id);
            }
        }
        public async Task<bool> AddOrUpdateAsync<TEntity>(IEnumerable<TEntity> entities) 
            where TEntity : class, IEntity, new()
        {
            foreach (TEntity entity in entities)
            {
                if (!await AddOrUpdateAsync(entity))
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> AddOrUpdateAsync<TEntity>(TEntity entity) 
            where TEntity : class, IEntity, new()
        {
            if (typeof(TEntity) == typeof(GenreResponse))
                return await Database.AddOrUpdateAsync(SerializeGenreResponse(entity), true);
            else if (typeof(TEntity) == typeof(MovieModel))
            {
                return await Database.AddOrUpdateAsync(SerializeMovie(entity));
            }
            else if (typeof(TEntity) == typeof(LanguageModel))
                return await Database.AddOrUpdateAsync(entity, true);
            else
                return await Database.AddOrUpdateAsync(entity);
        }

        public async Task<bool> RemoveAllAsync<TEntity>() 
            where TEntity : new()
        {
            return await Database.RemoveAllAsync<TEntity>();
        }

        public async Task<bool> RemoveAsync<TEntity>(TEntity entity) 
            where TEntity : new()
        {
            return await Database.RemoveAsync(entity);
        }

        private TResponse SerializeGenreResponse<TResponse>(TResponse response)
            where TResponse : class
        {
            var genres = response as GenreResponse;
            genres.EnglishGenresBlobbed = SerializeGenreList(genres.English);
            genres.SpanishGenresBlobbed = SerializeGenreList(genres.Spanish);
            return genres as TResponse;
        }

        private string SerializeGenreList(IList<GenreModel> list)
        {
            if (list == null)
                return null;
            else
                return JsonConvert.SerializeObject(list);
        }

        private TResponse DeserializeGenreResponse<TResponse>(TResponse response)
            where TResponse : class
        {
            var genres = response as GenreResponse;
            genres.English = DeserializeGenreList(genres.EnglishGenresBlobbed);
            genres.Spanish = DeserializeGenreList(genres.SpanishGenresBlobbed);
            return genres as TResponse;
        }

        private IList<GenreModel> DeserializeGenreList(string listBlobbed)
        {
            if (listBlobbed == null)
                return null;
            else
                return JsonConvert.DeserializeObject<List<GenreModel>>(listBlobbed);
        }

        private TEntity SerializeMovie<TEntity>(TEntity entity)
            where TEntity : class, IEntity, new()
        {
            var movie = entity as MovieModel;
            if (movie.Genre == null)
                return default(TEntity);

            movie.GenreBlobbed = JsonConvert.SerializeObject(movie.Genre);
            return movie as TEntity;
        }

        private TEntity DeserializeMovie<TEntity>(MovieModel movie)
            where TEntity : class, IEntity, new()
        {
            if (movie == null)
                return default(TEntity);

            movie.Genre = JsonConvert.DeserializeObject<GenreModel>(movie.GenreBlobbed);
            return movie as TEntity;
        }
    }
}
