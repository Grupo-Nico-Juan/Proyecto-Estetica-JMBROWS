
using Libreria.LogicaAplicacion.CasosDeUso.CUUsuarios;
using LogicaAccesoDatos.EF;
using LogicaAccesoDatos.Repositorios;
using LogicaAplicacion.InterfacesCasosDeUso;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using LogicaNegocio.Excepciones.Middleware;
using LogicaNegocio.InterfacesRepositorio;
using Libreria.LogicaNegocio.InterfacesRepositorio;

namespace apiJMBROWS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 🔧 (reemplaza 8080 si querés otro)
            builder.WebHost.UseUrls("http://*:8080");

            // Servicios MVC/Web API
            builder.Services.AddControllers();
            //Repositorios
            builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuarios>();
            builder.Services.AddScoped<IRepositorioSucursales, RepositorioSucursales>();
            builder.Services.AddScoped<IRepositorioServicios, RepositorioServicios>();
            builder.Services.AddScoped<IRepositorioHabilidades, RepositorioHabilidades>();
            builder.Services.AddScoped<IRepositorioClientes, RepositorioClientes>();
            builder.Services.AddScoped<IRepositorioTurnos, RepositorioTurnos>();



            //Casos de uso
            builder.Services.AddScoped<ICUAltaUsuario, CUAltaUsuario>();
            builder.Services.AddScoped<ICULoginUsuario, CULoginUsuario>();
            builder.Services.AddScoped<ICUAltaCliente, CUAltaCliente>();
            builder.Services.AddScoped<ICULoginCliente, CULoginCliente>();

            //CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost5173", policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            // Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API de Estética JMBROWS",
                    Version = "v1",
                    Description = "API REST para la gestión de citas y notificaciones",
                });
            });
            builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            builder.Services.AddAuthorization();

            // DbContext
            builder.Services.AddDbContext<EsteticaContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Habilitar Swagger en todos los entornos (solo mientras estás desarrollando o testeando)
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "JMBROWS API v1");
                c.RoutePrefix = "swagger"; // Deja esto si querés que se acceda con /swagger
            });

            app.UseCors("AllowLocalhost5173");
            app.UsarManejadorErrores();
            app.UseAuthorization();
            app.UseAuthentication();
            app.MapControllers();
            app.Run();

        }
    }
}
