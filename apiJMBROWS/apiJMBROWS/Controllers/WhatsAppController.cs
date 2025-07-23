using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.Dtos.WhatsappDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUTurno;
using LogicaNegocio.Entidades.Enums;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WhatsAppController : ControllerBase
    {
        private readonly IWhatsAppService _ws;
        private readonly string _verifyToken;
        private readonly ICUActualizarTurno _cuTurno;

        public WhatsAppController(IWhatsAppService ws, ICUActualizarTurno cuTurno, IConfiguration cfg)
        {
            _ws = ws;
            _cuTurno = cuTurno;
            _verifyToken = cfg["WhatsApp:WebhookVerifyToken"] ?? throw new ArgumentNullException(nameof(cfg), "El token de verificación de WhatsApp no puede ser nulo.");
        }

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

        [HttpGet("webhook")]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult VerifyWebhook(
        [FromQuery(Name = "hub.mode")] string mode,
        [FromQuery(Name = "hub.challenge")] string challenge,
        [FromQuery(Name = "hub.verify_token")] string verifyToken)
        {
            if (mode == "subscribe" && verifyToken == _verifyToken)
                return Ok(challenge);          // ✅ devuelve solo el reto
            return Unauthorized();
        }


        [HttpPost("webhook")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ReceiveWebhook([FromBody] JsonElement body)
        {
            // Solo procesamos mensajes interactivos
            try
            {
                var msgType = body.GetProperty("entry")[0]
                                  .GetProperty("changes")[0]
                                  .GetProperty("value")
                                  .GetProperty("messages")[0]
                                  .GetProperty("type")
                                  .GetString();

                if (msgType != "interactive") return Ok();

                var message = body.GetProperty("entry")[0]
                                   .GetProperty("changes")[0]
                                   .GetProperty("value")
                                   .GetProperty("messages")[0];

                var actionId = message.GetProperty("interactive")
                                      .GetProperty("button_reply")
                                      .GetProperty("id")
                                      .GetString();                    // CONFIRMAR / CANCELAR / …

                var ctxId = message.GetProperty("context")
                               .GetProperty("id")
                               .GetString();                      // “TURNO_123”

                // ─── Aplicar cambio de estado ───
                if (ctxId?.StartsWith("TURNO_") == true &&
                int.TryParse(ctxId.Split('_')[1], out int turnoId))
                {
                    var nuevoEstado = actionId switch
                    {
                        "CONFIRMAR" => EstadoTurno.Confirmado,
                        "CANCELAR" => EstadoTurno.Cancelado,
                        _ => (EstadoTurno?)null
                    }
                ;

                    if (nuevoEstado.HasValue)
                    {
                        _cuTurno.Ejecutar(new ActualizarTurnoDTO
                        {
                            Id = turnoId,
                            Estado = nuevoEstado
                        });
                    }
                }

                return Ok("EVENT_RECEIVED");
            }
            catch
            {
                return Ok();   // Respondemos 200 siempre para evitar reintentos infinitos
            }
        }
    }

}
