using Models.Classes;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Prism.Events;
using FilmsManager.Events;
using Prism.Logging;
using FilmsManager.Logging.Interfaces;
using FilmsManager.Services.Interfaces;

namespace FilmsManager.Services
{
    public class RestService : IRestService
    {
        private readonly IEventAggregator _eventAggregator;

        private readonly ICustomLogger _logger;

        private readonly IUrlService _urlService;

        private readonly IHttpService _httpService;

        public RestService(IEventAggregator eventAggregator, ICustomLogger logger, IUrlService urlService, IHttpService httpService)
        {
            _eventAggregator = eventAggregator;
            _logger = logger;
            _urlService = urlService;
            _httpService = httpService;
        }

        public async Task<TResponse> RefreshDataAsync<TEntity, TResponse>() where TEntity : IEntity
        {
            var result = default(TResponse);

            var uri = _urlService.GetUri<TEntity>();

            if (uri == null)
                return result;

            try
            {
                var response = await _httpService.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<TResponse>(content);
                    _logger.Log($"{typeof(TEntity).Name}(s) succesfully retrieved from API", Category.Info, Priority.Low);
                }
                else
                    _logger.Log($"{typeof(TEntity).Name}(s) failed retrieve from API", Category.Warn, Priority.High);
            }
            catch (Exception ex)
            {
                _logger.Log($"ERROR {ex.Message}", Category.Exception, Priority.High);
                _eventAggregator.GetEvent<ConnectionErrorEvent>().Publish();
            }
            return result;
        }

        public async Task SaveEntityAsync<TEntity>(TEntity entity, bool isNewItem) where TEntity : IEntity
        {
            var uri = _urlService.GetUri<TEntity>();
            if (uri == null)
                return;

            try
            {
                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await _httpService.PostAsync(uri, entity);
                }
                else
                {
                    response = await _httpService.PutAsync(uri, entity);
                }

                if (response.IsSuccessStatusCode)
                {
                    _logger.Log($"{typeof(TEntity).Name} successfully saved in the API", Category.Info, Priority.Medium);
                }
                else
                    _logger.Log($"{typeof(TEntity).Name} failed to be saved in the API", Category.Warn, Priority.High);
            }
            catch (Exception ex)
            {
                _logger.Log($"ERROR {ex.Message}", Category.Exception, Priority.High);
            }
        }

        public async Task DeleteEntityAsync<TEntity>(int id) where TEntity : IEntity
        {
            var uri = _urlService.GetUri<TEntity>(id.ToString());
            if (uri == null)
                return;

            try
            {
                var response = await _httpService.RemoveAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    _logger.Log($"{typeof(TEntity).Name} successfully deleted.", Category.Info, Priority.Medium);
                }
                else
                    _logger.Log($"{typeof(TEntity).Name} failed to be deleted.", Category.Warn, Priority.High);
            }
            catch (Exception ex)
            {
                _logger.Log($"ERROR {ex.Message}", Category.Exception, Priority.High);
            }
        }
    }
}
