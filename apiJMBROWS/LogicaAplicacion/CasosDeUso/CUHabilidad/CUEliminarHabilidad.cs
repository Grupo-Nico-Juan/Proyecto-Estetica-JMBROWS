using LogicaAplicacion.InterfacesCasosDeUso.ICUHabilidad;
using LogicaNegocio.InterfacesRepositorio;
using System;

namespace LogicaAplicacion.CasosDeUso.CUHabilidad
{
    public class CUEliminarHabilidad : ICUEliminarHabilidad
    {
        private readonly IRepositorioHabilidades _repo;

        public CUEliminarHabilidad(IRepositorioHabilidades repo)
        {
            _repo = repo;
        }

        public void Ejecutar(int id)
        {
            var habilidad = _repo.GetById(id);
            if (habilidad == null)
                throw new Exception("Habilidad no encontrada");

            _repo.Remove(id);
        }
    }
}