using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FilmsManager.Logging.Interfaces;
using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using Models.Classes;
using Prism.Logging;
using SQLite;

namespace FilmsManager.Services
{
    public class Database : IDatabase
    {
        private const string DatabaseName = "films_manager_database.db3";

        private readonly SQLiteAsyncConnection _database;

        public Database(IDatabasePath databasePath, ICustomLogger logger)
        {
            var path = Path.Combine(databasePath.GetDatabasePath(), DatabaseName);
            logger.Log(path, Category.Debug, Priority.Medium);

            _database = new SQLiteAsyncConnection(path);
            _database.CreateTablesAsync<MovieModel, GenreResponse, LanguageModel>().Wait();
        }

        public async Task<TResponse> FindAllAsync<TEntity, TResponse>() 
            where TEntity : new() 
            where TResponse : class, new()
        {
            var response = default(TResponse);

            if (typeof(TResponse) == typeof(GenreResponse))
                response = await _database.Table<TResponse>().FirstOrDefaultAsync();

            else if (typeof(TResponse) == typeof(LanguageModel))
                response = await _database.Table<TResponse>().FirstOrDefaultAsync();

            else
                response = await _database.Table<TEntity>().ToListAsync() as TResponse;

            return response;
        }

        public async Task<TEntity> FindAsync<TEntity>(int id) 
            where TEntity : IEntity, new()
        {
            return await _database.Table<TEntity>().Where(entity => entity.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> AddOrUpdateAsync<TEntity>(IEnumerable<TEntity> entities) 
            where TEntity : IEntity, new()
        {
            foreach (TEntity entity in entities)
            {
                if (!await AddOrUpdateAsync(entity, entity.Id > 0))
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> AddOrUpdateAsync<TEntity>(TEntity entity, bool isSingular = false) 
            where TEntity : IEntity, new()
        {
            if (isSingular)
            {
                await _database.DropTableAsync<TEntity>();
                await _database.CreateTableAsync<TEntity>();
                return await _database.InsertAsync(entity) > 0;
            }
            else
            {
                if (entity.Id == 0)
                    return await _database.InsertAsync(entity) > 0;
                else
                    return await _database.UpdateAsync(entity) > 0;
            }
        }

        public async Task<bool> RemoveAllAsync<TEntity>() 
            where TEntity : new()
        {
            var result = true;

            await _database.DropTableAsync<TEntity>();
            await _database.CreateTableAsync<TEntity>();

            return result;
        }

        public async Task<bool> RemoveAsync<TEntity>(TEntity entity) 
            where TEntity : new()
        {
            return (await _database.DeleteAsync(entity) > 0);
        }
    }
}
