using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace apiJMBROWS.Servicios
{
    public class StorageImagenAzure : IStorageImagenes
    {
        private readonly BlobContainerClient _container;

        public StorageImagenAzure(IConfiguration cfg)
        {
            var conn = cfg["AzureBlobStorage:ConnectionString"]!;
            var cont = cfg["AzureBlobStorage:ContainerName"]!;
            _container = new BlobContainerClient(conn, cont);
        }

        public async Task<string> SubirAsync(IFormFile archivo, string nombreDestino)
        {
            await _container.CreateIfNotExistsAsync();

            var blob = _container.GetBlobClient(nombreDestino);

            // Elimina si ya existía para evitar conflicto
            await blob.DeleteIfExistsAsync();

            var headers = new BlobHttpHeaders { ContentType = archivo.ContentType };

            await using var stream = archivo.OpenReadStream();
            await blob.UploadAsync(stream, headers);   // sobrecarga con HttpHeaders

            return blob.Uri.ToString();                // URL pública
        }

        public async Task EliminarAsync(string url)
        {
            // Obtiene solo el nombre del blob (después del container)
            var blobName = Path.GetFileName(new Uri(url).LocalPath);
            var blob = _container.GetBlobClient(blobName);
            await blob.DeleteIfExistsAsync();
        }
    }
}
