using LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.Excepciones;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Mvc;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly IRepositorioUsuarios _repo;

        public EmpleadoController(IRepositorioUsuarios repo)
        {
            _repo = repo;
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
        [HttpPost]
        public IActionResult Post([FromBody] Empleado e)
        {
            try
            {
                _repo.AddEmpleado(e);
                return CreatedAtAction(nameof(Get), new { id = e.Id }, e);
            }
            catch (UsuarioException ex)
            {
                return BadRequest(new { error = ex.Message });
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

