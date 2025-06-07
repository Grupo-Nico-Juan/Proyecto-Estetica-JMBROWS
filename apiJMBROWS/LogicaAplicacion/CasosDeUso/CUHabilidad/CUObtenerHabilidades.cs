using LogicaAplicacion.Dtos.HabilidadDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUHabilidad;
using LogicaNegocio.InterfacesRepositorio;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUHabilidad
{
    public class CUObtenerHabilidades : ICUObtenerHabilidades
    {
        private readonly IRepositorioHabilidades _repo;

        public CUObtenerHabilidades(IRepositorioHabilidades repo)
        {
            _repo = repo;
        }

        public IEnumerable<HabilidadDTO> Ejecutar()
        {
            return _repo.GetAll().Select(h => new HabilidadDTO
            {
                Id = h.Id,
                Nombre = h.Nombre,
                Descripcion = h.Descripcion
            });
        }
    }
}