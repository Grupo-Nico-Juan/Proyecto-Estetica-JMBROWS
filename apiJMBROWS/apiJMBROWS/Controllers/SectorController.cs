using LogicaAplicacion.Dtos.SectorDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUSector;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SectorController : ControllerBase
    {
        private readonly ICUAltaSector _altaSector;
        private readonly ICUActualizarSector _actualizarSector;
        private readonly ICUEliminarSector _eliminarSector;
        private readonly ICUObtenerSectorPorId _obtenerSectorPorId;
        private readonly ICUObtenerSectores _obtenerSectores;
        private readonly ICUObtenerSectoresPorSucursal _obtenerSectoresPorSucursal;

        public SectorController(
            ICUAltaSector altaSector,
            ICUActualizarSector actualizarSector,
            ICUEliminarSector eliminarSector,
            ICUObtenerSectorPorId obtenerSectorPorId,
            ICUObtenerSectores obtenerSectores,
            ICUObtenerSectoresPorSucursal obtenerSectoresSucursal)
        {
            _altaSector = altaSector;
            _actualizarSector = actualizarSector;
            _eliminarSector = eliminarSector;
            _obtenerSectorPorId = obtenerSectorPorId;
            _obtenerSectores = obtenerSectores;
            _obtenerSectoresPorSucursal = obtenerSectoresSucursal;
        }

        /// <summary>
        /// Obtiene todos los sectores.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todos los sectores")]
        [SwaggerResponse(200, "Lista de sectores", typeof(IEnumerable<SectorDTSSuc>))]
        public IActionResult Get()
        {
            var sectores = _obtenerSectores.Ejecutar();
            return Ok(sectores);
        }

        /// <summary>
        /// Obtiene un sector por su ID.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un sector por ID")]
        [SwaggerResponse(200, "Sector encontrado", typeof(SectorDTSSuc))]
        [SwaggerResponse(404, "Sector no encontrado")]
        public IActionResult GetById(int id)
        {
            try
            {
                var sector = _obtenerSectorPorId.Ejecutar(id);
                return Ok(sector);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Crea un nuevo sector. Solo administradores.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Crea un nuevo sector (solo administradores)")]
        [SwaggerResponse(201, "Sector creado correctamente")]
        [SwaggerResponse(400, "Error en los datos del sector")]
        public IActionResult Post([FromBody] AltaSectorDTO dto)
        {
            try
            {
                _altaSector.Ejecutar(dto);
                return StatusCode(201, "Sector creado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza un sector existente. Solo administradores.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Actualiza un sector (solo administradores)")]
        [SwaggerResponse(200, "Sector actualizado correctamente")]
        [SwaggerResponse(400, "Error en los datos del sector")]
        [SwaggerResponse(404, "Sector no encontrado")]
        public IActionResult Put(int id, [FromBody] ActualizarSectorDTO dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest(new { error = "El id de la URL no coincide con el del cuerpo." });

                _actualizarSector.Ejecutar(dto);
                return Ok("Sector actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un sector por ID. Solo administradores.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Elimina un sector (solo administradores)")]
        [SwaggerResponse(200, "Sector eliminado correctamente")]
        [SwaggerResponse(404, "Sector no encontrado")]
        public IActionResult Delete(int id)
        {
            try
            {
                _eliminarSector.Ejecutar(id);
                return Ok("Sector eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// obtiene sectores por sucursal.
        /// </summary>
        [HttpGet("sucursal/{sucursalId}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obtiene sectores por sucursal con servicios")]
        [SwaggerResponse(200, "Sectores con servicios", typeof(IEnumerable<SectorDTSSuc>))]
        [SwaggerResponse(404, "Sectores no encontrados")]
        public IActionResult GetPorSucursal(int sucursalId)
        {
            try
            {
                var sectores = _obtenerSectoresPorSucursal.Ejecutar(sucursalId);
                return Ok(sectores);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

    }
}