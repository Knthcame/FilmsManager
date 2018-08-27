using System;

namespace FilmsManager.Services.Interfaces
{
    public interface IUrlService
    {
        string BaseUrl { get; set; }

        Uri GetUri<TEntity>(string routeExtension = "");
    }
}
