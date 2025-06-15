using LogicaAplicacion.Dtos.HabilidadDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUHabilidad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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

    [HttpGet]
    [SwaggerOperation(Summary = "Obtiene todas las habilidades")]
    [SwaggerResponse(200, "Lista de habilidades", typeof(IEnumerable<HabilidadDTO>))]
    public IActionResult Get()
    {
        var habilidades = _obtenerHabilidades.Ejecutar();
        return Ok(habilidades);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Obtiene una habilidad por ID")]
    [SwaggerResponse(200, "Habilidad encontrada", typeof(HabilidadDTO))]
    [SwaggerResponse(404, "Habilidad no encontrada")]
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

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    [SwaggerOperation(Summary = "Crea una nueva habilidad (solo administradores)")]
    [SwaggerResponse(200, "Habilidad creada correctamente")]
    [SwaggerResponse(400, "Error en los datos de la habilidad")]
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
    [Authorize(Roles = "Administrador")]
    [SwaggerOperation(Summary = "Actualiza una habilidad (solo administradores)")]
    [SwaggerResponse(200, "Habilidad actualizada correctamente")]
    [SwaggerResponse(400, "Error en los datos de la habilidad")]
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
    [Authorize(Roles = "Administrador")]
    [SwaggerOperation(Summary = "Elimina una habilidad (solo administradores)")]
    [SwaggerResponse(200, "Habilidad eliminada correctamente")]
    [SwaggerResponse(404, "Habilidad no encontrada")]
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
    [SwaggerOperation(Summary = "Busca habilidades por nombre")]
    [SwaggerResponse(200, "Lista de habilidades encontradas", typeof(IEnumerable<HabilidadDTO>))]
    public IActionResult Buscar(string texto)
    {
        var resultados = _buscarHabilidadesPorNombre.Ejecutar(texto);
        return Ok(resultados);
    }
}