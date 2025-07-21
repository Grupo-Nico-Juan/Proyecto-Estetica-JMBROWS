using Microsoft.AspNetCore.Http;

namespace apiJMBROWS.Servicios
{
    public interface IStorageImagenes
    {
        Task<string> SubirAsync(IFormFile archivo, string nombreDestino);
        Task EliminarAsync(string url);
    }
}
