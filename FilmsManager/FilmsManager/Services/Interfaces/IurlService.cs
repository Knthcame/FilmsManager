using System;

namespace FilmsManager.Services.Interfaces
{
    public interface IUrlService
    {
        string BaseUrl { get; set; }

        //Uri GetUri<Tentity>(); Es podria implementar per evitar posar string.Empty

        Uri GetUri<TEntity>(string routeExtension);
    }
}
