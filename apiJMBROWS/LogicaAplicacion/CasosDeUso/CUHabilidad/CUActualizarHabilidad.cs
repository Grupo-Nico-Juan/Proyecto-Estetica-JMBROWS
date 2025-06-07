using LogicaAplicacion.Dtos.HabilidadDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUHabilidad;
using LogicaNegocio.InterfacesRepositorio;
using System;

namespace LogicaAplicacion.CasosDeUso.CUHabilidad
{
    public class CUActualizarHabilidad : ICUActualizarHabilidad
    {
        private readonly IRepositorioHabilidades _repo;

        public CUActualizarHabilidad(IRepositorioHabilidades repo)
        {
            _repo = repo;
        }

        public void Ejecutar(ActualizarHabilidadDTO dto)
        {
            var habilidad = _repo.GetById(dto.Id);
            if (habilidad == null)
                throw new Exception("Habilidad no encontrada");

            habilidad.Nombre = dto.Nombre;
            habilidad.Descripcion = dto.Descripcion;
            habilidad.EsValido();

            _repo.Update(habilidad.Id, habilidad);
        }
    }
}