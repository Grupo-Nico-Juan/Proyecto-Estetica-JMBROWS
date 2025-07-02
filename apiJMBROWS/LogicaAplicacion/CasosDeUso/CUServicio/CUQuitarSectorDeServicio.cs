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
    public class CUQuitarSectorDeServicio : ICUQuitarSectorDeServicio
    {
        private readonly IRepositorioServicios _repo;

        public CUQuitarSectorDeServicio(IRepositorioServicios repo)
        {
            _repo = repo;
        }

        public void Ejecutar(SectorAServicioDTO dto)
        {
            _repo.QuitarSector(dto.ServicioId, dto.SectorId);
        }
    }
}
