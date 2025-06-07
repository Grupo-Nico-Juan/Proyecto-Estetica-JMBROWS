using LogicaAplicacion.Dtos.ServicioDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUServicio;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;

namespace LogicaAplicacion.CasosDeUso.CUServicio
{
    public class CUActualizarServicio : ICUActualizarServicio
    {
        private readonly IRepositorioServicios _repo;

        public CUActualizarServicio(IRepositorioServicios repo)
        {
            _repo = repo;
        }

        public void Ejecutar(ActualizarServicioDTO dto)
        {
            var servicio = _repo.GetById(dto.Id);
            if (servicio == null)
                throw new Exception("Servicio no encontrado");

            servicio.Nombre = dto.Nombre;
            servicio.Descripcion = dto.Descripcion;
            servicio.DuracionMinutos = dto.DuracionMinutos;
            servicio.Precio = dto.Precio;
            servicio.EsValido();

            _repo.Update(servicio.Id, servicio);
        }
    }
}