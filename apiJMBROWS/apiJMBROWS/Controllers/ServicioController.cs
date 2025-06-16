using LogicaAplicacion.Dtos.ServicioDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServicioController : ControllerBase
    {
        private readonly ICUAltaServicio _altaServicio;
        private readonly ICUActualizarServicio _actualizarServicio;
        private readonly ICUEliminarServicio _eliminarServicio;
        private readonly ICUObtenerServicios _obtenerServicios;
        private readonly ICUObtenerServicioPorId _obtenerServicioPorId;
        private readonly ICUBuscarServiciosPorNombre _buscarServiciosPorNombre;

        public ServicioController(
            ICUAltaServicio altaServicio,
            ICUActualizarServicio actualizarServicio,
            ICUEliminarServicio eliminarServicio,
            ICUObtenerServicios obtenerServicios,
            ICUObtenerServicioPorId obtenerServicioPorId,
            ICUBuscarServiciosPorNombre buscarServiciosPorNombre)
        {
            _altaServicio = altaServicio;
            _actualizarServicio = actualizarServicio;
            _eliminarServicio = eliminarServicio;
            _obtenerServicios = obtenerServicios;
            _obtenerServicioPorId = obtenerServicioPorId;
            _buscarServiciosPorNombre = buscarServiciosPorNombre;
        }

        /// <summary>
        /// Obtiene todos los servicios.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obtiene todos los servicios")]
        [SwaggerResponse(200, "Lista de servicios", typeof(IEnumerable<ServicioDTO>))]
        public IActionResult Get()
        {
            var servicios = _obtenerServicios.Ejecutar();
            return Ok(servicios);
        }

        /// <summary>
        /// Obtiene un servicio por su ID.
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obtiene un servicio por ID")]
        [SwaggerResponse(200, "Servicio encontrado", typeof(ServicioDTO))]
        [SwaggerResponse(404, "Servicio no encontrado")]
        public IActionResult Get(int id)
        {
            try
            {
                var servicio = _obtenerServicioPorId.Ejecutar(id);
                return Ok(servicio);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Crea un nuevo servicio. Solo administradores.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Crea un nuevo servicio (solo administradores)")]
        [SwaggerResponse(200, "Servicio creado correctamente")]
        [SwaggerResponse(400, "Error en los datos del servicio")]
        public IActionResult Post([FromBody] AltaServicioDTO dto)
        {
            try
            {
                _altaServicio.Ejecutar(dto);
                return Ok("Servicio creado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza un servicio existente. Solo administradores.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Actualiza un servicio (solo administradores)")]
        [SwaggerResponse(200, "Servicio actualizado correctamente")]
        [SwaggerResponse(400, "Error en los datos del servicio")]
        public IActionResult Put(int id, [FromBody] ActualizarServicioDTO dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest(new { error = "El id de la URL no coincide con el del cuerpo." });

                _actualizarServicio.Ejecutar(dto);
                return Ok("Servicio actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un servicio por ID. Solo administradores.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Elimina un servicio (solo administradores)")]
        [SwaggerResponse(200, "Servicio eliminado correctamente")]
        [SwaggerResponse(404, "Servicio no encontrado")]
        public IActionResult Delete(int id)
        {
            try
            {
                _eliminarServicio.Ejecutar(id);
                return Ok("Servicio eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Busca servicios por nombre.
        /// </summary>
        [HttpGet("buscar/{texto}")]
        [SwaggerOperation(Summary = "Busca servicios por nombre")]
        [SwaggerResponse(200, "Lista de servicios encontrados", typeof(IEnumerable<ServicioDTO>))]
        public IActionResult Buscar(string texto)
        {
            var servicios = _buscarServiciosPorNombre.Ejecutar(texto);
            return Ok(servicios);
        }
    }
}