using LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.Excepciones;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Mvc;
using LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.Dtos.DtoUsuario;
using LogicaAplicacion.Dtos.EmpleadoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Libreria.LogicaAplicacion.CasosDeUso.CUUsuarios;
using LogicaNegocio.Excepciones;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly ICUAltaEmpleado _altaEmpleado;
        private readonly ICUObtenerEmpleados _obtenerEmpleados;
        private readonly ICUObtenerEmpleadoPorId _obtenerEmpleadoPorId;
        private readonly ICUActualizarEmpleado _actualizarEmpleado;
        private readonly ICUEliminarEmpleado _eliminarEmpleado;
        private readonly ICUBuscarEmpleadosPorNombre _buscarEmpleadosPorNombre;
        private readonly ICUAsignarHabilidadEmpleado _asignarHabilidadEmpleado;
        private readonly ICUQuitarHabilidadEmpleado _quitarHabilidadEmpleado;
        private readonly ICUObtenerHabilidadesDeEmpleado _obtenerHabilidadesDeEmpleado;
        private readonly ICUAsignarSectorEmpleado _asignarSectorEmpleado;
        private readonly ICUQuitarSectorEmpleado _quitarSectorEmpleado;
        private readonly ICUObtenerSectoresDeEmpleado _obtenerSectoresDeEmpleado;

        public EmpleadoController(
            ICUAltaEmpleado altaEmpleado,
            ICUObtenerEmpleados obtenerEmpleados,
            ICUObtenerEmpleadoPorId obtenerEmpleadoPorId,
            ICUActualizarEmpleado actualizarEmpleado,
            ICUEliminarEmpleado eliminarEmpleado,
            ICUBuscarEmpleadosPorNombre buscarEmpleadosPorNombre,
            ICUAsignarHabilidadEmpleado asignarHabilidadEmpleado,
            ICUQuitarHabilidadEmpleado quitarHabilidadEmpleado,
            ICUObtenerHabilidadesDeEmpleado obtenerHabilidadesDeEmpleado,
            ICUAsignarSectorEmpleado asignarSectorEmpleado,
            ICUQuitarSectorEmpleado quitarSectorEmpleado,
            ICUObtenerSectoresDeEmpleado obtenerSectoresDeEmpleado)
        {
            _altaEmpleado = altaEmpleado;
            _obtenerEmpleados = obtenerEmpleados;
            _obtenerEmpleadoPorId = obtenerEmpleadoPorId;
            _actualizarEmpleado = actualizarEmpleado;
            _eliminarEmpleado = eliminarEmpleado;
            _buscarEmpleadosPorNombre = buscarEmpleadosPorNombre;
            _asignarHabilidadEmpleado = asignarHabilidadEmpleado;
            _quitarHabilidadEmpleado = quitarHabilidadEmpleado;
            _obtenerHabilidadesDeEmpleado = obtenerHabilidadesDeEmpleado;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var empleados = _obtenerEmpleados.Ejecutar();
            return Ok(empleados);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var empleado = _obtenerEmpleadoPorId.Ejecutar(id);
                return Ok(empleado);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] AltaEmpleadoDTO dto)
        {
            try
            {
                _altaEmpleado.Ejecutar(dto);
                return Ok("Empleado creado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ActualizarEmpleadoDTO dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest(new { error = "El id de la URL no coincide con el del cuerpo." });

                _actualizarEmpleado.Ejecutar(dto);
                return Ok("Empleado actualizado correctamente.");
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
                _eliminarEmpleado.Ejecutar(id);
                return Ok("Empleado eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpGet("buscar/{texto}")]
        public IActionResult Buscar(string texto)
        {
            var empleados = _buscarEmpleadosPorNombre.Ejecutar(texto);
            return Ok(empleados);
        }

        [HttpPost("{empleadoId}/habilidades/{habilidadId}")]
        public IActionResult AsignarHabilidad(int empleadoId, int habilidadId)
        {
            try
            {
                _asignarHabilidadEmpleado.Ejecutar(new EmpleadoHabilidadDTO { EmpleadoId = empleadoId, HabilidadId = habilidadId });
                return Ok("Habilidad asignada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{empleadoId}/habilidades/{habilidadId}")]
        public IActionResult QuitarHabilidad(int empleadoId, int habilidadId)
        {
            try
            {
                _quitarHabilidadEmpleado.Ejecutar(new EmpleadoHabilidadDTO { EmpleadoId = empleadoId, HabilidadId = habilidadId });
                return Ok("Habilidad quitada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{empleadoId}/habilidades")]
        public IActionResult GetHabilidades(int empleadoId)
        {
            try
            {
                var habilidades = _obtenerHabilidadesDeEmpleado.Ejecutar(empleadoId);
                return Ok(habilidades);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}

