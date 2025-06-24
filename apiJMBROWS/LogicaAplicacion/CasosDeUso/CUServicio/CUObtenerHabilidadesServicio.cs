using LogicaAplicacion.Dtos.HabilidadDTO;
using LogicaAplicacion.Dtos.ServicioDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUServicio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CUServicio
{
    public class CUObtenerHabilidadesServicio : ICUObtenerHabilidadesServicio
    {
        private readonly IRepositorioServicios _repo;

        public CUObtenerHabilidadesServicio(IRepositorioServicios repo)
        {
            _repo = repo;
        }

        public List<ServicioHabilidadDTO> Ejecutar(int servicioId)
        {
            var habilidades = _repo.ObtenerHabilidadesDeServicio(servicioId);

            return habilidades.Select(h => new ServicioHabilidadDTO
            {
                Id = h.Id,
                Nombre = h.Nombre
            }).ToList();
        }
    }
}
