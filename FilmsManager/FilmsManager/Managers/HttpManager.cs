using FilmsManager.Events;
using FilmsManager.Managers.Interfaces;
using FilmsManager.Services.Interfaces;
using Models.Classes;
using Models.Constants;
using Plugin.Connectivity;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsManager.Managers
{
    public class HttpManager : IHttpManager
    {
        private readonly IDatabaseManager _databaseManager;

        private readonly IRestService _restService;

        private readonly IUrlService _urlService;

        private readonly IEventAggregator _eventAggregator;

        public HttpManager(IDatabaseManager databaseManager, IRestService restService, IUrlService urlService, IEventAggregator eventAggregator)
        {
            _databaseManager = databaseManager;
            _restService = restService;
            _urlService = urlService;
            _eventAggregator = eventAggregator;
        }

        public async Task<TResponse> RefreshDataAsync<TEntity, TResponse>() 
            where TEntity : class, IEntity, new() 
            where TResponse : class, new()
        {
            try
            {
                if (await IsApiReachableAsync<TEntity>())
                {
                    return await _restService.RefreshDataAsync<TEntity, TResponse>();
                }
                else
                {
                    var response = await _databaseManager.FindAllAsync<TEntity, TResponse>();

                    if (typeof(TEntity) == typeof(GenreModel) && IsGenresNullOrEmpty(response as GenreResponse))
                    {
                        var genres = GetDefaultGenres();
                        response = GetDefaultGenres() as TResponse;
                    }
                    if (typeof(TResponse) == typeof(List<MovieModel>))
                    {
                        _eventAggregator.GetEvent<MovieListRefreshedEvent>().Publish(); //Sometimes the refreshing icon stays on anyway 
                    }

                    return response;
                }
            }catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return default(TResponse);
            }
        }

        public async Task SaveEntityAsync<TEntity>(TEntity entity, bool isNewItem)
            where TEntity : class, IEntity, new()
        {
            if(await IsApiReachableAsync<TEntity>())
            {
                _restService.SaveEntityAsync(entity, isNewItem);
            }
            _databaseManager.AddOrUpdateAsync(entity);
        }
        public async Task DeleteEntityAsync<TEntity>(TEntity entity) 
            where TEntity : IEntity, new()
        {
            if(await IsApiReachableAsync<TEntity>())
            {
                _restService.DeleteEntityAsync<TEntity>(entity.Id);
            }
            _databaseManager.RemoveAsync(entity);
        }

        public async Task<bool> IsApiReachableAsync<TEntity>()
        {
            var url = _urlService.GetUri<TEntity>(string.Empty).AbsoluteUri;
            return await CrossConnectivity.Current.IsReachable(url, 3000);
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
    }
}
