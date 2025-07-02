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
            if (!dto.Id.HasValue)
                throw new ArgumentException("El ID del periodo laboral no puede ser nulo.");

            var periodo = _repo.ObtenerPorId(dto.Id.Value);
            if (periodo == null)
                throw new PeriodoLaboralException("Periodo laboral no encontrado.");

            periodo.Tipo = dto.Tipo;
            periodo.DiaSemana = dto.DiaSemana;
            periodo.HoraInicio = dto.HoraInicio;
            periodo.HoraFin = dto.HoraFin;
            periodo.Desde = dto.Desde;
            periodo.Hasta = dto.Hasta;
            periodo.Motivo = dto.Motivo;

            periodo.EsValido();
            _repo.Modificar(periodo);
        }
    }
}
