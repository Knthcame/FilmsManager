﻿using Models.ApiServices.Interfaces;
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
		HttpClient client;

		public List<MovieModel> Items { get; set; }

		public RestService()
		{
			client = new HttpClient
			{
				MaxResponseContentBufferSize = 256000
			};
		}

		public async Task<List<MovieModel>> RefreshDataAsync()
		{
			Items = new List<MovieModel>();
			
			var uri = new Uri(string.Format(ApiConstants.RestUrl, string.Empty));

			try
			{
				var response = await client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					Items = JsonConvert.DeserializeObject<List<MovieModel>>(content);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}

			return Items;
		}

		public async Task SaveToDoItemAsync(MovieModel item, bool isNewItem)
		{
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
