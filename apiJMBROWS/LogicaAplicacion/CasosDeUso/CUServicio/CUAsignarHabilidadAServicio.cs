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
    public class CUAsignarHabilidadAServicio : ICUAsignarHabilidadAServicio
    {
        private readonly IRepositorioServicios _repo;

        public CUAsignarHabilidadAServicio(IRepositorioServicios repo)
        {
            _repo = repo;
        }

        public void Ejecutar(HabilidadAServicioDTO dto)
        {
            _repo.AsignarHabilidad(dto.ServicioId, dto.HabilidadId);
        }
    }
}
