using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Mvc;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SucursalController : ControllerBase
    {
        private readonly IRepositorioSucursales _repo;

        public SucursalController(IRepositorioSucursales repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_repo.GetAll());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var suc = _repo.GetById(id);
            return suc == null ? NotFound() : Ok(suc);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Sucursal s)
        {
            _repo.Add(s);
            return CreatedAtAction(nameof(Get), new { id = s.Id }, s);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Sucursal s)
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
    }

}
