using apiJMBROWS.UtilidadesJwt;
using Libreria.LogicaNegocio.Excepciones;
using LogicaAplicacion.Dtos;
using LogicaAplicacion.Dtos.ClienteDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUCliente;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ICUAltaCliente _altaCliente;
        private readonly ICUObtenerClientePorTelefono _obtenerClientePorTelefono;

        public ClienteController(ICUAltaCliente altaCliente, ICUObtenerClientePorTelefono obtenerClientePorTelefono)
        {
            _altaCliente = altaCliente;
            _obtenerClientePorTelefono = obtenerClientePorTelefono;
        }

        /// <summary>
        /// Registra un nuevo cliente.
        /// </summary>
        [HttpPost("registrar")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Registra un nuevo cliente.")]
        [SwaggerResponse(StatusCodes.Status201Created, "Cliente registrado exitosamente.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Error en los datos.")]
        public IActionResult Registrar([FromBody] RegistroClienteDTO dto)
        {
            try
            {
                _altaCliente.AltaCliente(dto);
                return StatusCode(201, "Cliente registrado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene un cliente por teléfono.
        /// </summary>
        [HttpGet("telefono/{telefono}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obtiene un cliente por teléfono.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Cliente encontrado.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Cliente no encontrado.")]
        public IActionResult GetByTelefono(string telefono)
        {
            var cliente = _obtenerClientePorTelefono.Ejecutar(telefono);
            if (cliente == null)
                return NotFound();
            return Ok(cliente);
        }
    }
}
