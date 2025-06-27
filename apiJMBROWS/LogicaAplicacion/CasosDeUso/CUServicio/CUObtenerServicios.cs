using LogicaAplicacion.Dtos.ServicioDTO;
using LogicaAplicacion.Dtos.ExtraServicioDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUServicio;
using LogicaNegocio.InterfacesRepositorio;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUServicio
{
    public class CUObtenerServicios : ICUObtenerServicios
    {
        private readonly IRepositorioServicios _repo;
        private readonly IRepositorioExtrasServicio _repoExtras;

        public CUObtenerServicios(IRepositorioServicios repo, IRepositorioExtrasServicio repoExtras)
        {
            _repo = repo;
            _repoExtras = repoExtras;
        }

        public IEnumerable<ServicioDTO> Ejecutar()
        {
            return _repo.GetAll().Select(s => new ServicioDTO
            {
                Id = s.Id,
                Nombre = s.Nombre,
                Descripcion = s.Descripcion,
                DuracionMinutos = s.DuracionMinutos,
                Precio = s.Precio,
                Extras = _repoExtras.ObtenerPorServicio(s.Id)
                    .Select(e => new ExtraServicioDTO
                    {
                        Id = e.Id,
                        Nombre = e.Nombre,
                        DuracionMinutos = e.DuracionMinutos,
                        Precio = e.Precio,
                        ServicioId = e.ServicioId
                    }).ToList()
            });
        }
    }
}