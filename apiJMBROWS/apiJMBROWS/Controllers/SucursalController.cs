using LogicaAplicacion.Dtos.SucursalDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUSurcursal;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
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

    [HttpPost]
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

    [HttpPut("{id}")]
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

    [HttpGet]
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

    [HttpGet("{id}")]
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

    [HttpDelete("{id}")]
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
