using LogicaAplicacion.Dtos.WhatsappDTO;
using LogicaAplicacion.Infraestructura.ServiciosExternos;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WhatsAppController : ControllerBase
    {
        private readonly IWhatsAppService _ws;

        public WhatsAppController(IWhatsAppService ws) => _ws = ws;

        [HttpPost("verificar")]
        public async Task<IActionResult> EnviarCodigo([FromBody] WhatsAppVerificacionDTO dto)
        {
            try
            {
                await _ws.EnviarCodigoAsync(dto.ClienteId, dto.TelefonoDestino);
                return Ok();
            }
            catch (WhatsAppApiException ex)
            {
                return StatusCode((int)ex.StatusCode,
                    new { error = "WhatsApp API", detail = ex.Body });
            }
        }


        [HttpPost("confirmar")]
        public async Task<IActionResult> Confirmar([FromBody] VerificarCodigoDTO dto)
        {
            var ok = await _ws.VerificarCodigoAsync(dto.ClienteId, dto.Codigo);
            return ok ? Ok() : Unauthorized("Código incorrecto o expirado.");
        }
    }

}
