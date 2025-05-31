using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones.Middleware
{
    public static class ManejadorErroresMiddlewareExtension
    {
        public static IApplicationBuilder UsarManejadorErrores(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ManejadorErroresMiddleware>();
        }
    }
}
