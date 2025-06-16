using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUTurno;
using LogicaNegocio.Entidades;
using LogicaNegocio.Entidades.Enums;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorio;
using System.Collections.Generic;

namespace LogicaAplicacion.CasosDeUso.CUTurno
{
    public class CUAltaTurno : ICUAltaTurno
    {
        private readonly IRepositorioTurnos _repo;
        private readonly IRepositorioUsuarios _repoUsuarios;

        public CUAltaTurno(IRepositorioTurnos repo, IRepositorioUsuarios repoUsuarios)
        {
            _repo = repo;
            _repoUsuarios = repoUsuarios;
        }

        public void Ejecutar(AltaTurnoDTO dto)
        {
            var turno = new Turno
            {
                FechaHora = dto.FechaHora,
                EmpleadaId = dto.EmpleadaId,
                ClienteId = dto.ClienteId,
                Realizado = false,
                Detalles = new List<DetalleTurno>()
            };

            foreach (var det in dto.Detalles)
            {
                turno.Detalles.Add(new DetalleTurno
                {
                    ServicioId = det.ServicioId,
                });
            }

            turno.EsValido();

            var inicioNuevoTurno = turno.FechaHora;
            var finNuevoTurno = inicioNuevoTurno.AddMinutes(turno.DuracionTotal());

            // Validación de solapamiento con otros turnos
            var turnosDelDia = _repo.ObtenerTurnosDelDiaPorEmpleada(dto.EmpleadaId, dto.FechaHora.Date);
            foreach (var t in turnosDelDia)
            {
                if (t.Cancelado) continue;
                var inicioExistente = t.FechaHora;
                var finExistente = inicioExistente.AddMinutes(t.DuracionTotal());
                if (inicioNuevoTurno < finExistente && finNuevoTurno > inicioExistente)
                    throw new TurnoException("La empleada no está disponible en el horario seleccionado.");
            }

            // --- Validación de disponibilidad según periodos laborales ---
            var empleada = _repoUsuarios.GetEmpleadoById(dto.EmpleadaId);
            if (empleada == null)
                throw new TurnoException("La empleada no existe.");

            // Validar que el usuario es realmente un empleado
            if (!(empleada is Empleado))
                throw new TurnoException("El usuario seleccionado no es una empleada.");

            // 1. Validar que el turno esté dentro de algún horario habitual
            var horarios = empleada.PeriodosLaborales
                .Where(p => p.Tipo == TipoPeriodoLaboral.HorarioHabitual)
                .ToList();

            bool enHorario = horarios.Any(h =>
                h.DiaSemana == inicioNuevoTurno.DayOfWeek &&
                h.HoraInicio <= inicioNuevoTurno.TimeOfDay &&
                h.HoraFin >= finNuevoTurno.TimeOfDay);

            if (!enHorario)
                throw new TurnoException("El turno está fuera del horario laboral habitual de la empleada.");

            // 2. Validar que no se solape con ninguna licencia
            foreach (var periodo in empleada.PeriodosLaborales.Where(p => p.Tipo == TipoPeriodoLaboral.Licencia))
            {
                if (periodo.SeSuperpone(inicioNuevoTurno, finNuevoTurno))
                    throw new TurnoException("La empleada está de licencia o no disponible en el horario seleccionado.");
            }

            _repo.Add(turno);
        }
    }
}