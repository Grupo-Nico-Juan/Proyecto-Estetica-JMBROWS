using LogicaAplicacion.Dtos.ReportesDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUReportes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportesController : ControllerBase
    {
        private readonly ICUIngresosSucursalSector _ingresosSucursalSector;
        private readonly ICUEstadoTurnos _estadoTurnos;
        private readonly ICUTurnosPorServicio _turnosPorServicio;
        private readonly ICUHorarioMayorTurnos _horarioMayorTurnos;

        public ReportesController(
            ICUIngresosSucursalSector ingresosSucursalSector,
            ICUEstadoTurnos estadoTurnos,
            ICUTurnosPorServicio turnosPorServicio,
            ICUHorarioMayorTurnos horarioMayorTurnos)
        {
            _ingresosSucursalSector = ingresosSucursalSector;
            _estadoTurnos = estadoTurnos;
            _turnosPorServicio = turnosPorServicio;
            _horarioMayorTurnos = horarioMayorTurnos;
        }

        /// <summary>
        /// Obtiene el reporte de ingresos por sucursal y sector.
        /// </summary>
        [HttpGet("ingresos-sucursales")]
        [SwaggerOperation(Summary = "Ingresos por sucursal y sector")]
        [SwaggerResponse(200, "Lista de ingresos por sucursal", typeof(IEnumerable<IngresosSucursalDTO>))]
        public IActionResult IngresosSucursales(int anio, int mes)
        {
            var datos = _ingresosSucursalSector.Ejecutar(anio, mes);
            return Ok(datos);
        }

        /// <summary>
        /// Obtiene la cantidad de turnos realizados y cancelados.
        /// </summary>
        [HttpGet("estado-turnos")]
        [SwaggerOperation(Summary = "Cantidad de turnos realizados y cancelados")]
        [SwaggerResponse(200, "Estado de los turnos", typeof(EstadoTurnosDTO))]
        public IActionResult EstadoTurnos(int anio, int mes)
        {
            var datos = _estadoTurnos.Ejecutar(anio, mes);
            return Ok(datos);
        }

        /// <summary>
        /// Obtiene la cantidad de turnos asignados por servicio.
        /// </summary>
        [HttpGet("turnos-por-servicio")]
        [SwaggerOperation(Summary = "Cantidad de turnos por servicio")]
        [SwaggerResponse(200, "Turnos por servicio", typeof(IEnumerable<TurnosPorServicioDTO>))]
        public IActionResult TurnosPorServicio(int anio, int mes)
        {
            var datos = _turnosPorServicio.Ejecutar(anio, mes);
            return Ok(datos);
        }

        /// <summary>
        /// Obtiene el horario con la mayor cantidad de turnos registrados.
        /// </summary>
        [HttpGet("horario-mayor-turnos")]
        [SwaggerOperation(Summary = "Horario con mayor cantidad de turnos")]
        [SwaggerResponse(200, "Horario con mayor demanda", typeof(HorarioMayorTurnosDTO))]
        public IActionResult HorarioMayorTurnos(int anio, int mes)
        {
            var dato = _horarioMayorTurnos.Ejecutar(anio, mes);
            return Ok(dato);
        }
    }
}
