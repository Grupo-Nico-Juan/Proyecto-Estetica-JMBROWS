using LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.Excepciones;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Mvc;
using LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.Dtos.DtoUsuario;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Libreria.LogicaAplicacion.CasosDeUso.CUUsuarios;
using LogicaNegocio.Excepciones;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly IRepositorioUsuarios _repo;
        private readonly ICUAltaUsuario _altaUsuario;

        public EmpleadoController(IRepositorioUsuarios repo, ICUAltaUsuario altaUsuario)
        {
            _repo = repo;
            _altaUsuario = altaUsuario;
        }

        // GET: api/empleado
        [HttpGet]
        public IActionResult Get() => Ok(_repo.GetEmpleados());

        // GET: api/empleado/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var emp = _repo.GetEmpleadoById(id);
            if (emp == null) return NotFound();
            return Ok(emp);
        }

        // POST: api/empleado
        // POST: api/empleado/registrar
        [HttpPost("Registrar")]
        [Authorize(Roles = "Administrador")] // ✅ Solo admins pueden registrar
        [SwaggerOperation(Summary = "Registro de un nuevo empleado (solo administradores autorizados).")]
        [SwaggerResponse(StatusCodes.Status200OK, "Empleado registrado exitosamente.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Error en los datos del empleado.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "No autorizado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor.")]
        public IActionResult Registrar([FromBody] RegistroEmpleadoDTO dto)
        {
            try
            {
                _altaUsuario.AltaUsuario(dto);
                return Ok("Empleado registrado correctamente.");
            }
            catch (EmpleadoException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno: " + ex.Message });
            }
        }


        // PUT: api/empleado/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Empleado e)
        {
            try
            {
                _repo.UpdateEmpleado(id, e);
                return NoContent();
            }
            catch (UsuarioException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE: api/empleado/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repo.DeleteEmpleado(id);
                return NoContent();
            }
            catch (UsuarioException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }




    }
}

