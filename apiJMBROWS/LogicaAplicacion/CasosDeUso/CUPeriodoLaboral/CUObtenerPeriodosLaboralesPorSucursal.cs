using LogicaAplicacion.Dtos.PeriodoLaboralDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUPeriodoLaboral;
using LogicaNegocio.Entidades.Enums;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUPeriodoLaboral
{
    public class CUObtenerPeriodosLaboralesPorSucursal : ICUObtenerPeriodosLaboralesPorSucursal
    {
        private readonly IRepositorioUsuarios _repoUsuarios;

        public CUObtenerPeriodosLaboralesPorSucursal(IRepositorioUsuarios repoUsuarios)
        {
            _repoUsuarios = repoUsuarios;
        }

        public Dictionary<DayOfWeek, List<PeriodoLaboralDTO>> Ejecutar(int sucursalId)
        {
            var empleados = _repoUsuarios.GetEmpleados()
                .Where(e => (e.SucursalId != null && e.SucursalId == sucursalId) ||
                             e.SectoresAsignados.Any(s => s.SucursalId == sucursalId))
                .ToList();

            var periodos = empleados
                .SelectMany(e => e.PeriodosLaborales
                    .Where(p => p.Tipo == TipoPeriodoLaboral.HorarioHabitual)
                    .Select(p => new PeriodoLaboralDTO
                    {
                        Id = p.Id,
                        EmpleadaId = e.Id,
                        Tipo = p.Tipo,
                        DiaSemana = p.DiaSemana,
                        HoraInicio = p.HoraInicio,
                        HoraFin = p.HoraFin,
                        Desde = p.Desde,
                        Hasta = p.Hasta,
                        Motivo = p.Motivo
                    }))
                .ToList();

            return periodos
                .Where(p => p.DiaSemana.HasValue)
                .GroupBy(p => p.DiaSemana!.Value)
                .ToDictionary(g => g.Key, g => g.ToList());
        }
    }
}
