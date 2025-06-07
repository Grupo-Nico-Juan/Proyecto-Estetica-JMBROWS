using Libreria.LogicaAplicacion.CasosDeUso.CUUsuarios;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAccesoDatos.EF;
using LogicaAccesoDatos.Repositorios;
using LogicaAplicacion.CasosDeUso.CUEmpleado;
using LogicaAplicacion.CasosDeUso.CUHabilidad;
using LogicaAplicacion.CasosDeUso.CUServicio;
using LogicaAplicacion.CasosDeUso.CUSucursal;
using LogicaAplicacion.CasosDeUso.CUTurno;
using LogicaAplicacion.CasosDeUso.CUDetalleTurno;
using LogicaAplicacion.InterfacesCasosDeUso;
using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaAplicacion.InterfacesCasosDeUso.ICUHabilidad;
using LogicaAplicacion.InterfacesCasosDeUso.ICUServicio;
using LogicaAplicacion.InterfacesCasosDeUso.ICUSurcursal;
using LogicaAplicacion.InterfacesCasosDeUso.ICUTurno;
using LogicaAplicacion.InterfacesCasosDeUso.ICUDetalleTurno;
using LogicaNegocio.Excepciones.Middleware;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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
            builder.Services.AddScoped<IRepositorioDetalleTurno, RepositorioDetalleTurno>();

            //Casos de uso
            builder.Services.AddScoped<ICUAltaUsuario, CUAltaUsuario>();
            builder.Services.AddScoped<ICULoginUsuario, CULoginUsuario>();
            builder.Services.AddScoped<ICUAltaCliente, CUAltaCliente>();
            builder.Services.AddScoped<ICULoginCliente, CULoginCliente>();
            builder.Services.AddScoped<ICUModificarSucursal, CUModificarSucursal>();
            builder.Services.AddScoped<ICUAltaSucursal, CUAltaSucursal>();
            builder.Services.AddScoped<ICUObtenerSucursales, CUObtenerSucursales>();
            builder.Services.AddScoped<ICUObtenerSucursalPorId, CUObtenerSucursalPorId>();
            builder.Services.AddScoped<ICUEliminarSucursal, CUEliminarSucursal>();
            builder.Services.AddScoped<ICUAltaServicio, CUAltaServicio>();
            builder.Services.AddScoped<ICUActualizarServicio, CUActualizarServicio>();
            builder.Services.AddScoped<ICUEliminarServicio, CUEliminarServicio>();
            builder.Services.AddScoped<ICUObtenerServicios, CUObtenerServicios>();
            builder.Services.AddScoped<ICUObtenerServicioPorId, CUObtenerServicioPorId>();
            builder.Services.AddScoped<ICUBuscarServiciosPorNombre, CUBuscarServiciosPorNombre>();

            // Casos de uso de habilidades
            builder.Services.AddScoped<ICUAltaHabilidad, CUAltaHabilidad>();
            builder.Services.AddScoped<ICUActualizarHabilidad, CUActualizarHabilidad>();
            builder.Services.AddScoped<ICUEliminarHabilidad, CUEliminarHabilidad>();
            builder.Services.AddScoped<ICUObtenerHabilidadPorId, CUObtenerHabilidadPorId>();
            builder.Services.AddScoped<ICUBuscarHabilidadesPorNombre, CUBuscarHabilidadesPorNombre>();
            builder.Services.AddScoped<ICUObtenerHabilidades, CUObtenerHabilidades>();

            //Casos de uso Empleado
            builder.Services.AddScoped<ICUAltaEmpleado, CUAltaEmpleado>();
            builder.Services.AddScoped<ICUObtenerEmpleados, CUObtenerEmpleados>();
            builder.Services.AddScoped<ICUObtenerEmpleadoPorId, CUObtenerEmpleadoPorId>();
            builder.Services.AddScoped<ICUActualizarEmpleado, CUActualizarEmpleado>();
            builder.Services.AddScoped<ICUEliminarEmpleado, CUEliminarEmpleado>();
            builder.Services.AddScoped<ICUBuscarEmpleadosPorNombre, CUBuscarEmpleadosPorNombre>();
            builder.Services.AddScoped<ICUAsignarHabilidadEmpleado, CUAsignarHabilidadEmpleado>();
            builder.Services.AddScoped<ICUQuitarHabilidadEmpleado, CUQuitarHabilidadEmpleado>();
            builder.Services.AddScoped<ICUObtenerHabilidadesDeEmpleado, CUObtenerHabilidadesDeEmpleado>();
            builder.Services.AddScoped<ICUAsignarSectorEmpleado, CUAsignarSectorEmpleado>();
            builder.Services.AddScoped<ICUQuitarSectorEmpleado, CUQuitarSectorEmpleado>();
            builder.Services.AddScoped<ICUObtenerSectoresDeEmpleado, CUObtenerSectoresDeEmpleado>();
            //Casos de uso Turnos
            builder.Services.AddScoped<ICUActualizarTurno, CUActualizarTurno>();
            builder.Services.AddScoped<ICUEliminarTurno, CUEliminarTurno>();
            builder.Services.AddScoped<ICUAltaTurno, CUAltaTurno>();
            builder.Services.AddScoped<ICUObtenerTurnos, CUObtenerTurnos>();
            builder.Services.AddScoped<ICUObtenerTurnoPorId, CUObtenerTurnoPorId>();
            builder.Services.AddScoped<ICUObtenerTurnosPorEmpleada, CUObtenerTurnosPorEmpleada>();
            builder.Services.AddScoped<ICUObtenerTurnosDelDiaPorEmpleada, CUObtenerTurnosDelDiaPorEmpleada>();

            //Casos de uso DetalleTurno
            builder.Services.AddScoped<ICUAltaDetalleTurno, CUAltaDetalleTurno>();
            builder.Services.AddScoped<ICUActualizarDetalleTurno, CUActualizarDetalleTurno>();
            builder.Services.AddScoped<ICUEliminarDetalleTurno, CUEliminarDetalleTurno>();
            builder.Services.AddScoped<ICUObtenerDetalleTurnoPorId, CUObtenerDetalleTurnoPorId>();
            builder.Services.AddScoped<ICUObtenerDetallesTurno, CUObtenerDetallesTurno>();

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
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Ingrese: Bearer {su_token_jwt}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
                new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();

        }
    }
}
