using LogicaAplicacion.Dtos.WhatsappDTO;
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

        public WhatsAppService(IConfiguration config)
        {
            _httpClient = new HttpClient();
            _token = config["WhatsApp:Token"]!;
            _telefonoRemitente = config["WhatsApp:PhoneNumberId"]!;
            _urlApi = $"https://graph.facebook.com/v19.0/{_telefonoRemitente}/messages";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task EnviarMensajeVerificacionAsync(WhatsAppVerificacionDTO dto)
        {
            var mensaje = new
            {
                messaging_product = "whatsapp",
                to = dto.TelefonoDestino,
                type = "template",
                template = new
                {
                    name = "hello_world",
                    language = new { code = "en_US" }
                }
            };

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
    }
}
