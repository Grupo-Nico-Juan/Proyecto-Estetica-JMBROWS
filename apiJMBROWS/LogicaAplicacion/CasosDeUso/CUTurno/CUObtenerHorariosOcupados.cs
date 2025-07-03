using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUTurno;
using LogicaNegocio.Entidades;
using LogicaNegocio.Entidades.Enums;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUTurno
{
    public class CUObtenerHorariosOcupados : ICUObtenerHorariosOcupados
    {
        private readonly IRepositorioTurnos _repoTurno;
        private readonly IRepositorioUsuarios _repoEmpleado;
        private readonly IRepositorioServicios _repoServicio;
        private readonly IRepositorioExtrasServicio _repoExtras;

        public CUObtenerHorariosOcupados(IRepositorioTurnos repoTurno,
                                         IRepositorioUsuarios repoEmpleado,
                                         IRepositorioServicios repoServicio,
                                         IRepositorioExtrasServicio repoExtras)
        {
            _repoTurno = repoTurno;
            _repoEmpleado = repoEmpleado;
            _repoServicio = repoServicio;
            _repoExtras = repoExtras;
        }

        public List<HorarioOcupadoDTO> Ejecutar(HorariosDisponiblesFiltroDTO filtro)
        {
            var servicios = _repoServicio.ObtenerPorIds(filtro.ServicioIds);
            var extras = filtro.ExtraIds != null && filtro.ExtraIds.Any()
                ? _repoExtras.ObtenerPorIds(filtro.ExtraIds)
                : new List<ExtraServicio>();
            int duracionTotal = servicios.Sum(s => s.DuracionMinutos) + extras.Sum(e => e.DuracionMinutos);

            var habilidadesNecesarias = servicios
                .SelectMany(s => s.Habilidades)
                .Select(h => h.Id)
                .Distinct()
                .ToList();

            var empleadas = _repoEmpleado.GetEmpleados()
                .Where(e =>
                    e.SectoresAsignados.Any(s => s.SucursalId == filtro.SucursalId) &&
                    habilidadesNecesarias.All(h => e.Habilidades.Any(eh => eh.Id == h)))
                .ToList();

            var todosBloques = new HashSet<(DateTime inicio, DateTime fin)>();
            var disponibles = new HashSet<(DateTime inicio, DateTime fin)>();

            foreach (var empleada in empleadas)
            {
                var periodos = empleada.PeriodosLaborales
                    .Where(p =>
                        p.Tipo == TipoPeriodoLaboral.HorarioHabitual &&
                        p.DiaSemana == filtro.Fecha.DayOfWeek &&
                        p.HoraInicio.HasValue &&
                        p.HoraFin.HasValue)
                    .ToList();

                var turnos = _repoTurno.ObtenerTurnosDelDiaPorEmpleada(empleada.Id, filtro.Fecha);

                foreach (var p in periodos)
                {
                    var bloques = GenerarBloques(p.HoraInicio.Value, p.HoraFin.Value, filtro.Fecha, duracionTotal);

                    foreach (var b in bloques)
                    {
                        todosBloques.Add(b);
                        bool ocupado = turnos.Any(t => b.inicio < t.FechaHora.AddMinutes(t.DuracionTotal()) && b.fin > t.FechaHora);
                        bool enLicencia = empleada.PeriodosLaborales.Any(l =>
                            l.Tipo == TipoPeriodoLaboral.Licencia &&
                            l.Desde <= b.inicio &&
                            l.Hasta >= b.fin);
                        if (!ocupado && !enLicencia)
                        {
                            disponibles.Add(b);
                        }
                    }
                }
            }

            var ocupados = todosBloques.Except(disponibles)
                .Select(b => new HorarioOcupadoDTO
                {
                    FechaHoraInicio = b.inicio,
                    FechaHoraFin = b.fin
                })
                .ToList();

            return ocupados;
        }

        private List<(DateTime inicio, DateTime fin)> GenerarBloques(TimeSpan desde, TimeSpan hasta, DateTime fecha, int duracionMinutos)
        {
            var bloques = new List<(DateTime, DateTime)>();
            var actual = fecha.Date + desde;
            var fin = fecha.Date + hasta;

            while (actual.AddMinutes(duracionMinutos) <= fin)
            {
                bloques.Add((actual, actual.AddMinutes(duracionMinutos)));
                actual = actual.AddMinutes(15);
            }

            return bloques;
        }
    }
}
