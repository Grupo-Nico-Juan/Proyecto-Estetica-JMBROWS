using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUDetalleTurno;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DetalleTurnoController : ControllerBase
    {
        private readonly ICUAltaDetalleTurno _altaDetalleTurno;
        private readonly ICUActualizarDetalleTurno _actualizarDetalleTurno;
        private readonly ICUEliminarDetalleTurno _eliminarDetalleTurno;
        private readonly ICUObtenerDetalleTurnoPorId _obtenerDetalleTurnoPorId;
        private readonly ICUObtenerDetallesTurno _obtenerDetallesTurno;

        public DetalleTurnoController(
            ICUAltaDetalleTurno altaDetalleTurno,
            ICUActualizarDetalleTurno actualizarDetalleTurno,
            ICUEliminarDetalleTurno eliminarDetalleTurno,
            ICUObtenerDetalleTurnoPorId obtenerDetalleTurnoPorId,
            ICUObtenerDetallesTurno obtenerDetallesTurno)
        {
            _altaDetalleTurno = altaDetalleTurno;
            _actualizarDetalleTurno = actualizarDetalleTurno;
            _eliminarDetalleTurno = eliminarDetalleTurno;
            _obtenerDetalleTurnoPorId = obtenerDetalleTurnoPorId;
            _obtenerDetallesTurno = obtenerDetallesTurno;
        }

        /// <summary>
        /// Obtiene todos los detalles de turno.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todos los detalles de turno")]
        [SwaggerResponse(200, "Lista de detalles", typeof(IEnumerable<DetalleTurnoDTO>))]
        public IActionResult Get()
        {
            var detalles = _obtenerDetallesTurno.Ejecutar();
            return Ok(detalles);
        }

        /// <summary>
        /// Obtiene un detalle de turno por ID.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un detalle de turno por ID")]
        [SwaggerResponse(200, "Detalle encontrado", typeof(DetalleTurnoDTO))]
        [SwaggerResponse(404, "Detalle no encontrado")]
        public IActionResult GetById(int id)
        {
            try
            {
                var detalle = _obtenerDetalleTurnoPorId.Ejecutar(id);
                return Ok(detalle);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Crea un nuevo detalle de turno. Solo administradores.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Crea un nuevo detalle de turno (solo administradores)")]
        [SwaggerResponse(201, "Detalle creado correctamente")]
        [SwaggerResponse(400, "Error en los datos del detalle")]
        public IActionResult Post([FromBody] AltaDetalleTurnoDTO dto)
        {
            try
            {
                _altaDetalleTurno.Ejecutar(dto);
                return StatusCode(201, "Detalle de turno creado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza un detalle de turno existente. Solo administradores.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Actualiza un detalle de turno (solo administradores)")]
        [SwaggerResponse(200, "Detalle actualizado correctamente")]
        [SwaggerResponse(400, "Error en los datos del detalle")]
        [SwaggerResponse(404, "Detalle no encontrado")]
        public IActionResult Put(int id, [FromBody] ActualizarDetalleTurnoDTO dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest(new { error = "El id de la URL no coincide con el del cuerpo." });

                _actualizarDetalleTurno.Ejecutar(dto);
                return Ok("Detalle de turno actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un detalle de turno por ID. Solo administradores.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Elimina un detalle de turno (solo administradores)")]
        [SwaggerResponse(200, "Detalle eliminado correctamente")]
        [SwaggerResponse(404, "Detalle no encontrado")]
        public IActionResult Delete(int id)
        {
            try
            {
                _eliminarDetalleTurno.Ejecutar(id);
                return Ok("Detalle de turno eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}