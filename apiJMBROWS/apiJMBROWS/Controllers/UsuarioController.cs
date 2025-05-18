using Libreria.LogicaAplicacion.CasosDeUso.CUUsuarios;
using Libreria.LogicaNegocio.Excepciones;
using LogicaAplicacion.Dtos.DtoUsuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace apiJMBROWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ICUAltaUsuario _altaUsuario;

        public UsuarioController(ICUAltaUsuario altaUsuario)
        {
            _altaUsuario = altaUsuario;
        }

        // POST: api/Usuario/Registrar
        [AllowAnonymous]
        [HttpPost("Registrar")]
        [SwaggerOperation(Summary = "Registro de un nuevo usuario.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Usuario registrado exitosamente.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Error en los datos del usuario.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor.")]
        public IActionResult Registrar([FromBody] RegistroUsuarioDTO dto)
        {
            try
            {
                _altaUsuario.AltaUsuario(dto);
                return Ok("Usuario registrado correctamente.");
            }
            catch (UsuarioException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno: " + ex.Message });
            }
        }
    }
}
