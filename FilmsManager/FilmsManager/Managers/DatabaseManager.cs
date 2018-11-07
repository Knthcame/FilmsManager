using System.Collections.Generic;
using System.Threading.Tasks;
using FilmsManager.Logging.Interfaces;
using FilmsManager.Managers.Interfaces;
using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using Models.Classes;
using Newtonsoft.Json;
using Prism.Logging;

namespace FilmsManager.Managers
{
    public class DatabaseManager : IDatabaseManager
    {
        public IDatabase Database { get; set; }

        private readonly ICustomLogger _logger;

        public DatabaseManager(IDatabase database, ICustomLogger logger)
        {
            Database = database;
            _logger = logger;
        }

        public async Task<TResponse> FindAllAsync<TEntity, TResponse>()
            where TEntity : class, IEntity, new()
            where TResponse : class, new()
        {
            TResponse response = default(TResponse);

            if (typeof(TEntity) == typeof(MovieModel))
            {
                var list = new List<MovieModel>();
                var movies = await Database.FindAllAsync<TEntity, TResponse>() as List<MovieModel>;
                foreach (MovieModel movie in movies)
                {
                    list.Add(DeserializeMovie<TEntity>(movie) as MovieModel);
                }
                response = list as TResponse;
            }
            else if (typeof(TResponse) == typeof(GenreResponse))
            {
                var genres = await Database.FindAllAsync<TEntity, TResponse>();
                if (genres == null)
                    return default(TResponse);

                response = DeserializeGenreResponse(genres);
            }
            else
                response = await Database.FindAllAsync<TEntity, TResponse>();

            if (response == null)
                _logger.Log($"Failed to get {typeof(TEntity).Name} from database", Category.Warn, Priority.Medium);
            else
                _logger.Log($"Succesfully got {typeof(TEntity).Name} from database", Category.Info, Priority.Low);

            return response;
        }

        public async Task<TEntity> FindAsync<TEntity>(int id) 
            where TEntity : class, IEntity, new()
        {
            TEntity entity = default(TEntity);

            if (typeof(TEntity) == typeof(MovieModel))
            {
                MovieModel movie = await Database.FindAsync<TEntity>(id) as MovieModel;
                entity = DeserializeMovie<TEntity>(movie);
            }
            else
            {
                entity = await Database.FindAsync<TEntity>(id);
            }

            if (entity == null)
                _logger.Log($"Failed to get {typeof(TEntity).Name} from database", Category.Warn, Priority.Medium);
            else
                _logger.Log($"Succesfully got {typeof(TEntity).Name} from database", Category.Info, Priority.Low);

            return entity;
        }

        public async Task<bool> AddOrUpdateAsync<TEntity>(IEnumerable<TEntity> entities) 
            where TEntity : class, IEntity, new()
        {
            int fails = 0;
            int oks = 0;
            foreach (TEntity entity in entities)
            {
                if (!await AddOrUpdateAsync(entity))
                    fails++;
            }
            _logger.Log($"Added/updated {oks}/{oks + fails} {typeof(TEntity).Name}", Category.Info, Priority.Medium);
            return fails == 0;
        }

        public async Task<bool> AddOrUpdateAsync<TEntity>(TEntity entity) 
            where TEntity : class, IEntity, new()
        {
            bool succes;
            if (typeof(TEntity) == typeof(GenreResponse))
                succes = await Database.AddOrUpdateAsync(SerializeGenreResponse(entity), true);
            else if (typeof(TEntity) == typeof(MovieModel))
            {
                succes = await Database.AddOrUpdateAsync(SerializeMovie(entity));
            }
            else
                succes = await Database.AddOrUpdateAsync(entity, typeof(TEntity) == typeof(LanguageModel));

            if (succes)
                _logger.Log($"Succesfully added/updated a {typeof(TEntity).Name}", Category.Info, Priority.Low);
            else
                _logger.Log($"Failed to add/update a {typeof(TEntity).Name}", Category.Info, Priority.Low);

            return succes;
        }

        public async Task<bool> RemoveAllAsync<TEntity>() 
            where TEntity : new()
        {
            var succes = await Database.RemoveAllAsync<TEntity>();
            if (succes)
                _logger.Log($"Succesfully removed all {typeof(TEntity).Name}", Category.Info, Priority.Low);
            else
                _logger.Log($"Failed to remove all {typeof(TEntity).Name}", Category.Warn, Priority.High);

            return succes;
        }

        public async Task<bool> RemoveAsync<TEntity>(TEntity entity) 
            where TEntity : new()
        {
            var succes = await Database.RemoveAsync(entity);
            if (succes)
                _logger.Log($"Succesfully removed {typeof(TEntity).Name}", Category.Info, Priority.Low);
            else
                _logger.Log($"Failed to remove {typeof(TEntity).Name}", Category.Warn, Priority.High);

            return succes;
        }

        private TResponse SerializeGenreResponse<TResponse>(TResponse response)
            where TResponse : class
        {
            var genres = response as GenreResponse;
            genres.EnglishGenresBlobbed = SerializeGenreList(genres.English);
            genres.SpanishGenresBlobbed = SerializeGenreList(genres.Spanish);
            return genres as TResponse;
        }

        private TResponse DeserializeGenreResponse<TResponse>(TResponse response)
            where TResponse : class
        {
            var genres = response as GenreResponse;
            genres.English = DeserializeGenreList(genres.EnglishGenresBlobbed);
            genres.Spanish = DeserializeGenreList(genres.SpanishGenresBlobbed);
            return genres as TResponse;
        }

        private string SerializeGenreList(IList<GenreModel> list)
        {
            if (list == null)
                return null;
            else
                return JsonConvert.SerializeObject(list);
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
