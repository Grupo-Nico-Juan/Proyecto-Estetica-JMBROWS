using Libreria.LogicaAplicacion.CasosDeUso.CUUsuarios;
using Libreria.LogicaNegocio.Excepciones;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.CasosDeUso.CUEmpleado;
using LogicaAplicacion.Dtos.DtoUsuario;
using LogicaAplicacion.Dtos.EmpleadoDTO;
using LogicaAplicacion.Dtos.EmpleadoDTO.EmpleadoDispibleDTO;
using LogicaAplicacion.Dtos.HabilidadDTO;
using LogicaAplicacion.Dtos.SectorDTO;
using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaAplicacion.InterfacesCasosDeUso.ICUTurno;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requiere autenticación para todos los endpoints por defecto
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
        private readonly ICUObtenerEmpleadasDisponibles _obtenerDisponibles;
        private readonly ICUObtenerEmpleadoPorHabilidad _obtenerEmpleadoPorHabilidad;
        private readonly ICUObtenerTurnosDelDiaPorEmpleada _cuObtenerTurnosDelDia;
        private readonly ICUObtenerEmpleadasPorSector _obtenerEmpleadasPorSector;
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
            ICUObtenerSectoresDeEmpleado obtenerSectoresDeEmpleado,
            ICUObtenerEmpleadasDisponibles obtenerDisponibles,
            ICUObtenerEmpleadoPorHabilidad obtenerEmpleadoPorHabilidad,
            ICUObtenerTurnosDelDiaPorEmpleada ObtenerTurnosDelDia,
            ICUObtenerEmpleadasPorSector obtenerEmpleadasPorSector)
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
            _asignarSectorEmpleado = asignarSectorEmpleado;
            _quitarSectorEmpleado = quitarSectorEmpleado;
            _obtenerSectoresDeEmpleado = obtenerSectoresDeEmpleado;
            _obtenerDisponibles = obtenerDisponibles;
            _obtenerEmpleadoPorHabilidad = obtenerEmpleadoPorHabilidad;
            _cuObtenerTurnosDelDia = ObtenerTurnosDelDia;
            _obtenerEmpleadasPorSector = obtenerEmpleadasPorSector;

        }

        /// <summary>
        /// Obtiene todos los empleados.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obtiene todos los empleados")]
        [SwaggerResponse(200, "Lista de empleados", typeof(IEnumerable<EmpleadoDTO>))]
        public IActionResult Get()
        {
            var empleados = _obtenerEmpleados.Ejecutar();
            return Ok(empleados);
        }
        /// <summary>
        /// Obtiene la lista de empleadas disponibles que pueden realizar los servicios indicados.
        /// </summary>
        [HttpPost("disponibles")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Empleadas disponibles para servicios")]
        [SwaggerResponse(200, "Lista de empleadas válidas", typeof(List<EmpleadaDisponibleDTO>))]
        public IActionResult GetEmpleadasDisponibles([FromBody] ConsultaEmpleadasDisponiblesDTO dto)
        {
            try
            {
                var result = _obtenerDisponibles.Ejecutar(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene un empleado por su ID.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un empleado por ID")]
        [AllowAnonymous]
        [SwaggerResponse(200, "Empleado encontrado", typeof(EmpleadoDTO))]
        [SwaggerResponse(404, "Empleado no encontrado")]
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

        /// <summary>
        /// Crea un nuevo empleado. Solo administradores.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Crea un nuevo empleado (solo administradores)")]
        [SwaggerResponse(200, "Empleado creado correctamente")]
        [SwaggerResponse(400, "Error en los datos del empleado")]
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

        /// <summary>
        /// Actualiza un empleado existente. Solo administradores.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Actualiza un empleado (solo administradores)")]
        [SwaggerResponse(200, "Empleado actualizado correctamente")]
        [SwaggerResponse(400, "Error en los datos del empleado")]
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

        /// <summary>
        /// Elimina un empleado por ID. Solo administradores.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Elimina un empleado (solo administradores)")]
        [SwaggerResponse(200, "Empleado eliminado correctamente")]
        [SwaggerResponse(404, "Empleado no encontrado")]
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

        /// <summary>
        /// Busca empleados por nombre o apellido.
        /// </summary>
        [HttpGet("buscar/{texto}")]
        [SwaggerOperation(Summary = "Busca empleados por nombre o apellido")]
        [SwaggerResponse(200, "Lista de empleados encontrados", typeof(IEnumerable<EmpleadoDTO>))]
        public IActionResult Buscar(string texto)
        {
            var empleados = _buscarEmpleadosPorNombre.Ejecutar(texto);
            return Ok(empleados);
        }

        /// <summary>
        /// Asigna una habilidad a un empleado. Solo administradores.
        /// </summary>
        [HttpPost("{empleadoId}/habilidades/{habilidadId}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Asigna una habilidad a un empleado (solo administradores)")]
        [SwaggerResponse(200, "Habilidad asignada correctamente")]
        [SwaggerResponse(400, "Error al asignar habilidad")]
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

        /// <summary>
        /// Quita una habilidad de un empleado. Solo administradores.
        /// </summary>
        [HttpDelete("{empleadoId}/habilidades/{habilidadId}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Quita una habilidad de un empleado (solo administradores)")]
        [SwaggerResponse(200, "Habilidad quitada correctamente")]
        [SwaggerResponse(400, "Error al quitar habilidad")]
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

        /// <summary>
        /// Obtiene las habilidades de un empleado.
        /// </summary>
        [HttpGet("{empleadoId}/habilidades")]
        [SwaggerOperation(Summary = "Obtiene las habilidades de un empleado")]
        [SwaggerResponse(200, "Lista de habilidades", typeof(IEnumerable<HabilidadDTO>))]
        [SwaggerResponse(404, "Empleado no encontrado")]    
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

        /// <summary>
        /// Asigna un sector a un empleado. Solo administradores.
        /// </summary>
        [HttpPost("{empleadoId}/sectores/{sectorId}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Asigna un sector a un empleado (solo administradores)")]
        [SwaggerResponse(200, "Sector asignado correctamente")]
        [SwaggerResponse(400, "Error al asignar sector")]
        public IActionResult AsignarSector(int empleadoId, int sectorId)
        {
            try
            {
                _asignarSectorEmpleado.Ejecutar(new EmpleadoSectorDTO { EmpleadoId = empleadoId, SectorId = sectorId });
                return Ok("Sector asignado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Quita un sector de un empleado. Solo administradores.
        /// </summary>
        [HttpDelete("{empleadoId}/sectores/{sectorId}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Quita un sector de un empleado (solo administradores)")]
        [SwaggerResponse(200, "Sector quitado correctamente")]
        [SwaggerResponse(400, "Error al quitar sector")]
        public IActionResult QuitarSector(int empleadoId, int sectorId)
        {
            try
            {
                _quitarSectorEmpleado.Ejecutar(new EmpleadoSectorDTO { EmpleadoId = empleadoId, SectorId = sectorId });
                return Ok("Sector quitado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene los sectores de un empleado.
        /// </summary>
        [HttpGet("{empleadoId}/sectores")]
        [SwaggerOperation(Summary = "Obtiene los sectores de un empleado")]
        [SwaggerResponse(200, "Lista de sectores", typeof(IEnumerable<SectorDTSSuc>))]
        [SwaggerResponse(404, "Empleado no encontrado")]
        public IActionResult GetSectores(int empleadoId)
        {
            try
            {
                var sectores = _obtenerSectoresDeEmpleado.Ejecutar(empleadoId);
                return Ok(sectores);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpGet("habilidad/{habilidadId}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obtiene empleados que tienen una habilidad específica")]
        public IActionResult GetPorHabilidad(int habilidadId)
        {
            var empleados = _obtenerEmpleadoPorHabilidad.Ejecutar(habilidadId);
            return Ok(empleados);
        }


        [HttpGet("{empleadoId}/turnos-del-dia")]
        [SwaggerOperation(Summary = "Obtiene los turnos del día de una empleada")]
        [AllowAnonymous]
        [SwaggerResponse(200, "Lista de turnos del día", typeof(IEnumerable<TurnoDTO>))]
        public IActionResult GetTurnosDelDia(int empleadoId)
        {
            try
            {
                var turnos = _cuObtenerTurnosDelDia.Ejecutar(empleadoId, DateTime.Today);
                return Ok(turnos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("sector/{sectorId}/empleadas")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obtiene las empleadas asignadas a un sector")]
        [SwaggerResponse(200, "Lista de empleadas", typeof(IEnumerable<EmpleadoDTO>))]
        public IActionResult GetEmpleadasPorSector(int sectorId)
        {
            try
            {
                var empleadas = _obtenerEmpleadasPorSector.Ejecutar(sectorId);
                return Ok(empleadas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}