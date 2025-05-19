using apiJMBROWS.UtilidadesJwt;
using Libreria.LogicaAplicacion.CasosDeUso.CUUsuarios;
using Libreria.LogicaNegocio.Excepciones;
using LogicaAplicacion.Dtos.DtoUsuario;
using LogicaAplicacion.InterfacesCasosDeUso;
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
        private readonly ICULoginUsuario _login;
        private readonly IConfiguration _config;

        public UsuarioController(ICUAltaUsuario altaUsuario, ICULoginUsuario login, IConfiguration config)
        {
            _altaUsuario = altaUsuario;
            _login = login;
            _config = config;
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Autenticación de usuario.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Autenticación exitosa.", typeof(LoginDTO))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Credenciales incorrectas.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor.")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid)                          // ← validación automática
                return BadRequest(ModelState);

            try
            {
                var usuario = _login.LoginUsuario(dto);

                var token = ManejadorJwt.GenerarToken(
                    usuario.Email,
                    usuario.Rol,
                    _config["Jwt:Key"],
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"]
                );

                return Ok(new
                {
                    token,
                    email = usuario.Email,
                    rol = usuario.Rol,
                    expires = DateTime.UtcNow.AddHours(2)
                });
            }
            catch (UsuarioException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
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
