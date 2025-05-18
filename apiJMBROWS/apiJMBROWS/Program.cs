
using Libreria.LogicaAplicacion.CasosDeUso.CUUsuarios;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAccesoDatos.EF;
using LogicaAccesoDatos.Repositorios;
using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace apiJMBROWS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Servicios MVC/Web API
            builder.Services.AddControllers();
            //Repositorios
            builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuarios>();
            //Casos de uso
            builder.Services.AddScoped<ICUAltaUsuario, CUAltaUsuario>();
            // Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API de Est�tica JMBROWS",
                    Version = "v1",
                    Description = "API REST para la gesti�n de citas y notificaciones",
                });
            });

            // DbContext
            builder.Services.AddDbContext<EsteticaContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "JMBROWS API v1");
                    c.RoutePrefix = "swagger";
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
