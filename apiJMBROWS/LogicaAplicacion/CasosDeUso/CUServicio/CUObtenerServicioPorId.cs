using LogicaAplicacion.Dtos.ServicioDTO;
using LogicaAplicacion.Dtos.ExtraServicioDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUServicio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUServicio
{
    public class CUObtenerServicioPorId : ICUObtenerServicioPorId
    {
        private readonly IRepositorioServicios _repo;
        private readonly IRepositorioExtrasServicio _repoExtras;

        public CUObtenerServicioPorId(IRepositorioServicios repo, IRepositorioExtrasServicio repoExtras)
        {
            _repo = repo;
            _repoExtras = repoExtras;
        }

        public ServicioDTO Ejecutar(int id)
        {
            var s = _repo.GetById(id);
            if (s == null)
                throw new Exception("Servicio no encontrado");

            var extras = _repoExtras.ObtenerPorServicio(s.Id)
                .Select(e => new ServiciosExtrasDTO
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    DuracionMinutos = e.DuracionMinutos,
                    Precio = e.Precio,
                    ServicioId = e.ServicioId
                }).ToList();

            return new ServicioDTO
            {
                Id = s.Id,
                Nombre = s.Nombre,
                Descripcion = s.Descripcion,
                DuracionMinutos = s.DuracionMinutos,
                Precio = s.Precio,
                Imagenes = s.Imagenes.Select(i => i.Url).ToList(),
                Extras = extras
            };
        }
    }
}