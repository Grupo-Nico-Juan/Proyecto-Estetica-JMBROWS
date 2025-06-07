using LogicaAplicacion.Dtos.HabilidadDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUHabilidad;
using LogicaNegocio.InterfacesRepositorio;
using System;

namespace LogicaAplicacion.CasosDeUso.CUHabilidad
{
    public class CUObtenerHabilidadPorId : ICUObtenerHabilidadPorId
    {
        private readonly IRepositorioHabilidades _repo;

        public CUObtenerHabilidadPorId(IRepositorioHabilidades repo)
        {
            _repo = repo;
        }

        public ActualizarHabilidadDTO Ejecutar(int id)
        {
            var habilidad = _repo.GetById(id);
            if (habilidad == null)
                throw new Exception("Habilidad no encontrada");

            return new ActualizarHabilidadDTO
            {
                Id = habilidad.Id,
                Nombre = habilidad.Nombre,
                Descripcion = habilidad.Descripcion
            };
        }
    }
}