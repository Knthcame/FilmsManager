using System;
using System.Collections.Generic;
using FilmsManager.Constants;
using Models.Classes;

namespace FilmsManager.Services.Interfaces
{
    public class UrlService : IUrlService
    {
        private Dictionary<Type, string> _controller = new Dictionary<Type, string>
        {
            {typeof(MovieModel), ApiConstants.MovieController },
            {typeof(GenreModel), ApiConstants.GenreController }
        };

        public string BaseUrl { get; set; } = ApiConstants.RestUrl;

        public Uri GetUri<TEntity>(string routeExtension = "")
        {
            var type = typeof(TEntity);
            if (_controller.TryGetValue(type, out string controller))
                return new Uri(string.Format(ApiConstants.RestUrl + controller, routeExtension));
            else
                return new Uri(string.Format(ApiConstants.RestUrl, routeExtension));
        }
    }
}
