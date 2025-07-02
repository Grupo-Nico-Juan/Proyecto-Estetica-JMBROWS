using LogicaAplicacion.Dtos.WhatsappDTO;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LogicaAplicacion.Infraestructura.ServiciosExternos
{
    public class WhatsAppService
    {
        private readonly HttpClient _httpClient;
        private readonly string _token;
        private readonly string _telefonoRemitente;
        private readonly string _urlApi;
        private readonly IMemoryCache _cache;

        public WhatsAppService(IConfiguration config, IMemoryCache cache)
        {
            _httpClient = new HttpClient();
            _token = config["WhatsApp:Token"]!;
            _telefonoRemitente = config["WhatsApp:PhoneNumberId"]!;
            _urlApi = $"https://graph.facebook.com/v19.0/{_telefonoRemitente}/messages";
            _cache = cache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task EnviarMensajeVerificacionAsync(WhatsAppVerificacionDTO dto)
        {
            // 1. Generar un código de 6 dígitos
            var codigo = new Random().Next(100000, 999999).ToString();

            // 2. Guardar temporalmente en memoria (10 minutos)
            _cache.Set($"verif_{dto.ClienteId}", codigo, TimeSpan.FromMinutes(10));

            // 3. Armar el mensaje con el código y el botón
            var mensaje = new
            {
                messaging_product = "whatsapp",
                to = dto.TelefonoDestino,
                type = "template",
                template = new
                {
                    name = "verificacion_preaprobada",
                    language = new { code = "es" },
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

            // 4. Enviar el mensaje
            var request = new HttpRequestMessage(HttpMethod.Post, _urlApi);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            request.Content = new StringContent(JsonSerializer.Serialize(mensaje), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al enviar mensaje de WhatsApp: {error}");
            }
        }


        public bool VerificarCodigo(int clienteId, string codigoIngresado)
        {
            if (_cache.TryGetValue($"verif_{clienteId}", out string? codigoGuardado))
            {
                if (codigoGuardado == codigoIngresado)
                {
                    _cache.Remove($"verif_{clienteId}"); // Limpia el código luego de usarlo
                    return true;
                }
            }
            return false;
        }

    }
}
