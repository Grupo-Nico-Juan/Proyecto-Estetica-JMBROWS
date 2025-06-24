using LogicaAplicacion.Dtos.HabilidadDTO;
using LogicaAplicacion.Dtos.SectorDTO;
using LogicaAplicacion.Dtos.ServicioDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServicioController : ControllerBase
    {
        private readonly ICUAltaServicio _altaServicio;
        private readonly ICUActualizarServicio _actualizarServicio;
        private readonly ICUEliminarServicio _eliminarServicio;
        private readonly ICUObtenerServicios _obtenerServicios;
        private readonly ICUObtenerServicioPorId _obtenerServicioPorId;
        private readonly ICUBuscarServiciosPorNombre _buscarServiciosPorNombre;
        private readonly ICUAsignarSectorAServicio _asignarSector;
        private readonly ICUQuitarSectorDeServicio _quitarSector;
        private readonly ICUAsignarHabilidadAServicio _asignarHabilidad;
        private readonly ICUQuitarHabilidadDeServicio _quitarHabilidad;
        private readonly ICUObtenerHabilidadesServicio _obtenerHabilidadesServicio;
        private readonly ICUObtenerSectoresServicio _obtenerSectoresServicio;
        public ServicioController(
            ICUAltaServicio altaServicio,
            ICUActualizarServicio actualizarServicio,
            ICUEliminarServicio eliminarServicio,
            ICUObtenerServicios obtenerServicios,
            ICUObtenerServicioPorId obtenerServicioPorId,
            ICUBuscarServiciosPorNombre buscarServiciosPorNombre,
            ICUAsignarSectorAServicio asignarSector,
            ICUQuitarSectorDeServicio quitarSector,
            ICUAsignarHabilidadAServicio asignarHabilidad,
            ICUQuitarHabilidadDeServicio quitarHabilidad,
            ICUObtenerHabilidadesServicio obtenerHabilidadesServicio,
            ICUObtenerSectoresServicio obtenerSectoresServicio)
        {
            _altaServicio = altaServicio;
            _actualizarServicio = actualizarServicio;
            _eliminarServicio = eliminarServicio;
            _obtenerServicios = obtenerServicios;
            _obtenerServicioPorId = obtenerServicioPorId;
            _buscarServiciosPorNombre = buscarServiciosPorNombre;
            _asignarSector = asignarSector;
            _quitarSector = quitarSector;
            _asignarHabilidad = asignarHabilidad;
            _quitarHabilidad = quitarHabilidad;
            _obtenerHabilidadesServicio = obtenerHabilidadesServicio;
            _obtenerSectoresServicio = obtenerSectoresServicio;
        }

        /// <summary>
        /// Obtiene todos los servicios.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obtiene todos los servicios")]
        [SwaggerResponse(200, "Lista de servicios", typeof(IEnumerable<ServicioDTO>))]
        public IActionResult Get()
        {
            var servicios = _obtenerServicios.Ejecutar();
            return Ok(servicios);
        }

        /// <summary>
        /// Obtiene un servicio por su ID.
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obtiene un servicio por ID")]
        [SwaggerResponse(200, "Servicio encontrado", typeof(ServicioDTO))]
        [SwaggerResponse(404, "Servicio no encontrado")]
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

        /// <summary>
        /// Crea un nuevo servicio. Solo administradores.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Crea un nuevo servicio (solo administradores)")]
        [SwaggerResponse(200, "Servicio creado correctamente")]
        [SwaggerResponse(400, "Error en los datos del servicio")]
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

        /// <summary>
        /// Actualiza un servicio existente. Solo administradores.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Actualiza un servicio (solo administradores)")]
        [SwaggerResponse(200, "Servicio actualizado correctamente")]
        [SwaggerResponse(400, "Error en los datos del servicio")]
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

        /// <summary>
        /// Elimina un servicio por ID. Solo administradores.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Elimina un servicio (solo administradores)")]
        [SwaggerResponse(200, "Servicio eliminado correctamente")]
        [SwaggerResponse(404, "Servicio no encontrado")]
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

        /// <summary>
        /// Busca servicios por nombre.
        /// </summary>
        [HttpGet("buscar/{texto}")]
        [SwaggerOperation(Summary = "Busca servicios por nombre")]
        [SwaggerResponse(200, "Lista de servicios encontrados", typeof(IEnumerable<ServicioDTO>))]
        public IActionResult Buscar(string texto)
        {
            var servicios = _buscarServiciosPorNombre.Ejecutar(texto);
            return Ok(servicios);
        }
        /// <summary>
        /// Asigna un sector a un servicio.
        /// </summary>
        [HttpPost("{servicioId}/sectores/{sectorId}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Asigna un sector a un servicio")]
        [SwaggerResponse(200, "Sector quitado correctamente")]
        [SwaggerResponse(400, "Error al quitar sector")]

        public IActionResult AsignarSector(int servicioId, int sectorId)
        {
            try
            {
                _asignarSector.Ejecutar(new SectorAServicioDTO
                {
                    ServicioId = servicioId,
                    SectorId = sectorId
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Quita un sector de un servicio.
        /// </summary>
        [HttpDelete("{servicioId}/sectores/{sectorId}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Quita un sector de un servicio")]
        [SwaggerResponse(200, "Lista de sectores", typeof(IEnumerable<SectorDTSSuc>))]
        [SwaggerResponse(404, "Empleado no encontrado")]
        public IActionResult QuitarSector(int servicioId, int sectorId)
        {
            try
            {
                _quitarSector.Ejecutar(new SectorAServicioDTO
                {
                    ServicioId = servicioId,
                    SectorId = sectorId
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        /// <summary>
        /// Asigna una habilidad a un servicio.
        /// </summary>
        [HttpPost("{servicioId}/habilidades/{habilidadId}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Asigna una habilidad a un servicio")]
        [SwaggerResponse(200, "Habilidad asignada correctamente")]
        [SwaggerResponse(400, "Error al asignar habilidad")]
        public IActionResult AsignarHabilidad(int servicioId, int habilidadId)
        {
            try
            {
                _asignarHabilidad.Ejecutar(new HabilidadAServicioDTO
                {
                    ServicioId = servicioId,
                    HabilidadId = habilidadId
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Quita una habilidad de un servicio.
        /// </summary>
        [HttpDelete("{servicioId}/habilidades/{habilidadId}")]
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Quita una habilidad de un servicio")]
        [SwaggerResponse(200, "Habilidad quitada correctamente")]
        [SwaggerResponse(400, "Error al quitar habilidad")]
        public IActionResult QuitarHabilidad(int servicioId, int habilidadId)
        {
            try
            {
                _quitarHabilidad.Ejecutar(new HabilidadAServicioDTO
                {
                    ServicioId = servicioId,
                    HabilidadId = habilidadId
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// 
        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Obtiene las habilidades de un servicio")]
        [SwaggerResponse(200, "Lista de habilidades", typeof(IEnumerable<HabilidadDTO>))]
        [SwaggerResponse(404, "Empleado no encontrado")]
        public IActionResult GetHabilidades(int servicioId)
        {
            try
            {
                var result = _obtenerHabilidadesServicio.Ejecutar(servicioId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(new { error = e.Message });
            }
        }


        [Authorize(Roles = "Administrador")]
        [SwaggerOperation(Summary = "Quita una habilidad de un servicio")]
        [HttpGet("{servicioId}/sectores")]
        [SwaggerResponse(200, "Lista de sectores", typeof(IEnumerable<SectorDTSSuc>))]
        [SwaggerResponse(404, "Empleado no encontrado")]
        public IActionResult GetSectores(int servicioId)
        {
            try
            {
                var result = _obtenerSectoresServicio.Ejecutar(servicioId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(new { error = e.Message });
            }
        }


    }
}