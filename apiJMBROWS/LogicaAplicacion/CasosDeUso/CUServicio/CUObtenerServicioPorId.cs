using LogicaAplicacion.Dtos.ServicioDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUServicio;
using LogicaNegocio.InterfacesRepositorio;
using System;

namespace LogicaAplicacion.CasosDeUso.CUServicio
{
    public class CUObtenerServicioPorId : ICUObtenerServicioPorId
    {
        private readonly IRepositorioServicios _repo;

        public CUObtenerServicioPorId(IRepositorioServicios repo)
        {
            _repo = repo;
        }

        public ServicioDTO Ejecutar(int id)
        {
            var s = _repo.GetById(id);
            if (s == null)
                throw new Exception("Servicio no encontrado");

            return new ServicioDTO
            {
                Id = s.Id,
                Nombre = s.Nombre,
                Descripcion = s.Descripcion,
                DuracionMinutos = s.DuracionMinutos,
                Precio = s.Precio
            };
        }
    }
}