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

namespace Models.ApiServices
{
    public class RestService : IRestService
    {
		HttpClient client;

		public List<ToDoItem> Items { get; set; }

		public RestService()
		{
			client = new HttpClient
			{
				MaxResponseContentBufferSize = 256000
			};
		}

		public async Task<List<ToDoItem>> RefreshDataAsync()
		{
			Items = new List<ToDoItem>();

			// RestUrl = http://developer.xamarin.com:8081/api/ToDoItems
			var uri = new Uri(string.Format(ApiConstants.RestUrl, string.Empty));

			try
			{
				var response = await client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					Items = JsonConvert.DeserializeObject<List<ToDoItem>>(content);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}

			return Items;
		}

		public async Task SaveToDoItemAsync(ToDoItem item, bool isNewItem)
		{
			// RestUrl = http://developer.xamarin.com:8081/api/todoitems
			var uri = new Uri(string.Format(ApiConstants.RestUrl, string.Empty));

			try
			{
				var json = JsonConvert.SerializeObject(item);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = null;
				if (isNewItem)
				{
					response = await client.PostAsync(uri, content);
				}
				else
				{
					response = await client.PutAsync(uri, content);
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

		public async Task DeleteToDoItemAsync(string id)

		{
			// RestUrl = http://developer.xamarin.com:8081/api/todoitems/{0}
			var uri = new Uri(string.Format(ApiConstants.RestUrl, id));

			try
			{
				var response = await client.DeleteAsync(uri);

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
