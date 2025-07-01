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

        [HttpGet("ingresos-sucursales")]
        [SwaggerOperation(Summary = "Ingresos por sucursal y sector" )]
        public IActionResult IngresosSucursales(int anio, int mes)
        {
            var datos = _ingresosSucursalSector.Ejecutar(anio, mes);
            return Ok(datos);
        }

        [HttpGet("estado-turnos")]
        [SwaggerOperation(Summary = "Cantidad de turnos realizados y cancelados" )]
        public IActionResult EstadoTurnos(int anio, int mes)
        {
            var datos = _estadoTurnos.Ejecutar(anio, mes);
            return Ok(datos);
        }

        [HttpGet("turnos-por-servicio")]
        [SwaggerOperation(Summary = "Cantidad de turnos por servicio" )]
        public IActionResult TurnosPorServicio(int anio, int mes)
        {
            var datos = _turnosPorServicio.Ejecutar(anio, mes);
            return Ok(datos);
        }

        [HttpGet("horario-mayor-turnos")]
        [SwaggerOperation(Summary = "Horario con mayor cantidad de turnos" )]
        public IActionResult HorarioMayorTurnos(int anio, int mes)
        {
            var dato = _horarioMayorTurnos.Ejecutar(anio, mes);
            return Ok(dato);
        }
    }
}
