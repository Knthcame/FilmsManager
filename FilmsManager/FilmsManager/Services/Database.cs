using FilmsManager.Services.Interfaces;
using Models.Classes;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FilmsManager.Services
{
    public class Database : IDatabase
    {
        private const string DatabaseName = "films_manager_database.db3";

        private readonly SQLiteAsyncConnection _database;

        public Database(IDatabasePath databasePath)
        {
            _database = new SQLiteAsyncConnection(Path.Combine(databasePath.GetDatabasePath(), DatabaseName));
            _database.CreateTablesAsync<MovieModel, GenreModel>();
        }

        public async Task<bool> AddOrUpdateAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : IEntity, new()
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

        public async Task<bool> AddOrUpdateAsync<TEntity>(TEntity entity) where TEntity : IEntity, new()
        {
            int i;
            if (string.IsNullOrEmpty(entity.Id))
                i = await _database.InsertAsync(entity);
            else
                i = await _database.UpdateAsync(entity);
            return true;
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync<TEntity>() where TEntity : new()
        {
            return await _database.Table<TEntity>().ToListAsync();
        }

        public async Task<TEntity> FindAsync<TEntity>(string id) where TEntity : IEntity, new()
        {
            return await _database.Table<TEntity>().Where(entity=> entity.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> RemoveAllAsync<TEntity>() where TEntity : new()
        {
            var result = true;

            var entities = await FindAllAsync<TEntity>();
            foreach( TEntity entity in entities)
            {
                if (!await RemoveAsync(entity))
                    result = false;
            }

            return result;
        }

        public async Task<bool> RemoveAsync<TEntity>(TEntity entity) where TEntity : new()
        {
            return (await _database.DeleteAsync(entity) > 0);
        }
    }
}
