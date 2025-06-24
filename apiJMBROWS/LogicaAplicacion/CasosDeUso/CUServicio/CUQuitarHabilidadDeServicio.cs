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
    public class CUQuitarHabilidadDeServicio : ICUQuitarHabilidadDeServicio
    {
        private readonly IRepositorioServicios _repo;

        public CUQuitarHabilidadDeServicio(IRepositorioServicios repo)
        {
            _repo = repo;
        }

        public void Ejecutar(HabilidadAServicioDTO dto)
        {
            _repo.QuitarHabilidad(dto.ServicioId, dto.HabilidadId);
        }
    }
}
