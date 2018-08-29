using FilmsManager.Managers.Interfaces;
using FilmsManager.Services.Interfaces;
using Models.Classes;
using Plugin.Connectivity;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FilmsManager.Managers
{
    public class HttpManager : IHttpManager
    {
        private readonly IDatabase _database;

        private readonly IRestService _restService;

        private readonly IUrlService _urlService;

        public HttpManager(IDatabase database, IRestService restService, IUrlService urlService)
        {
            _database = database;
            _restService = restService;
            _urlService = urlService;
        }

        public async Task<TResponse> RefreshDataAsync<TEntity, TResponse>() where TEntity : IEntity, new() where TResponse : class, new()
        {
            try
            {
                if (await IsApiReachableAsync<TEntity>())
                {
                    return await _restService.RefreshDataAsync<TEntity, TResponse>();
                }
                else
                {
                    var response = await _database.FindAllAsync<TEntity, TResponse>();

                    if (typeof(TEntity) == typeof(GenreModel) && response == null)
                    {
                        var genres = GetDefaultGenres();
                        response = GetDefaultGenres() as TResponse;
                    }

                    return response;
                }
            }catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return default(TResponse);
            }
        }

        public async Task SaveEntityAsync<TEntity>(TEntity entity, bool isNewItem) where TEntity : IEntity, new()
        {
            if(await IsApiReachableAsync<TEntity>())
            {
                _restService.SaveEntityAsync(entity, isNewItem);
            }
            _database.AddOrUpdateAsync(entity);
        }
        public async Task DeleteEntityAsync<TEntity>(TEntity entity) where TEntity : IEntity, new()
        {
            if(await IsApiReachableAsync<TEntity>())
            {
                _restService.DeleteEntityAsync<TEntity>(entity.Id);
            }
            _database.RemoveAsync(entity);
        }

        public async Task<bool> IsApiReachableAsync<TEntity>()
        {
            var url = _urlService.GetUri<TEntity>(string.Empty).AbsoluteUri;
            return await CrossConnectivity.Current.IsReachable(url, 3000);
        }

        public GenreResponse GetDefaultGenres()
        {
            var response = new GenreResponse();
            _database.AddOrUpdateAsync(response);
            return response;
        }
    }
}
