namespace apiJMBROWS.Controllers
{
    using Libreria.LogicaNegocio.InterfacesRepositorio;
    using LogicaNegocio.Entidades;
    using LogicaNegocio.InterfacesRepositorio;
    using Microsoft.AspNetCore.Mvc;

    namespace apiJMBROWS.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class HabilidadController : ControllerBase
        {
            private readonly IRepositorioHabilidades _repo;

            public HabilidadController(IRepositorioHabilidades repo)
            {
                _repo = repo;
            }

            [HttpGet]
            public IActionResult Get()
            {
                var habilidades = _repo.GetAll();
                return Ok(habilidades);
            }

            [HttpGet("{id}")]
            public IActionResult Get(int id)
            {
                var habilidad = _repo.GetById(id);
                if (habilidad == null)
                    return NotFound();
                return Ok(habilidad);
            }

            [HttpPost]
            public IActionResult Post([FromBody] Habilidad habilidad)
            {
                try
                {
                    _repo.Add(habilidad);
                    return CreatedAtAction(nameof(Get), new { id = habilidad.Id }, habilidad);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { error = ex.Message });
                }
            }

            [HttpPut("{id}")]
            public IActionResult Put(int id, [FromBody] Habilidad habilidad)
            {
                try
                {
                    _repo.Update(id, habilidad);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(new { error = ex.Message });
                }
            }

            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                try
                {
                    _repo.Remove(id);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return NotFound(new { error = ex.Message });
                }
            }

            [HttpGet("buscar/{texto}")]
            public IActionResult Buscar(string texto)
            {
                var resultados = _repo.BuscarPorNombre(texto);
                return Ok(resultados);
            }
        }
    }

}
