using System;
using System.Linq;
using System.Threading.Tasks;
using FilmsManager.Logging.Interfaces;
using FilmsManager.Managers.Interfaces;
using FilmsManager.Models;
using FilmsManager.Services.Interfaces;
using Models.Classes;
using Models.Constants;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Prism.Events;
using Prism.Logging;

namespace FilmsManager.Managers
{
    public class HttpManager : IHttpManager
    {
        private readonly IDatabaseManager _databaseManager;

        private readonly IRestService _restService;

        private readonly IUrlService _urlService;

        private readonly IEventAggregator _eventAggregator;

        private readonly ICustomLogger _logger;

        private bool isApiUnsynchronized;

        public HttpManager(IDatabaseManager databaseManager, IRestService restService, IUrlService urlService, IEventAggregator eventAggregator, ICustomLogger logger)
        {
            _databaseManager = databaseManager;
            _restService = restService;
            _urlService = urlService;
            _eventAggregator = eventAggregator;
            _logger = logger;
            CrossConnectivity.Current.ConnectivityChanged += async(sender, args) => await Current_ConnectivityChangedAsync(sender, args);
        }

        public async Task<TResponse> RefreshDataAsync<TEntity, TResponse>() 
            where TEntity : class, IEntity, new() 
            where TResponse : class, new()
        {
            try
            {
                var response = default(TResponse);
                if (typeof(TEntity) == typeof(LanguageModel))
                    response = await _databaseManager.FindAllAsync<TEntity, TResponse>();

                else if (await IsApiReachableAsync<TEntity>())
                {
                    response = await _restService.RefreshDataAsync<TEntity, TResponse>();
                    //_databaseManager.
                }
                else
                {
                    response = await _databaseManager.FindAllAsync<TEntity, TResponse>();

                    if (typeof(TEntity) == typeof(GenreModel) && IsGenresNullOrEmpty(response as GenreResponse))
                    {
                        var genres = GetDefaultGenres();
                        response = GetDefaultGenres() as TResponse;
                        _databaseManager.AddOrUpdateAsync(genres);
                    }
                }
                return response;

            }catch (Exception ex)
            {
                _logger.Log(ex.Message, Category.Exception, Priority.High);
                return default(TResponse);
            }
        }

        public async Task SaveEntityAsync<TEntity>(TEntity entity, bool isNewItem)
            where TEntity : class, IEntity, new()
        {
            if(await IsApiReachableAsync<TEntity>() && typeof(TEntity) != typeof(LanguageModel))
            {
                _restService.SaveEntityAsync(entity, isNewItem);
            }
            _databaseManager.AddOrUpdateAsync(entity);
        }
        public async Task DeleteEntityAsync<TEntity>(TEntity entity) 
            where TEntity : IEntity, new()
        {
            if(await IsApiReachableAsync<TEntity>() && typeof(TEntity) != typeof(LanguageModel))
            {
                _restService.DeleteEntityAsync<TEntity>(entity.Id);
            }
            _databaseManager.RemoveAsync(entity);
        }

        public async Task<bool> IsApiReachableAsync<TEntity>()
        {
            var uri = _urlService.GetUri<TEntity>();
            return await CrossConnectivity.Current.IsRemoteReachable(uri.Host, uri.Port, 3000);
        }

        public GenreResponse GetDefaultGenres()
        {
            var response = GenreConstants.DefaultGenres;
            _databaseManager.AddOrUpdateAsync(response);
            return response;
        }

        private bool IsGenresNullOrEmpty(GenreResponse response)
        {
            if (response == null)
                return true;
            if (response.English == null || response.Spanish == null)
                return true;
            else
                return !response.English.Any() && !response.Spanish.Any();
        }

        private async Task Current_ConnectivityChangedAsync(object sender, ConnectivityChangedEventArgs args)
        {
            if (args.IsConnected && await IsApiReachableAsync<MovieModel>())
            {
                SyncApi<MovieModel>();
                //SyncApi<GenreModel>(); Unnecessary for now
            }
        }

        private void SyncApi<TEntity>()
        {

        }

        private void SyncDatabase<TEntity>()
        {

        }
    }
}
