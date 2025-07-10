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
    public class CUObtenerHorariosPorEmpleada : ICUObtenerHorariosPorEmpleada
    {
        private readonly IRepositorioTurnos _repoTurno;
        private readonly IRepositorioUsuarios _repoEmpleado;
        private readonly IRepositorioServicios _repoServicio;
        private readonly IRepositorioExtrasServicio _repoExtras;

        public CUObtenerHorariosPorEmpleada(IRepositorioTurnos repoTurno,
                                             IRepositorioUsuarios repoEmpleado,
                                             IRepositorioServicios repoServicio,
                                             IRepositorioExtrasServicio repoExtras)
        {
            _repoTurno = repoTurno;
            _repoEmpleado = repoEmpleado;
            _repoServicio = repoServicio;
            _repoExtras = repoExtras;
        }

        public List<HorarioDisponibleDTO> Ejecutar(HorariosPorEmpleadaFiltroDTO filtro)
        {
            var ahora = DateTimeOffset.UtcNow;
            var empleada = _repoEmpleado.GetEmpleadoById(filtro.EmpleadaId);
            if (empleada == null)
                return new List<HorarioDisponibleDTO>();

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

            bool empleadaValida = empleada.SectoresAsignados.Any(s => s.SucursalId == filtro.SucursalId) &&
                                   habilidadesNecesarias.All(h => empleada.Habilidades.Any(eh => eh.Id == h));
            if (!empleadaValida)
                return new List<HorarioDisponibleDTO>();

            var periodos = empleada.PeriodosLaborales
                .Where(p => p.Tipo == TipoPeriodoLaboral.HorarioHabitual &&
                            p.DiaSemana == filtro.Fecha.DayOfWeek &&
                            p.HoraInicio.HasValue &&
                            p.HoraFin.HasValue)
                .ToList();

            var turnos = _repoTurno.ObtenerTurnosDelDiaPorEmpleada(empleada.Id, filtro.Fecha.UtcDateTime);

            var horarios = new List<HorarioDisponibleDTO>();

            foreach (var p in periodos)
            {
                var bloques = GenerarBloques(p.HoraInicio.Value, p.HoraFin.Value, filtro.Fecha, duracionTotal);

                foreach (var bloque in bloques)
                {
                    bool ocupado = turnos.Any(t =>
                        bloque.inicio < t.FechaHora.AddMinutes(t.DuracionTotal()) &&
                        bloque.fin > t.FechaHora);

                    bool enLicencia = empleada.PeriodosLaborales.Any(l =>
                        l.Tipo == TipoPeriodoLaboral.Licencia &&
                        l.Desde <= bloque.inicio &&
                        l.Hasta >= bloque.fin);

                    if (!ocupado && !enLicencia)
                    {
                        horarios.Add(new HorarioDisponibleDTO
                        {
                            FechaHoraInicio = bloque.inicio,
                            FechaHoraFin = bloque.fin,
                            EmpleadasDisponibles = new List<EmpleadoTurnoDTO>
                            {
                                new EmpleadoTurnoDTO
                                {
                                    Id = empleada.Id,
                                    Nombre = empleada.Nombre,
                                    Apellido = empleada.Apellido,
                                    Color = empleada.Color,
                                    Cargo = empleada.Cargo
                                }
                            }
                        });
                    }
                }
            }

            return horarios;
        }

        private List<(DateTimeOffset inicio, DateTimeOffset fin)> GenerarBloques(TimeSpan desde, TimeSpan hasta, DateTimeOffset fecha, int duracionMinutos)
        {
            var bloques = new List<(DateTimeOffset, DateTimeOffset)>();
            var actual = fecha.Date + desde;
            var fin = fecha.Date + hasta;

            while (actual.AddMinutes(duracionMinutos) <= fin)
            {
                bloques.Add((actual, actual.AddMinutes(duracionMinutos)));
                actual = actual.AddMinutes(duracionMinutos);
            }

            return bloques;
        }
    }
}
