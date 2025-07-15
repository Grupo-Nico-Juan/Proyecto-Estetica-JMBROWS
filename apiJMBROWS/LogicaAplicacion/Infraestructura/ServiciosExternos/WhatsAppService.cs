using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using LogicaAplicacion.Infraestructura.ServiciosExternos;
using LogicaNegocio.Excepciones;
using Libreria.LogicaNegocio.Excepciones;
using LogicaAplicacion.Infraestructura.Helpers;

public interface IWhatsAppService
{
    Task EnviarCodigoAsync(int clienteId, string telefono);
    Task<bool> VerificarCodigoAsync(int clienteId, string codigo);
}

public class WhatsAppService : IWhatsAppService
{
    private readonly IHttpClientFactory _httpFactory;
    private readonly WhatsAppSettings _cfg;
    private readonly IDistributedCache _cache;
    private readonly ILogger<WhatsAppService> _log;

    public WhatsAppService(
        IHttpClientFactory httpFactory,
        IOptions<WhatsAppSettings> opt,
        IDistributedCache cache,
        ILogger<WhatsAppService> log)
    {
        _httpFactory = httpFactory;
        _cfg = opt.Value;
        _cache = cache;
        _log = log;
    }

    public async Task EnviarCodigoAsync(int clienteId, string telefono)
    {
        // ─── Normalizar el teléfono (09xxxxxxx → +598xxxxxxxx) ───
        try
        {
            telefono = UruguayPhoneHelper.Normalizar(telefono);
        }
        catch (ArgumentException ex)
        {
            throw new UsuarioException(ex.Message);
        }

        // ─── Generar y cachear código ───
        string codigo = RandomNumberGenerator
            .GetInt32(100_000, 999_999)
            .ToString("D6");

        await _cache.SetStringAsync($"verif_{clienteId}", codigo,
            new DistributedCacheEntryOptions
            { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) });

        // ─── Payload WhatsApp (plantilla verify en en_US) ───
        var mensaje = new
        {
            messaging_product = "whatsapp",
            to = telefono,
            type = "template",
            template = new
            {
                name = "verify",
                language = new { code = "en_US" },
                components = new object[]
                    {
                new
                {
                    type = "body",
                    parameters = new[]
                    {
                        new { type = "text", text = codigo }
                    }
                },
                new
                {
                    type = "button",
                    sub_type = "url",
                    index = 0,
                    parameters = new[]
                    {
                        new { type = "text", text = codigo }
                    }
                }
                    }
            }
        };

        using var client = _httpFactory.CreateClient("WhatsApp");
        var request = new HttpRequestMessage(
            HttpMethod.Post,
            $"{_cfg.PhoneNumberId}/messages")
        {
            Content = JsonContent.Create(mensaje)
        };
        request.Headers.Authorization = new("Bearer", _cfg.Token);

        var rsp = await client.SendAsync(request);

        if (!rsp.IsSuccessStatusCode)
        {
            string body = await rsp.Content.ReadAsStringAsync();
            _log.LogWarning("WhatsApp error {Status}: {Body}", rsp.StatusCode, body);
            throw new WhatsAppApiException(rsp.StatusCode, body);
        }
    }

    public async Task<bool> VerificarCodigoAsync(int clienteId, string codigoIngresado)
    {
        var codigoGuardado = await _cache.GetStringAsync($"verif_{clienteId}");
        bool ok = codigoGuardado == codigoIngresado;
        if (ok) await _cache.RemoveAsync($"verif_{clienteId}");
        return ok;
    }
}
