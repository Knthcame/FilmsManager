using FilmsManager.Services.Interfaces;
using Models.Classes;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FilmsManager.Services
{
    public class HttpService : IHttpService
    {
        HttpClient _client;

        public HttpService()
        {
            _client = new HttpClient
            {
                MaxResponseContentBufferSize = 256000
            };
        }

        public Task<HttpResponseMessage> GetAsync(Uri uri)
        {
            return _client.GetAsync(uri);
        }

        public Task<HttpResponseMessage> PostAsync(Uri uri, object body)
        {
            return _client.PostAsync(uri, CreateHttpBody(body));
        }

        public Task<HttpResponseMessage> PutAsync(Uri uri, object body)
        {
            return _client.PutAsync(uri, CreateHttpBody(body));
        }

        public Task<HttpResponseMessage> RemoveAsync(Uri uri)
        {
            return _client.DeleteAsync(uri);
        }

        private StringContent CreateHttpBody(object body)
        {
            var json = JsonConvert.SerializeObject(body);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
