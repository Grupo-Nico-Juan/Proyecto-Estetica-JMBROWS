using LogicaAplicacion.InterfacesCasosDeUso.ICUSurcursal;
using LogicaNegocio.InterfacesRepositorio;
using System;

namespace LogicaAplicacion.CasosDeUso.CUSucursal
{
    public class CUEliminarSucursal : ICUEliminarSucursal
    {
        private readonly IRepositorioSucursales _repo;

        public CUEliminarSucursal(IRepositorioSucursales repo)
        {
            _repo = repo;
        }

        public void Ejecutar(int id)
        {
            var sucursal = _repo.GetById(id);
            if (sucursal == null)
                throw new Exception("Sucursal no encontrada");

            _repo.Remove(id);
        }
    }
}