using apiJMBROWS.UtilidadesJwt;
using Libreria.LogicaNegocio.Excepciones;
using LogicaAplicacion.Dtos;
using LogicaAplicacion.Dtos.DtoUsuario;
using LogicaAplicacion.Dtos.EmpleadoDTO;
using LogicaAplicacion.InterfacesCasosDeUso;
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
        private readonly ICULoginCliente _loginCliente;
        private readonly IConfiguration _config;
        private readonly ICUGetClientes _getClientes;

        public ClienteController(ICUAltaCliente altaCliente, ICULoginCliente loginCliente, IConfiguration config, ICUGetClientes clientes)
        {
            _altaCliente = altaCliente;
            _loginCliente = loginCliente;
            _config = config;
            _getClientes = clientes;
        }

        [HttpPost("registrar")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Registra un nuevo cliente.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Cliente registrado exitosamente.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Error en los datos.")]
        public IActionResult Registrar([FromBody] RegistroClienteDTO dto)
        {
            try
            {
                _altaCliente.AltaCliente(dto);
                return Ok("Cliente registrado correctamente.");
            }
            catch (UsuarioException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno: " + ex.Message });
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Autenticación de cliente.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Autenticación exitosa.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Credenciales inválidas.")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            try
            {
                Cliente cliente = _loginCliente.LoginCliente(dto);

                string token = ManejadorJwt.GenerarTokenCliente(
                    cliente.Email,
                    _config["Jwt:Key"],
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"]
                );


                return Ok(new
                {
                    token,
                    email = cliente.Email,
                    expires = DateTime.UtcNow.AddHours(2)
                });
            }
            catch (UsuarioException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }


        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todos los clientes")]
        [SwaggerResponse(200, "Lista de clientes", typeof(IEnumerable<ClienteDTO>))]
        public IActionResult getClientes()
        {
            var clientes = _getClientes.Ejecutar();
            return Ok(clientes);
        }
    }
}
