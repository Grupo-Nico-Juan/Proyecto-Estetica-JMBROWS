using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Mvc;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioController : ControllerBase
    {
        private readonly IRepositorioServicios _repo;

        public ServicioController(IRepositorioServicios repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_repo.GetAll());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var s = _repo.GetById(id);
            return s == null ? NotFound() : Ok(s);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Servicio s)
        {
            _repo.Add(s);
            return CreatedAtAction(nameof(Get), new { id = s.Id }, s);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Servicio s)
        {
            _repo.Update(id, s);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repo.Remove(id);
            return NoContent();
        }

        [HttpGet("buscar/{texto}")]
        public IActionResult Buscar(string texto)
        {
            return Ok(_repo.BuscarPorNombre(texto));
        }
    }

}
