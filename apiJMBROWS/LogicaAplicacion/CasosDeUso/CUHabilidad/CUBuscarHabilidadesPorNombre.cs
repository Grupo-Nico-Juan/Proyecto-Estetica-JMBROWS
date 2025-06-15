using LogicaAplicacion.Dtos.HabilidadDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUHabilidad;
using LogicaNegocio.InterfacesRepositorio;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUHabilidad
{
    public class CUBuscarHabilidadesPorNombre : ICUBuscarHabilidadesPorNombre
    {
        private readonly IRepositorioHabilidades _repo;

        public CUBuscarHabilidadesPorNombre(IRepositorioHabilidades repo)
        {
            _repo = repo;
        }

        public IEnumerable<ActualizarHabilidadDTO> Ejecutar(string texto)
        {
            return _repo.BuscarPorNombre(texto).Select(h => new ActualizarHabilidadDTO
            {
                Id = h.Id,
                Nombre = h.Nombre,
                Descripcion = h.Descripcion
            });
        }
    }
}