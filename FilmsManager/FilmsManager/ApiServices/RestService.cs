using Models.ApiServices.Interfaces;
using Models.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FilmsManager.Constants;
using Prism.Events;
using FilmsManager.Events;
using Prism.Logging;
using FilmsManager.Logging.Interfaces;

namespace Models.ApiServices
{
    public class RestService : IRestService
    {
        private readonly IEventAggregator _eventAggregator;

        private readonly ICustomLogger _logger;

        HttpClient _client;

        private Dictionary<Type, string> _controller = new Dictionary<Type, string>
        {
            {typeof(MovieModel), ApiConstants.MovieController },
            {typeof(GenreModel), ApiConstants.GenreController }
        };

		public RestService(IEventAggregator eventAggregator, ICustomLogger logger)
		{
            _eventAggregator = eventAggregator;
            _logger = logger;
			_client = new HttpClient
			{
				MaxResponseContentBufferSize = 256000
			};
		}

		public async Task<JEntity> RefreshDataAsync<TEntity, JEntity>() where TEntity : IEntity
        {
			JEntity result = default(JEntity);

            var type = typeof(TEntity);
            if (!_controller.TryGetValue(type, out string controller))
                return default(JEntity);

            var uri = new Uri(string.Format(ApiConstants.RestUrl + controller, string.Empty));

			try
			{
                var response = await _client.GetAsync(uri);
                //_client.GetAsync(uri); Should fix the crash, but just stays eternally loading...
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					result = JsonConvert.DeserializeObject<JEntity>(content);
				}
                if (typeof(JEntity) == typeof(IList<MovieModel>))
                {
                    _eventAggregator.GetEvent<MovieListRefreshedEvent>().Publish();
                }
			}
			catch (Exception ex)
			{
				_logger.Log($"				ERROR {ex.Message}", Category.Exception, Priority.);
                _eventAggregator.GetEvent<ConnectionErrorEvent>().Publish();
			}
			return result;
		}

		public async Task SaveEntityAsync<TEntity>(MovieModel item, bool isNewItem) where TEntity : IEntity
		{
            var type = typeof(TEntity);

            if (!_controller.TryGetValue(type, out string controller))
                return;

            var uri = new Uri(string.Format(ApiConstants.RestUrl + controller, string.Empty));

			try
			{
				var json = JsonConvert.SerializeObject(item);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = null;
				if (isNewItem)
				{
					response = await _client.PostAsync(uri, content);
				}
				else
				{
					response = await _client.PutAsync(uri, content);
				}

				if (response.IsSuccessStatusCode)
				{
					_logger.Log($"				{typeof(TEntity).Name} successfully saved.", Category.Info, Priority.Medium);
				}

			}
			catch (Exception ex)
			{
				_logger.Log($"				ERROR {ex.Message}", Category.Exception, Priority.High);
			}
		}

		public async Task DeleteEntityAsync<TEntity>(string id) where TEntity : IEntity

        {
            var type = typeof(TEntity);

            if (!_controller.TryGetValue(type, out string controller))
                return;

            var uri = new Uri(string.Format(ApiConstants.RestUrl + controller, id));

			try
			{
				var response = await _client.DeleteAsync(uri);

				if (response.IsSuccessStatusCode)
				{
					_logger.Log($"				{typeof(TEntity).Name} successfully deleted.", Category.Info, Priority.Medium);
				}

			}
			catch (Exception ex)
			{
                _logger.Log($"				ERROR {ex.Message}", Category.Exception, Priority.High);
            }
		}
	}
}
