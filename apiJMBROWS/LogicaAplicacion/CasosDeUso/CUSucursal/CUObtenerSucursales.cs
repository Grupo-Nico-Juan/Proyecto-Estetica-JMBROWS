using LogicaAplicacion.Dtos.SucursalDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUSurcursal;
using LogicaNegocio.InterfacesRepositorio;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUSucursal
{
    public class CUObtenerSucursales : ICUObtenerSucursales
    {
        private readonly IRepositorioSucursales _repo;

        public CUObtenerSucursales(IRepositorioSucursales repo)
        {
            _repo = repo;
        }

        public IEnumerable<SucursalDTO> Ejecutar()
        {
            return _repo.GetAll().Select(s => new SucursalDTO
            {
                Id = s.Id,
                Nombre = s.Nombre,
                Direccion = s.Direccion,
                Telefono = s.Telefono
            });
        }
    }
}