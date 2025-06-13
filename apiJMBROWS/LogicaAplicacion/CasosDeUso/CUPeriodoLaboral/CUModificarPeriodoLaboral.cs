using LogicaAplicacion.Dtos.PeriodoLaboralDTO;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CUPeriodoLaboral
{
    public class CUModificarPeriodoLaboral : ICUModificarPeriodoLaboral
    {
        private readonly IRepositorioPeriodoLaboral _repo;

        public CUModificarPeriodoLaboral(IRepositorioPeriodoLaboral repo)
        {
            _repo = repo;
        }

        public void Ejecutar(PeriodoLaboralDTO dto)
        {
            var periodo = _repo.ObtenerPorId(dto.Id);
            if (periodo == null)
                throw new PeriodoLaboralException("Periodo laboral no encontrado.");

            periodo.Desde = dto.Desde;
            periodo.Hasta = dto.Hasta;
            periodo.Motivo = dto.Motivo;
            periodo.EsLicencia = dto.EsLicencia;

            periodo.EsValido();
            _repo.Modificar(periodo);
        }
    }
}
