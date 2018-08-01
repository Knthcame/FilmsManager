using Models.ApiServices.Interfaces;
using Models.Constants;
using Models.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FilmsManager.Constants;

namespace Models.ApiServices
{
    public class RestService : IRestService
    {
		HttpClient _client;

        private Dictionary<Type, string> _controller = new Dictionary<Type, string>
        {
            {typeof(MovieModel), ApiConstants.MovieController },
            {typeof(GenreModel), ApiConstants.GenreController }
        };

		public RestService()
		{
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
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					result = JsonConvert.DeserializeObject<JEntity>(content);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}

			return result;
		}

		public async Task SaveToDoItemAsync<TEntity>(MovieModel item, bool isNewItem) where TEntity : IEntity
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
					Debug.WriteLine(@"				TodoItem successfully saved.");
				}

			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
		}

		public async Task DeleteToDoItemAsync<TEntity>(string id) where TEntity : IEntity

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
					Debug.WriteLine(@"				TodoItem successfully deleted.");
				}

			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
		}
	}
}
