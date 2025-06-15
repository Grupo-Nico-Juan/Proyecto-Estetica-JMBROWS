using LogicaAplicacion.InterfacesCasosDeUso.ICUPeriodoLaboral;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PeriodoLaboralController : ControllerBase
    {
        private readonly ICUObtenerPeriodosLaboralesPorEmpleada _obtenerPeriodosPorEmpleada;
        private readonly ICUAltaPeriodoLaboral _altaPeriodoLaboral;
        private readonly ICUModificarPeriodoLaboral _modificarPeriodoLaboral;
        private readonly ICUEliminarPeriodoLaboral _eliminarPeriodoLaboral;

        public PeriodoLaboralController(
            ICUObtenerPeriodosLaboralesPorEmpleada obtenerPeriodosPorEmpleada,
            ICUAltaPeriodoLaboral altaPeriodoLaboral,
            ICUModificarPeriodoLaboral modificarPeriodoLaboral,
            ICUEliminarPeriodoLaboral eliminarPeriodoLaboral)
        {
            _obtenerPeriodosPorEmpleada = obtenerPeriodosPorEmpleada;
            _altaPeriodoLaboral = altaPeriodoLaboral;
            _modificarPeriodoLaboral = modificarPeriodoLaboral;
            _eliminarPeriodoLaboral = eliminarPeriodoLaboral;
        }

        /// <summary>
        /// Obtiene todos los periodos laborales de una empleada.
        /// </summary>
        /// <param name="empleadaId">ID de la empleada</param>
        [HttpGet("empleada/{empleadaId}")]
        [SwaggerOperation(Summary = "Obtiene todos los periodos laborales de una empleada")]
        [SwaggerResponse(200, "Lista de periodos laborales", typeof(IEnumerable<LogicaAplicacion.Dtos.PeriodoLaboralDTO.PeriodoLaboralDTO>))]
        [SwaggerResponse(404, "No se encontraron periodos laborales para la empleada")]
        public IActionResult GetPorEmpleada(int empleadaId)
        {
            try
            {
                var periodos = _obtenerPeriodosPorEmpleada.Ejecutar(empleadaId);
                if (periodos == null || !periodos.Any())
                    return NotFound(new { error = "No se encontraron periodos laborales para la empleada." });

                return Ok(periodos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Crea un nuevo periodo laboral para una empleada.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Crea un nuevo periodo laboral para una empleada")]
        [SwaggerResponse(200, "Periodo laboral creado correctamente")]
        [SwaggerResponse(400, "Error en los datos del periodo laboral")]
        public IActionResult Post([FromBody] LogicaAplicacion.Dtos.PeriodoLaboralDTO.AltaPeriodoLaboralDTO dto)
        {
            try
            {
                _altaPeriodoLaboral.Ejecutar(dto);
                return Ok("Periodo laboral creado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Modifica un periodo laboral existente. Solo administradores.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Modifica un periodo laboral (solo administradores)")]
        [SwaggerResponse(200, "Periodo laboral modificado correctamente")]
        [SwaggerResponse(400, "Error en los datos del periodo laboral")]
        public IActionResult Put(int id, [FromBody] LogicaAplicacion.Dtos.PeriodoLaboralDTO.PeriodoLaboralDTO dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest(new { error = "El id de la URL no coincide con el del cuerpo." });

                _modificarPeriodoLaboral.Ejecutar(dto);
                return Ok("Periodo laboral modificado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un periodo laboral por ID. Solo administradores.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Elimina un periodo laboral (solo administradores)")]
        [SwaggerResponse(200, "Periodo laboral eliminado correctamente")]
        [SwaggerResponse(404, "Periodo laboral no encontrado")]
        public IActionResult Delete(int id)
        {
            try
            {
                _eliminarPeriodoLaboral.Ejecutar(id);
                return Ok("Periodo laboral eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}