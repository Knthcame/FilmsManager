using FilmsManager.Constants;
using Models.Classes;
using System;
using System.Collections.Generic;

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

        public Uri GetUri<TEntity>(string routeExtension)
        {
            var type = typeof(TEntity);
            if (!_controller.TryGetValue(type, out string controller))
                return null;

            return new Uri(string.Format(ApiConstants.RestUrl + controller, routeExtension));
        }
    }
}
