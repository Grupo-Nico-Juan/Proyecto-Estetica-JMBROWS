using LogicaAplicacion.Dtos.PeriodoLaboralDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUPeriodoLaboral;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

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
        private readonly ICUObtenerPeriodosLaboralesPorSucursal _obtenerPeriodosPorSucursal;

        public PeriodoLaboralController(
            ICUObtenerPeriodosLaboralesPorEmpleada obtenerPeriodosPorEmpleada,
            ICUAltaPeriodoLaboral altaPeriodoLaboral,
            ICUModificarPeriodoLaboral modificarPeriodoLaboral,
            ICUEliminarPeriodoLaboral eliminarPeriodoLaboral,
            ICUObtenerPeriodosLaboralesPorSucursal obtenerPeriodosPorSucursal)
        {
            _obtenerPeriodosPorEmpleada = obtenerPeriodosPorEmpleada;
            _altaPeriodoLaboral = altaPeriodoLaboral;
            _modificarPeriodoLaboral = modificarPeriodoLaboral;
            _eliminarPeriodoLaboral = eliminarPeriodoLaboral;
            _obtenerPeriodosPorSucursal = obtenerPeriodosPorSucursal;
        }

        /// <summary>
        /// Obtiene todos los periodos laborales de una empleada.
        /// </summary>
        /// <param name="empleadaId">ID de la empleada</param>
        [HttpGet("empleada/{empleadaId}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obtiene todos los periodos laborales de una empleada")]
        [SwaggerResponse(200, "Lista de periodos laborales", typeof(IEnumerable<PeriodoLaboralDTO>))]
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
        /// Obtiene los periodos HorarioHabitual de las empleadas de una sucursal agrupados por día.
        /// </summary>
        [HttpGet("sucursal/{sucursalId}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obtiene periodos laborales por sucursal")]
        [SwaggerResponse(200, "Periodos agrupados por día", typeof(Dictionary<DayOfWeek, List<PeriodoLaboralDTO>>))]
        public IActionResult GetPorSucursal(int sucursalId)
        {
            try
            {
                var periodos = _obtenerPeriodosPorSucursal.Ejecutar(sucursalId);
                if (periodos == null || periodos.Count == 0)
                    return NotFound(new { error = "No se encontraron periodos laborales para la sucursal." });

                return Ok(periodos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene un periodo laboral por su ID.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador")] // o [AllowAnonymous] si querés que sea público
        [SwaggerOperation(Summary = "Obtiene un periodo laboral por su ID")]
        [SwaggerResponse(200, "Periodo laboral encontrado", typeof(PeriodoLaboralDTO))]
        [SwaggerResponse(404, "Periodo laboral no encontrado")]
        public IActionResult GetPorId(int id)
        {
            try
            {

                var periodo = _obtenerPeriodosPorEmpleada
                    .Ejecutar(id) 
                    .FirstOrDefault(p => p.Id == id);

                if (periodo == null)
                    return NotFound(new { error = "No se encontró el periodo laboral." });

                return Ok(periodo);
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
        public IActionResult Post([FromBody] AltaPeriodoLaboralDTO dto)
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
        public IActionResult Put(int id, [FromBody] PeriodoLaboralDTO dto)
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