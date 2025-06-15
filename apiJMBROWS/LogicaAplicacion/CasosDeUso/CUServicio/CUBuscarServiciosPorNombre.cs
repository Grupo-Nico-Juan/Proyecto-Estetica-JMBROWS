using LogicaAplicacion.Dtos.ServicioDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUServicio;
using LogicaNegocio.InterfacesRepositorio;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUServicio
{
    public class CUBuscarServiciosPorNombre : ICUBuscarServiciosPorNombre
    {
        private readonly IRepositorioServicios _repo;

        public CUBuscarServiciosPorNombre(IRepositorioServicios repo)
        {
            _repo = repo;
        }

        public IEnumerable<ServicioDTO> Ejecutar(string texto)
        {
            return _repo.BuscarPorNombre(texto).Select(s => new ServicioDTO
            {
                Id = s.Id,
                Nombre = s.Nombre,
                Descripcion = s.Descripcion,
                DuracionMinutos = s.DuracionMinutos,
                Precio = s.Precio
            });
        }
    }
}