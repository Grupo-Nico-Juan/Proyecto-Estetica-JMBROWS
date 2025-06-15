using LogicaAplicacion.Dtos.SucursalDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUSurcursal;
using LogicaNegocio.InterfacesRepositorio;
using System;

namespace LogicaAplicacion.CasosDeUso.CUSucursal
{
    public class CUObtenerSucursalPorId : ICUObtenerSucursalPorId
    {
        private readonly IRepositorioSucursales _repo;

        public CUObtenerSucursalPorId(IRepositorioSucursales repo)
        {
            _repo = repo;
        }

        public SucursalDTO Ejecutar(int id)
        {
            var sucursal = _repo.GetById(id);
            if (sucursal == null)
                throw new Exception("Sucursal no encontrada");

            return new SucursalDTO
            {
                Id = sucursal.Id,
                Nombre = sucursal.Nombre,
                Direccion = sucursal.Direccion,
                Telefono = sucursal.Telefono
            };
        }
    }
}