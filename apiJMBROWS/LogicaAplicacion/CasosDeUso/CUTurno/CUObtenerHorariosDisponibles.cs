using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUTurno;
using LogicaNegocio.Entidades.Enums;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUTurno
{
    public class CUObtenerHorariosDisponibles : ICUObtenerHorariosDisponibles
    {
        private readonly IRepositorioTurnos _repoTurno;
        private readonly IRepositorioUsuarios _repoEmpleado;
        private readonly IRepositorioServicios _repoServicio;
        private readonly IRepositorioExtrasServicio _repoExtras;

        public CUObtenerHorariosDisponibles(IRepositorioTurnos repoTurno,
                                       IRepositorioUsuarios repoEmpleado,
                                       IRepositorioServicios repoServicio,
                                       IRepositorioExtrasServicio repoExtras)
        {
            _repoTurno = repoTurno;
            _repoEmpleado = repoEmpleado;
            _repoServicio = repoServicio;
            _repoExtras = repoExtras;
        }

        public List<HorarioDisponibleDTO> Ejecutar(HorariosDisponiblesFiltroDTO filtro)
        {
            // 1. Obtener los servicios y calcular duración total
            var servicios = _repoServicio.ObtenerPorIds(filtro.ServicioIds);
            var extras = filtro.ExtraIds != null && filtro.ExtraIds.Any()
                ? _repoExtras.ObtenerPorIds(filtro.ExtraIds)
                : new List<ExtraServicio>();

            int duracionTotal = servicios.Sum(s => s.DuracionMinutos) + extras.Sum(e => e.DuracionMinutos);

            // 2. Determinar habilidades necesarias
            var habilidadesNecesarias = servicios
                .SelectMany(s => s.Habilidades)
                .Select(h => h.Id)
                .Distinct()
                .ToList();

            // 3. Obtener empleadas de la sucursal con todas las habilidades necesarias
            var empleadas = _repoEmpleado.GetEmpleados()
                .Where(e =>
                    e.SectoresAsignados.Any(s => s.SucursalId == filtro.SucursalId) &&
                    habilidadesNecesarias.All(h => e.Habilidades.Any(eh => eh.Id == h)))
                .ToList();

            var horarios = new List<HorarioDisponibleDTO>();

            foreach (var empleada in empleadas)
            {
                // 4. Filtrar periodos laborales tipo HorarioHabitual para el día seleccionado
                var periodos = empleada.PeriodosLaborales
                    .Where(p =>
                        p.Tipo == TipoPeriodoLaboral.HorarioHabitual &&
                        p.DiaSemana == filtro.Fecha.DayOfWeek &&
                        p.HoraInicio.HasValue &&
                        p.HoraFin.HasValue)
                    .ToList();

                // 5. Obtener turnos tomados de la empleada en esa fecha
                var turnos = _repoTurno.ObtenerTurnosDelDiaPorEmpleada(empleada.Id, filtro.Fecha);

                foreach (var p in periodos)
                {
                    var bloques = GenerarBloques(p.HoraInicio.Value, p.HoraFin.Value, filtro.Fecha, duracionTotal);

                    foreach (var bloque in bloques)
                    {
                        // 6. Verificar disponibilidad
                        bool ocupado = turnos.Any(t =>
                            bloque.inicio < t.FechaHora.AddMinutes(t.DuracionTotal()) &&
                            bloque.fin > t.FechaHora);

                        bool enLicencia = empleada.PeriodosLaborales.Any(l =>
                            l.Tipo == TipoPeriodoLaboral.Licencia &&
                            l.Desde <= bloque.inicio &&
                            l.Hasta >= bloque.fin);

                        if (!ocupado && !enLicencia)
                        {
                            var existente = horarios.FirstOrDefault(h =>
                                h.FechaHoraInicio == bloque.inicio && h.FechaHoraFin == bloque.fin);

                            if (existente == null)
                            {
                                existente = new HorarioDisponibleDTO
                                {
                                    FechaHoraInicio = bloque.inicio,
                                    FechaHoraFin = bloque.fin,
                                    EmpleadasDisponibles = new List<EmpleadoTurnoDTO>()
                                };
                                horarios.Add(existente);
                            }

                            existente.EmpleadasDisponibles.Add(new EmpleadoTurnoDTO
                            {
                                Id = empleada.Id,
                                Nombre = empleada.Nombre,
                                Apellido = empleada.Apellido,
                                Color = empleada.Color,
                                Cargo = empleada.Cargo
                            });
                        }
                    }
                }
            }

            return horarios.OrderBy(h => h.FechaHoraInicio).ToList(); ;
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