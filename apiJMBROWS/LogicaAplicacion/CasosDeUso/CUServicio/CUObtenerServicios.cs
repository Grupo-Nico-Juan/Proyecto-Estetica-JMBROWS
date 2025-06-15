using LogicaAplicacion.Dtos.ServicioDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUServicio;
using LogicaNegocio.InterfacesRepositorio;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUServicio
{
    public class CUObtenerServicios : ICUObtenerServicios
    {
        private readonly IRepositorioServicios _repo;

        public CUObtenerServicios(IRepositorioServicios repo)
        {
            _repo = repo;
        }

        public IEnumerable<ServicioDTO> Ejecutar()
        {
            return _repo.GetAll().Select(s => new ServicioDTO
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