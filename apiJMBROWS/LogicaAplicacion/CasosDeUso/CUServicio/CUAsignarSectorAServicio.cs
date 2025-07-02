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
    public class CUAsignarSectorAServicio : ICUAsignarSectorAServicio
    {
        private readonly IRepositorioServicios _repo;

        public CUAsignarSectorAServicio(IRepositorioServicios repo)
        {
            _repo = repo;
        }

        public void Ejecutar(SectorAServicioDTO dto)
        {
            _repo.AsignarSector(dto.ServicioId, dto.SectorId);
        }
    }
}
