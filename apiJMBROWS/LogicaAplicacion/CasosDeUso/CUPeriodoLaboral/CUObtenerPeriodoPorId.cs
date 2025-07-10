using LogicaAplicacion.Dtos.PeriodoLaboralDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUPeriodoLaboral;
using LogicaNegocio.Entidades.Enums;
using LogicaNegocio.InterfacesRepositorio;
using System;

namespace LogicaAplicacion.CasosDeUso.CUPeriodoLaboral
{
    public class CUObtenerPeriodoPorId : ICUObtenerPeriodoPorId
    {
        private readonly IRepositorioPeriodoLaboral _repo;

        public CUObtenerPeriodoPorId(IRepositorioPeriodoLaboral repo)
        {
            _repo = repo;
        }

        public PeriodoLaboralDTO Ejecutar(int id)
        {
            var periodo = _repo.ObtenerPorId(id);

            if (periodo == null)
                throw new Exception("No se encontró el periodo laboral con el ID especificado.");

            return new PeriodoLaboralDTO
            {
                Id = periodo.Id,
                EmpleadaId = periodo.EmpleadaId,
                Tipo = periodo.Tipo,
                DiaSemana = periodo.DiaSemana,
                HoraInicio = periodo.HoraInicio,
                HoraFin = periodo.HoraFin,
                Desde = periodo.Desde,
                Hasta = periodo.Hasta,
                Motivo = periodo.Motivo
            };
        }
    }
}
