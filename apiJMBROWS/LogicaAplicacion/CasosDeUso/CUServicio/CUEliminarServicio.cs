using LogicaAplicacion.InterfacesCasosDeUso.ICUServicio;
using LogicaNegocio.InterfacesRepositorio;
using System;

namespace LogicaAplicacion.CasosDeUso.CUServicio
{
    public class CUEliminarServicio : ICUEliminarServicio
    {
        private readonly IRepositorioServicios _repo;

        public CUEliminarServicio(IRepositorioServicios repo)
        {
            _repo = repo;
        }

        public void Ejecutar(int id)
        {
            var servicio = _repo.GetById(id);
            if (servicio == null)
                throw new Exception("Servicio no encontrado");

            _repo.Remove(id);
        }
    }
}