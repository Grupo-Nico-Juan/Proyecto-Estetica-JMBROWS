using apiJMBROWS.Servicios;
using LogicaAplicacion.InterfacesCasosDeUso.ICUServicio;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Http;

namespace LogicaAplicacion.CasosDeUso.CUServicio
{
    public class CUSubirImagenesServicio : ICUSubirImagenesServicio
    {
        private readonly IRepositorioServicios _repoServicios;
        private readonly IRepoServicioImagenes _repoImgs;
        private readonly IStorageImagenes _storage;

        public CUSubirImagenesServicio(
            IRepositorioServicios repoServicios,
            IRepoServicioImagenes repoImgs,
            IStorageImagenes storage)
        {
            _repoServicios = repoServicios;
            _repoImgs = repoImgs;
            _storage = storage;
        }

        public async Task SubirAsync(int servicioId, List<IFormFile> archivos)
        {
            var servicio = _repoServicios.GetById(servicioId)
                          ?? throw new Exception("Servicio no encontrado.");

            if (archivos == null || archivos.Count == 0)
                throw new Exception("No se enviaron archivos.");

            foreach (var file in archivos)
            {
                // validaciones mínimas
                if (!file.ContentType.StartsWith("image/"))
                    throw new Exception($"Archivo {file.FileName} no es una imagen.");

                if (file.Length > 5 * 1024 * 1024) // 5 MB
                    throw new Exception($"Archivo {file.FileName} excede 5 MB.");

                var nombreBlob = $"servicio_{servicioId}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var url = await _storage.SubirAsync(file, nombreBlob);

                _repoImgs.Add(new ServicioImagen
                {
                    ServicioId = servicioId,
                    Url = url
                });
            }

            await _repoImgs.SaveAsync();
        }
    }
}
