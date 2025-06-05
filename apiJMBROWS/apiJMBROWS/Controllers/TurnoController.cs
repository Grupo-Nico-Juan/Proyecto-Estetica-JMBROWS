using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Mvc;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurnosController : ControllerBase
    {
        private readonly IRepositorioTurnos _repositorioTurnos;

        public TurnosController(IRepositorioTurnos repositorioTurnos)
        {
            _repositorioTurnos = repositorioTurnos;
        }

        // GET api/turnos
        [HttpGet]
        public IActionResult GetAll()
        {
            var todos = _repositorioTurnos.GetAll();
            return Ok(todos);
        }

        // GET api/turnos/5
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var turno = _repositorioTurnos.GetById(id);
            if (turno == null) return NotFound();
            return Ok(turno);
        }

        // POST api/turnos
        [HttpPost]
        public IActionResult Create([FromBody] Turno nuevo)
        {
            try
            {
                _repositorioTurnos.Add(nuevo);
                return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/turnos/5
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Turno modificado)
        {
            try
            {
                _repositorioTurnos.Update(id, modificado);
                return NoContent();
            }
            catch (InvalidOperationException inv)
            {
                return NotFound(inv.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/turnos/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repositorioTurnos.Remove(id);
                return NoContent();
            }
            catch (InvalidOperationException inv)
            {
                return NotFound(inv.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/turnos/empleada/3
        [HttpGet("empleada/{empleadaId:int}")]
        public IActionResult TurnosPorEmpleada(int empleadaId)
        {
            var lista = _repositorioTurnos.BuscarPorEmpleada(empleadaId);
            return Ok(lista);
        }

        // GET api/turnos/empleada/3/hoy
        [HttpGet("empleada/{empleadaId:int}/hoy")]
        public IActionResult TurnosHoy(int empleadaId)
        {
            var lista = _repositorioTurnos.ObtenerTurnosDelDiaPorEmpleada(empleadaId, DateTime.Now);
            return Ok(lista);
        }
    }
}
