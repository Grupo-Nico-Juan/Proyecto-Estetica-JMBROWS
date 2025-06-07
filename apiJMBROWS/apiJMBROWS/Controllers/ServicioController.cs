using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.Dtos.ServicioDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUServicio;
using Microsoft.AspNetCore.Mvc;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioController : ControllerBase
    {
        private readonly IRepositorioServicios _repo;
        private readonly ICUAltaServicio _altaServicio;
        private readonly ICUActualizarServicio _actualizarServicio;
        private readonly ICUEliminarServicio _eliminarServicio;
        private readonly ICUObtenerServicios _obtenerServicios;
        private readonly ICUObtenerServicioPorId _obtenerServicioPorId;
        private readonly ICUBuscarServiciosPorNombre _buscarServiciosPorNombre;

        public ServicioController(
            IRepositorioServicios repo,
            ICUAltaServicio altaServicio,
            ICUActualizarServicio actualizarServicio,
            ICUEliminarServicio eliminarServicio,
            ICUObtenerServicios obtenerServicios,
            ICUObtenerServicioPorId obtenerServicioPorId,
            ICUBuscarServiciosPorNombre buscarServiciosPorNombre)
        {
            _repo = repo;
            _altaServicio = altaServicio;
            _actualizarServicio = actualizarServicio;
            _eliminarServicio = eliminarServicio;
            _obtenerServicios = obtenerServicios;
            _obtenerServicioPorId = obtenerServicioPorId;
            _buscarServiciosPorNombre = buscarServiciosPorNombre;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var servicios = _obtenerServicios.Ejecutar();
            return Ok(servicios);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var servicio = _obtenerServicioPorId.Ejecutar(id);
                return Ok(servicio);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] AltaServicioDTO dto)
        {
            try
            {
                _altaServicio.Ejecutar(dto);
                return Ok("Servicio creado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ActualizarServicioDTO dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest(new { error = "El id de la URL no coincide con el del cuerpo." });

                _actualizarServicio.Ejecutar(dto);
                return Ok("Servicio actualizado correctamente.");
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
                _eliminarServicio.Ejecutar(id);
                return Ok("Servicio eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpGet("buscar/{texto}")]
        public IActionResult Buscar(string texto)
        {
            var servicios = _buscarServiciosPorNombre.Ejecutar(texto);
            return Ok(servicios);
        }
    }
}
