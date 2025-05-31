using Libreria.LogicaNegocio.Excepciones;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace LogicaNegocio.Excepciones.Middleware
{
    public class ManejadorErroresMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ManejadorErroresMiddleware> _logger;

        public ManejadorErroresMiddleware(RequestDelegate next, ILogger<ManejadorErroresMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // Ejecutar siguiente middleware o controlador
            }
            catch (ExcepcionReglaDeNegocio ex)
            {
                _logger.LogWarning(ex, "Regla de negocio violada");
                await ManejarExcepcionAsync(context, ex, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado");
                await ManejarExcepcionAsync(context, ex, HttpStatusCode.InternalServerError);
            }
        }

        private async Task ManejarExcepcionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var respuesta = new
            {
                error = exception.Message,
                codigo = context.Response.StatusCode
            };

            var json = JsonSerializer.Serialize(respuesta);
            await context.Response.WriteAsync(json);
        }
    }
}
