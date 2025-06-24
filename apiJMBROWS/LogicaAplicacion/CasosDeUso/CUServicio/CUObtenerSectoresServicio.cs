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
    public class CUObtenerSectoresServicio : ICUObtenerSectoresServicio
    {
        private readonly IRepositorioServicios _repo;

        public CUObtenerSectoresServicio(IRepositorioServicios repo)
        {
            _repo = repo;
        }

        public List<ServicioSectorDTO> Ejecutar(int servicioId)
        {
            var sectores = _repo.ObtenerSectoresDeServicio (servicioId);

            return sectores.Select(s => new ServicioSectorDTO
            {
                Id = s.Id,
                Nombre = s.Nombre,
                SucursalId = s.SucursalId
            }).ToList();
        }
    }

}
