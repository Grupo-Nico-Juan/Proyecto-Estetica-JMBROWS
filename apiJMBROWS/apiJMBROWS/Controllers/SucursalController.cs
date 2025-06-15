using LogicaAplicacion.Dtos.SucursalDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUSurcursal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SucursalController : ControllerBase
{
    private readonly ICUAltaSucursal _altaSucursal;
    private readonly ICUModificarSucursal _modificarSucursal;
    private readonly ICUObtenerSucursales _obtenerSucursales;
    private readonly ICUObtenerSucursalPorId _obtenerSucursalPorId;
    private readonly ICUEliminarSucursal _eliminarSucursal;

    public SucursalController(
        ICUAltaSucursal altaSucursal,
        ICUModificarSucursal modificarSucursal,
        ICUObtenerSucursales obtenerSucursales,
        ICUObtenerSucursalPorId obtenerSucursalPorId,
        ICUEliminarSucursal eliminarSucursal)
    {
        _altaSucursal = altaSucursal;
        _modificarSucursal = modificarSucursal;
        _obtenerSucursales = obtenerSucursales;
        _obtenerSucursalPorId = obtenerSucursalPorId;
        _eliminarSucursal = eliminarSucursal;
    }

    /// <summary>
    /// Obtiene todas las sucursales.
    /// </summary>
    [HttpGet]
    [SwaggerOperation(Summary = "Obtiene todas las sucursales")]
    [SwaggerResponse(200, "Lista de sucursales", typeof(IEnumerable<SucursalDTO>))]
    public IActionResult Get()
    {
        try
        {
            var sucursales = _obtenerSucursales.Ejecutar();
            return Ok(sucursales);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene una sucursal por su ID.
    /// </summary>
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Obtiene una sucursal por ID")]
    [SwaggerResponse(200, "Sucursal encontrada", typeof(SucursalDTO))]
    [SwaggerResponse(404, "Sucursal no encontrada")]
    public IActionResult Get(int id)
    {
        try
        {
            var sucursal = _obtenerSucursalPorId.Ejecutar(id);
            return Ok(sucursal);
        }
        catch (Exception ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Crea una nueva sucursal. Solo administradores.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    [SwaggerOperation(Summary = "Crea una nueva sucursal (solo administradores)")]
    [SwaggerResponse(200, "Sucursal creada correctamente")]
    [SwaggerResponse(400, "Error en los datos de la sucursal")]
    public IActionResult Post([FromBody] AltaSucursalDTO dto)
    {
        try
        {
            _altaSucursal.Ejecutar(dto);
            return Ok("Sucursal creada correctamente.");
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Modifica una sucursal existente. Solo administradores.
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Administrador")]
    [SwaggerOperation(Summary = "Modifica una sucursal (solo administradores)")]
    [SwaggerResponse(200, "Sucursal modificada correctamente")]
    [SwaggerResponse(400, "Error en los datos de la sucursal")]
    public IActionResult Put(int id, [FromBody] SucursalDTO dto)
    {
        try
        {
            if (id != dto.Id)
                return BadRequest(new { error = "El id de la URL no coincide con el del cuerpo." });

            _modificarSucursal.Ejecutar(dto);
            return Ok("Sucursal modificada correctamente.");
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Elimina una sucursal por ID. Solo administradores.
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    [SwaggerOperation(Summary = "Elimina una sucursal (solo administradores)")]
    [SwaggerResponse(200, "Sucursal eliminada correctamente")]
    [SwaggerResponse(404, "Sucursal no encontrada")]
    public IActionResult Delete(int id)
    {
        try
        {
            _eliminarSucursal.Ejecutar(id);
            return Ok("Sucursal eliminada correctamente.");
        }
        catch (Exception ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }
}