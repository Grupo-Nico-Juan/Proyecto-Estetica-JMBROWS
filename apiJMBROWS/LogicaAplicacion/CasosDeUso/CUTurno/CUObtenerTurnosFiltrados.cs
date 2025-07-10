using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUTurno;
using LogicaNegocio.InterfacesRepositorio;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUTurno
{
    public class CUObtenerTurnosFiltrados : ICUObtenerTurnosFiltrados
    {
        private readonly IRepositorioTurnos _repo;

        public CUObtenerTurnosFiltrados(IRepositorioTurnos repo)
        {
            _repo = repo;
        }

        public IEnumerable<TurnoCalendarioDTO> Ejecutar(TurnoFiltroDTO filtro)
        {
            var turnos = _repo.GetAll();

            // Si no se especifica rango de fechas, tomar el día actual
            if (!filtro.FechaInicio.HasValue && !filtro.FechaFin.HasValue)
            {
                var hoy = DateTime.Today;
                filtro.FechaInicio = hoy;
                filtro.FechaFin = hoy.AddDays(1).AddTicks(-1);
            }

            turnos = turnos
                .Where(t =>
                    (!filtro.EmpleadaId.HasValue || t.EmpleadaId == filtro.EmpleadaId.Value) &&
                    (!filtro.Estado.HasValue || t.Estado == filtro.Estado.Value) &&
                    (!filtro.FechaInicio.HasValue || t.FechaHora >= filtro.FechaInicio.Value) &&
                    (!filtro.FechaFin.HasValue || t.FechaHora <= filtro.FechaFin.Value)
                );

            return turnos.Select(t => new TurnoCalendarioDTO
            {
                Id = t.Id,
                SucursalId = t.SucursalId,
                SucursalNombre = t.Sucursal.Nombre,

                FechaHoraInicio = t.FechaHora,
                FechaHoraFin = t.FechaHora.AddMinutes(t.DuracionTotal()),

                EmpleadaNombre = $"{t.Empleada.Nombre} {t.Empleada.Apellido}",
                EmpleadaColor = t.Empleada.Color,
                EmpleadaId = t.EmpleadaId,

                ClienteNombre = t.Cliente.Nombre,
                ClienteApellido = t.Cliente.Apellido,
                ClienteTelefono = t.Cliente.Telefono,

                Servicios = t.Detalles
                    .Select(d => d.Servicio.Nombre)
                    .ToList(),

                Extras = t.Detalles
                    .SelectMany(d => d.Servicio.Extras)
                    .Select(e => e.Servicio.Nombre)
                    .Distinct()
                    .ToList(),

                Estado = t.Estado
            });
        }
    }
}