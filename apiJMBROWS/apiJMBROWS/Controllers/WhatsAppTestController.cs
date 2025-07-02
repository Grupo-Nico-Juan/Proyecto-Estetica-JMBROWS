using LogicaAplicacion.Dtos.WhatsappDTO;
using LogicaAplicacion.Infraestructura.ServiciosExternos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WhatsAppTestController : ControllerBase
    {
        private readonly WhatsAppService _whatsAppService;

        public WhatsAppTestController(WhatsAppService whatsAppService)
        {
            _whatsAppService = whatsAppService;
        }

        /// <summary>
        /// Envía un mensaje de verificación de WhatsApp al cliente.
        /// </summary>
        [HttpPost("verificacion")]
        [SwaggerOperation(Summary = "Envía un mensaje de verificación al cliente.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Mensaje enviado correctamente.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Error al enviar el mensaje.")]
        public async Task<IActionResult> EnviarMensaje([FromBody] WhatsAppVerificacionDTO dto)
        {
            try
            {
                await _whatsAppService.EnviarMensajeVerificacionAsync(dto);
                return Ok("Mensaje enviado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Verifica el código de WhatsApp ingresado por el cliente.
        /// </summary>
        [HttpPost("verificar-codigo")]
        [SwaggerOperation(Summary = "Verifica el código de WhatsApp del cliente.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Código verificado correctamente.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Código incorrecto o expirado.")]
        public IActionResult VerificarCodigo([FromBody] VerificarCodigoDTO dto)
        {
            bool ok = _whatsAppService.VerificarCodigo(dto.ClienteId, dto.Codigo);
            if (!ok) return BadRequest("Código incorrecto o expirado.");


            return Ok("Código verificado correctamente.");
        }

    }
}
