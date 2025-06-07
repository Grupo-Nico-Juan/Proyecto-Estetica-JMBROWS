using LogicaAplicacion.Dtos.HabilidadDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUHabilidad;
using Microsoft.AspNetCore.Mvc;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HabilidadController : ControllerBase
    {
        private readonly ICUAltaHabilidad _altaHabilidad;
        private readonly ICUActualizarHabilidad _actualizarHabilidad;
        private readonly ICUEliminarHabilidad _eliminarHabilidad;
        private readonly ICUObtenerHabilidadPorId _obtenerHabilidadPorId;
        private readonly ICUBuscarHabilidadesPorNombre _buscarHabilidadesPorNombre;
        private readonly ICUObtenerHabilidades _obtenerHabilidades;

        public HabilidadController(
            ICUAltaHabilidad altaHabilidad,
            ICUActualizarHabilidad actualizarHabilidad,
            ICUEliminarHabilidad eliminarHabilidad,
            ICUObtenerHabilidadPorId obtenerHabilidadPorId,
            ICUBuscarHabilidadesPorNombre buscarHabilidadesPorNombre,
            ICUObtenerHabilidades obtenerHabilidades)
        {
            _altaHabilidad = altaHabilidad;
            _actualizarHabilidad = actualizarHabilidad;
            _eliminarHabilidad = eliminarHabilidad;
            _obtenerHabilidadPorId = obtenerHabilidadPorId;
            _buscarHabilidadesPorNombre = buscarHabilidadesPorNombre;
            _obtenerHabilidades = obtenerHabilidades;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var habilidad = _obtenerHabilidadPorId.Ejecutar(id);
                return Ok(habilidad);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            var habilidades = _obtenerHabilidades.Ejecutar();
            return Ok(habilidades);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AltaHabilidadDTO dto)
        {
            try
            {
                _altaHabilidad.Ejecutar(dto);
                return Ok("Habilidad creada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ActualizarHabilidadDTO dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest(new { error = "El id de la URL no coincide con el del cuerpo." });

                _actualizarHabilidad.Ejecutar(dto);
                return Ok("Habilidad actualizada correctamente.");
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
                _eliminarHabilidad.Ejecutar(id);
                return Ok("Habilidad eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpGet("buscar/{texto}")]
        public IActionResult Buscar(string texto)
        {
            var resultados = _buscarHabilidadesPorNombre.Ejecutar(texto);
            return Ok(resultados);
        }
    }
}
