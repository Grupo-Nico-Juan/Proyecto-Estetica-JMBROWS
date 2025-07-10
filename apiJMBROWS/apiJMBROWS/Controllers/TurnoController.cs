using LogicaAplicacion.CasosDeUso.CUDetalleTurno;
using LogicaAplicacion.CasosDeUso.CUTurno;
using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUDetalleTurno;
using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaAplicacion.InterfacesCasosDeUso.ICUTurno;
using LogicaNegocio.Entidades.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TurnosController : ControllerBase
    {
        private readonly ICUAltaTurno _altaTurno;
        private readonly ICUObtenerTurnos _obtenerTurnos;
        private readonly ICUObtenerTurnoPorId _obtenerTurnoPorId;
        private readonly ICUObtenerTurnosPorEmpleada _obtenerTurnosPorEmpleada;
        private readonly ICUObtenerTurnosDelDiaPorEmpleada _obtenerTurnosDelDiaPorEmpleada;
        private readonly ICUActualizarTurno _actualizarTurno;
        private readonly ICUEliminarTurno _eliminarTurno;
        private readonly ICUAltaDetalleTurno _altaDetalleTurno;
        private readonly ICUObtenerDetallesTurno _obtenerDetallesTurno;
        private readonly ICUActualizarDetalleTurno _actualizarDetalleTurno;
        private readonly ICUObtenerDetalleTurnoPorId _obtenerDetalleTurnoPorId;
        private readonly ICUEliminarDetalleTurno _eliminarDetalleTurno;
        private readonly ICUObtenerHorariosDisponibles _horariosDisponibles;
        private readonly ICUObtenerHorariosPorEmpleada _horariosPorEmpleada;
        private readonly ICUObtenerHorariosOcupados _horariosOcupados;
        private readonly ICUMarcarTurnoComoRealizado _marcarTurnoComoRealizado;
        private readonly ICUObtenerTurnosFiltrados _obtenerTurnosFiltrados;
        public TurnosController(
            ICUAltaTurno altaTurno,
            ICUObtenerTurnos obtenerTurnos,
            ICUObtenerTurnoPorId obtenerTurnoPorId,
            ICUObtenerTurnosPorEmpleada obtenerTurnosPorEmpleada,
            ICUObtenerTurnosDelDiaPorEmpleada obtenerTurnosDelDiaPorEmpleada,
            ICUActualizarTurno actualizarTurno,
            ICUEliminarTurno eliminarTurno,
            ICUAltaDetalleTurno altaDetalleTurno,
            ICUObtenerDetallesTurno obtenerDetallesTurno,
            ICUActualizarDetalleTurno actualizarDetalleTurno,
            ICUObtenerDetalleTurnoPorId obtenerDetalleTurnoPorId,
            ICUEliminarDetalleTurno eliminarDetalleTurno,
            ICUObtenerHorariosDisponibles horariosDisponibles,
            ICUObtenerHorariosPorEmpleada horariosPorEmpleada,
            ICUObtenerHorariosOcupados horariosOcupados,
            ICUMarcarTurnoComoRealizado marcarTurnoComoRealizado,
            ICUObtenerTurnosFiltrados obtenerTurnosFiltrados
            )
        {
            _altaTurno = altaTurno;
            _obtenerTurnos = obtenerTurnos;
            _obtenerTurnoPorId = obtenerTurnoPorId;
            _obtenerTurnosPorEmpleada = obtenerTurnosPorEmpleada;
            _obtenerTurnosDelDiaPorEmpleada = obtenerTurnosDelDiaPorEmpleada;
            _actualizarTurno = actualizarTurno;
            _eliminarTurno = eliminarTurno;
            _altaDetalleTurno = altaDetalleTurno;
            _obtenerDetallesTurno = obtenerDetallesTurno;
            _actualizarDetalleTurno = actualizarDetalleTurno;
            _obtenerDetalleTurnoPorId = obtenerDetalleTurnoPorId;
            _eliminarDetalleTurno = eliminarDetalleTurno;
            _horariosDisponibles = horariosDisponibles;
            _horariosPorEmpleada = horariosPorEmpleada;
            _horariosOcupados = horariosOcupados;
            _marcarTurnoComoRealizado = marcarTurnoComoRealizado;
            _obtenerTurnosFiltrados = obtenerTurnosFiltrados;
        }

        /// <summary>
        /// Obtiene todos los turnos.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obtiene todos los turnos")]
        [SwaggerResponse(200, "Lista de turnos", typeof(IEnumerable<TurnoDTO>))]
        public IActionResult GetAll()
        {
            var turnos = _obtenerTurnos.Ejecutar();
            return Ok(turnos);
        }

        [HttpGet("filtrar")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obtiene turnos con filtros opcionales")]
        [SwaggerResponse(200, "Lista de turnos filtrados para el calendario", typeof(List<TurnoCalendarioDTO>))]
        public IActionResult Filtrar(
            [FromQuery] int? empleadaId,
            [FromQuery] int? sectorId,
            [FromQuery] EstadoTurno? estado,
            [FromQuery] DateTime? fechaInicio,
            [FromQuery] DateTime? fechaFin
        )
        {
            var filtro = new TurnoFiltroDTO
            {
                EmpleadaId = empleadaId,
                Estado = estado,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            };

            var turnos = _obtenerTurnosFiltrados.Ejecutar(filtro);
            return Ok(turnos);
        }


        [HttpPost("{id}/agregar-detalle")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Agrega un detalle a un turno existente")]
        [SwaggerResponse(200, "Detalle agregado correctamente")]
        [SwaggerResponse(400, "Error en los datos")]
        public IActionResult AgregarDetalle(int id, [FromBody] AltaDetalleTurnoDTO dto)
        {
            try
            {
                if (dto.TurnoId != id)
                    return BadRequest(new { error = "El ID del turno en la URL no coincide con el del cuerpo." });

                _altaDetalleTurno.Ejecutar(dto);
                return Ok(new { mensaje = "Detalle agregado con éxito." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpGet("{id}/detalles")]
        [SwaggerOperation(Summary = "Obtiene los detalles de un turno")]
        [SwaggerResponse(200, "Lista de detalles", typeof(IEnumerable<DetalleTurnoDTO>))]
        public IActionResult ObtenerDetallesDeTurno(int id)
        {
            try
            {
                var detalles = _obtenerDetallesTurno.Ejecutar(id);
                return Ok(detalles);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
        [HttpPut("{turnoId}/detalle/{detalleId}")]
        [SwaggerOperation(Summary = "Actualiza un detalle de turno")]
        public IActionResult ActualizarDetalle(int turnoId, int detalleId, [FromBody] ActualizarDetalleTurnoDTO dto)
        {
            try
            {
                if (detalleId != dto.Id || turnoId != dto.TurnoId)
                    return BadRequest(new { error = "IDs no coinciden." });

                _actualizarDetalleTurno.Ejecutar(dto);
                return Ok("Detalle actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpDelete("{turnoId}/detalle/{detalleId}")]
        [SwaggerOperation(Summary = "Elimina un detalle de turno")]
        public IActionResult EliminarDetalle(int turnoId, int detalleId)
        {
            try
            {
                var detalle = _obtenerDetalleTurnoPorId.Ejecutar(detalleId);
                if (detalle.TurnoId != turnoId)
                    return BadRequest(new { error = "El detalle no pertenece al turno indicado." });

                _eliminarDetalleTurno.Ejecutar(detalleId);
                return Ok("Detalle eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }





        /// <summary>
        /// Obtiene un turno por su ID.
        /// </summary>
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obtiene un turno por ID")]
        [SwaggerResponse(200, "Turno encontrado", typeof(TurnoDTO))]
        [SwaggerResponse(404, "Turno no encontrado")]
        public IActionResult GetById(int id)
        {
            try
            {
                var turno = _obtenerTurnoPorId.Ejecutar(id);
                return Ok(turno);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Crea un nuevo turno. Solo administradores.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Crea un nuevo turno (solo administradores)")]
        [SwaggerResponse(201, "Turno creado correctamente")]
        [SwaggerResponse(400, "Error en los datos del turno")]
        public IActionResult Create([FromBody] AltaTurnoDTO dto)
        {
            try
            {
                _altaTurno.Ejecutar(dto);
                return StatusCode(201, "Turno creado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene los turnos de una empleada.
        /// </summary>
        [HttpGet("empleada/{empleadaId:int}")]
        [SwaggerOperation(Summary = "Obtiene los turnos de una empleada")]
        [SwaggerResponse(200, "Lista de turnos", typeof(IEnumerable<TurnoDTO>))]
        public IActionResult TurnosPorEmpleada(int empleadaId)
        {
            var lista = _obtenerTurnosPorEmpleada.Ejecutar(empleadaId);
            return Ok(lista);
        }

        /// <summary>
        /// Obtiene los turnos de hoy de una empleada.
        /// </summary>
        [HttpGet("empleada/{empleadaId:int}/hoy")]
        [SwaggerOperation(Summary = "Obtiene los turnos de hoy de una empleada")]
        [SwaggerResponse(200, "Lista de turnos de hoy", typeof(IEnumerable<TurnoDTO>))]
        public IActionResult TurnosHoy(int empleadaId)
        {
            var lista = _obtenerTurnosDelDiaPorEmpleada.Ejecutar(empleadaId, DateTimeOffset.UtcNow.Date);
            return Ok(lista);
        }

        /// <summary>
        /// Actualiza un turno existente. Solo administradores.
        /// </summary>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Actualiza un turno (solo administradores)")]
        [SwaggerResponse(200, "Turno actualizado correctamente")]
        [SwaggerResponse(400, "Error en los datos del turno")]
        [SwaggerResponse(404, "Turno no encontrado")]
        public IActionResult Update(int id, [FromBody] ActualizarTurnoDTO dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest(new { error = "El id de la URL no coincide con el del cuerpo." });

                _actualizarTurno.Ejecutar(dto);
                return Ok("Turno actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un turno por ID. Solo administradores.
        /// </summary>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Elimina un turno (solo administradores)")]
        [SwaggerResponse(200, "Turno eliminado correctamente")]
        [SwaggerResponse(404, "Turno no encontrado")]
        public IActionResult Delete(int id)
        {
            try
            {
                _eliminarTurno.Ejecutar(id);
                return Ok("Turno eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }


        /// <summary>
        /// Muestra los horarios disponibles dependiendo servicio, sucursal y empleadas disponibles
        /// </summary>
        [HttpPost("horarios-disponibles")]
        [AllowAnonymous] 
        [SwaggerOperation(Summary = "Obtiene horarios disponibles para uno o varios servicios")]
        [SwaggerResponse(200, "Lista de horarios con empleadas disponibles", typeof(List<HorarioDisponibleDTO>))]
        [SwaggerResponse(400, "Faltan datos necesarios")]
        [SwaggerResponse(500, "Error interno")]
        public ActionResult<List<HorarioDisponibleDTO>> ObtenerHorariosDisponibles([FromBody] HorariosDisponiblesFiltroDTO filtro)
        {
            try
            {
                if (filtro.ServicioIds == null || !filtro.ServicioIds.Any())
                    return BadRequest("Debe seleccionar al menos un servicio.");

                var horarios = _horariosDisponibles.Ejecutar(filtro);

                return Ok(horarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener horarios: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene los horarios disponibles para una empleada seleccionada
        /// </summary>
        [HttpPost("horarios-disponibles-empleada")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Horarios disponibles de una empleada")]
        [SwaggerResponse(200, "Lista de horarios disponibles", typeof(List<HorarioDisponibleDTO>))]
        public ActionResult<List<HorarioDisponibleDTO>> ObtenerHorariosPorEmpleada([FromBody] HorariosPorEmpleadaFiltroDTO filtro)
        {
            try
            {
                if (filtro.ServicioIds == null || !filtro.ServicioIds.Any())
                    return BadRequest("Debe seleccionar al menos un servicio.");

                var horarios = _horariosPorEmpleada.Ejecutar(filtro);
                return Ok(horarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener horarios: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene las franjas horarias sin disponibilidad
        /// </summary>
        [HttpPost("horarios-ocupados")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Horarios sin disponibilidad para la fecha seleccionada")]
        public ActionResult<List<HorarioOcupadoDTO>> ObtenerHorariosOcupados([FromBody] HorariosDisponiblesFiltroDTO filtro)
        {
            try
            {
                if (filtro.ServicioIds == null || !filtro.ServicioIds.Any())
                    return BadRequest("Debe seleccionar al menos un servicio.");

                var horarios = _horariosOcupados.Ejecutar(filtro);
                return Ok(horarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener horarios: {ex.Message}");
            }
        }


        [HttpPut("{id}/marcar-realizado")]
        [Authorize(Roles = "Administrador,Empleado")] 
        [SwaggerOperation(Summary = "Marca un turno como realizado mediante PIN")]
        [SwaggerResponse(200, "Turno marcado como realizado")]
        [SwaggerResponse(404, "Turno no encontrado")]
        public IActionResult MarcarRealizado(int id, [FromBody] RealizadoTurnoDTO dto)
        {
            try
            {
                _marcarTurnoComoRealizado.Ejecutar(id, dto.Pin);
                return Ok("Turno marcado como realizado.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }

}