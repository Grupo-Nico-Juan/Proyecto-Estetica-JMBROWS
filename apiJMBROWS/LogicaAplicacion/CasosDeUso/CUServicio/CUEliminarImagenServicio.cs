using apiJMBROWS.Servicios;
using LogicaAplicacion.InterfacesCasosDeUso.ICUServicio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CUServicio
{
    public class CUEliminarImagenServicio : ICUEliminarImagenServicio
    {
        private readonly IRepoServicioImagenes _repoImgs;
        private readonly IStorageImagenes _storage;

        public CUEliminarImagenServicio(IRepoServicioImagenes repoImgs, IStorageImagenes storage)
        {
            _repoImgs = repoImgs;
            _storage = storage;
        }

        public async Task Ejecutar(int imagenId)
        {
            var img = _repoImgs.Get(imagenId)
                      ?? throw new Exception("Imagen no encontrada.");

            await _storage.EliminarAsync(img.Url);
            _repoImgs.Remove(img);
            await _repoImgs.SaveAsync();
        }
    }

}
