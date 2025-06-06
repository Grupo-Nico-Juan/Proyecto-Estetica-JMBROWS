using LogicaAplicacion.Dtos.SucursalDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUSurcursal;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SucursalController : ControllerBase
{
    private readonly ICUAltaSucursal _altaSucursal;

    public SucursalController(ICUAltaSucursal altaSucursal)
    {
        _altaSucursal = altaSucursal;
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
}
