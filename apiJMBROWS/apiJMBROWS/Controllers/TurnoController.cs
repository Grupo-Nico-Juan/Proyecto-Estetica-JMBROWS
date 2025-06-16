using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUTurno;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TurnosController : ControllerBase
    {
        private readonly ICUAltaTurno _altaTurno;
        private readonly ICUObtenerTurnos _obtenerTurnos;
        private readonly ICUObtenerTurnoPorId _obtenerTurnoPorId;
        private readonly ICUObtenerTurnosPorEmpleada _obtenerTurnosPorEmpleada;
        private readonly ICUObtenerTurnosDelDiaPorEmpleada _obtenerTurnosDelDiaPorEmpleada;
        private readonly ICUActualizarTurno _actualizarTurno;
        private readonly ICUEliminarTurno _eliminarTurno;

        public TurnosController(
            ICUAltaTurno altaTurno,
            ICUObtenerTurnos obtenerTurnos,
            ICUObtenerTurnoPorId obtenerTurnoPorId,
            ICUObtenerTurnosPorEmpleada obtenerTurnosPorEmpleada,
            ICUObtenerTurnosDelDiaPorEmpleada obtenerTurnosDelDiaPorEmpleada,
            ICUActualizarTurno actualizarTurno,
            ICUEliminarTurno eliminarTurno)
        {
            _altaTurno = altaTurno;
            _obtenerTurnos = obtenerTurnos;
            _obtenerTurnoPorId = obtenerTurnoPorId;
            _obtenerTurnosPorEmpleada = obtenerTurnosPorEmpleada;
            _obtenerTurnosDelDiaPorEmpleada = obtenerTurnosDelDiaPorEmpleada;
            _actualizarTurno = actualizarTurno;
            _eliminarTurno = eliminarTurno;
        }

        /// <summary>
        /// Obtiene todos los turnos.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obtiene todos los turnos")]
        [SwaggerResponse(200, "Lista de turnos", typeof(IEnumerable<TurnoDTO>))]
        public IActionResult GetAll()
        {
            var turnos = _obtenerTurnos.Ejecutar();
            return Ok(turnos);
        }

        /// <summary>
        /// Obtiene un turno por su ID.
        /// </summary>
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obtiene un turno por ID")]
        [SwaggerResponse(200, "Turno encontrado", typeof(TurnoDTO))]
        [SwaggerResponse(404, "Turno no encontrado")]
        public IActionResult GetById(int id)
        {
            try
            {
                var turno = _obtenerTurnoPorId.Ejecutar(id);
                return Ok(turno);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Crea un nuevo turno. Solo administradores.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Crea un nuevo turno (solo administradores)")]
        [SwaggerResponse(201, "Turno creado correctamente")]
        [SwaggerResponse(400, "Error en los datos del turno")]
        public IActionResult Create([FromBody] AltaTurnoDTO dto)
        {
            try
            {
                _altaTurno.Ejecutar(dto);
                return StatusCode(201, "Turno creado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene los turnos de una empleada.
        /// </summary>
        [HttpGet("empleada/{empleadaId:int}")]
        [SwaggerOperation(Summary = "Obtiene los turnos de una empleada")]
        [SwaggerResponse(200, "Lista de turnos", typeof(IEnumerable<TurnoDTO>))]
        public IActionResult TurnosPorEmpleada(int empleadaId)
        {
            var lista = _obtenerTurnosPorEmpleada.Ejecutar(empleadaId);
            return Ok(lista);
        }

        /// <summary>
        /// Obtiene los turnos de hoy de una empleada.
        /// </summary>
        [HttpGet("empleada/{empleadaId:int}/hoy")]
        [SwaggerOperation(Summary = "Obtiene los turnos de hoy de una empleada")]
        [SwaggerResponse(200, "Lista de turnos de hoy", typeof(IEnumerable<TurnoDTO>))]
        public IActionResult TurnosHoy(int empleadaId)
        {
            var lista = _obtenerTurnosDelDiaPorEmpleada.Ejecutar(empleadaId, DateTime.Today);
            return Ok(lista);
        }

        /// <summary>
        /// Actualiza un turno existente. Solo administradores.
        /// </summary>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Actualiza un turno (solo administradores)")]
        [SwaggerResponse(200, "Turno actualizado correctamente")]
        [SwaggerResponse(400, "Error en los datos del turno")]
        [SwaggerResponse(404, "Turno no encontrado")]
        public IActionResult Update(int id, [FromBody] ActualizarTurnoDTO dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest(new { error = "El id de la URL no coincide con el del cuerpo." });

                _actualizarTurno.Ejecutar(dto);
                return Ok("Turno actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un turno por ID. Solo administradores.
        /// </summary>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Elimina un turno (solo administradores)")]
        [SwaggerResponse(200, "Turno eliminado correctamente")]
        [SwaggerResponse(404, "Turno no encontrado")]
        public IActionResult Delete(int id)
        {
            try
            {
                _eliminarTurno.Ejecutar(id);
                return Ok("Turno eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}