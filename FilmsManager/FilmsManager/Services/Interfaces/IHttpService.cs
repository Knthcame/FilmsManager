using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FilmsManager.Services.Interfaces
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> GetAsync(Uri uri);

        Task<HttpResponseMessage> PostAsync(Uri uri, object body);

        Task<HttpResponseMessage> PutAsync(Uri uri, object body);

        Task<HttpResponseMessage> RemoveAsync(Uri uri);
    }
}
