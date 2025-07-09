using LogicaAplicacion.Dtos.EmpleadoDTO.EmpleadoDispibleDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaNegocio.Entidades.Enums;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CUEmpleado
{
    public class CUObtenerEmpleadasDisponibles : ICUObtenerEmpleadasDisponibles
    {
        private readonly IRepositorioUsuarios _repoUsuarios;
        private readonly IRepositorioServicios _repoServicios;
        private readonly IRepositorioTurnos _repoTurnos;

        public CUObtenerEmpleadasDisponibles(IRepositorioUsuarios repoUsuarios,
                                             IRepositorioServicios repoServicios,
                                             IRepositorioTurnos repoTurnos)
        {
            _repoUsuarios = repoUsuarios;
            _repoServicios = repoServicios;
            _repoTurnos = repoTurnos;
        }

        public List<EmpleadaDisponibleDTO> Ejecutar(ConsultaEmpleadasDisponiblesDTO dto)
        {
            var servicios = _repoServicios.ObtenerPorIds(dto.ServiciosSeleccionados);

            var habilidadesRequeridas = servicios
                .SelectMany(s => s.Habilidades.Select(h => h.Id))
                .Distinct()
                .ToList();

            var empleadas = _repoUsuarios.GetEmpleados()
                .Where(e => habilidadesRequeridas.All(hr => e.Habilidades.Any(h => h.Id == hr)))
                .ToList();

            var fecha = dto.FechaHoraInicio.Date;

            var disponibles = new List<EmpleadaDisponibleDTO>();

            foreach (var emp in empleadas)
            {
                var turnosDelDia = _repoTurnos.ObtenerTurnosDelDiaPorEmpleada(emp.Id, fecha);
                bool estaLibre = !turnosDelDia.Any(t =>
                    dto.FechaHoraInicio < t.FechaHora.AddMinutes(t.Detalles.Sum(d => d.Servicio.DuracionMinutos)) &&
                    dto.FechaHoraInicio.AddMinutes(servicios.Sum(s => s.DuracionMinutos)) > t.FechaHora);

                bool tieneHorarioHabitual = emp.PeriodosLaborales.Any(p =>
                    p.Tipo == TipoPeriodoLaboral.HorarioHabitual &&
                    p.DiaSemana == dto.FechaHoraInicio.DayOfWeek &&
                    p.HoraInicio <= dto.FechaHoraInicio.TimeOfDay &&
                    p.HoraFin >= dto.FechaHoraInicio.TimeOfDay.Add(TimeSpan.FromMinutes(servicios.Sum(s => s.DuracionMinutos)))
                );

                bool noEstaDeLicencia = !emp.PeriodosLaborales.Any(p =>
                    p.Tipo == TipoPeriodoLaboral.Licencia &&
                    p.Desde <= dto.FechaHoraInicio &&
                    p.Hasta >= dto.FechaHoraInicio
                );

                if (estaLibre && tieneHorarioHabitual && noEstaDeLicencia)
                {
                    disponibles.Add(new EmpleadaDisponibleDTO
                    {
                        Id = emp.Id,
                        NombreCompleto = $"{emp.Nombre} {emp.Apellido}",
                        ServiciosQuePuedeRealizar = servicios.Select(s => s.Id).ToList(),
                        Color = emp.Color
                    });
                }
            }

            return disponibles;
        }
    }

}
