using LogicaAplicacion.Dtos.WhatsappDTO;
using LogicaAplicacion.Infraestructura.ServiciosExternos;
using Microsoft.AspNetCore.Mvc;

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
        /// Enviar mensaje de verificación de cliente (solo para pruebas)
        /// </summary>
        [HttpPost("verificacion")]
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
    }
}
