using Libreria.LogicaAplicacion.CasosDeUso.CUUsuarios;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAccesoDatos.EF;
using LogicaAccesoDatos.Repositorios;
using LogicaAplicacion.CasosDeUso.CUCliente;
using LogicaAplicacion.CasosDeUso.CUDetalleTurno;
using LogicaAplicacion.CasosDeUso.CUEmpleado;
using LogicaAplicacion.CasosDeUso.CUHabilidad;
using LogicaAplicacion.CasosDeUso.CUPeriodoLaboral;
using LogicaAplicacion.CasosDeUso.CUServicio;
using LogicaAplicacion.CasosDeUso.CUSucursal;
using LogicaAplicacion.CasosDeUso.CUTurno;
using LogicaAplicacion.InterfacesCasosDeUso;
using LogicaAplicacion.InterfacesCasosDeUso.ICUCliente;
using LogicaAplicacion.InterfacesCasosDeUso.ICUDetalleTurno;
using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaAplicacion.InterfacesCasosDeUso.ICUHabilidad;
using LogicaAplicacion.InterfacesCasosDeUso.ICUPeriodoLaboral;
using LogicaAplicacion.InterfacesCasosDeUso.ICUSector;
using LogicaAplicacion.InterfacesCasosDeUso.ICUServicio;
using LogicaAplicacion.InterfacesCasosDeUso.ICUSurcursal;
using LogicaAplicacion.InterfacesCasosDeUso.ICUTurno;
using LogicaNegocio.Excepciones.Middleware;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace apiJMBROWS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://*:8080");

            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
             options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
             });

            // Repositorios
            builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuarios>();
            builder.Services.AddScoped<IRepositorioSucursales, RepositorioSucursales>();
            builder.Services.AddScoped<IRepositorioServicios, RepositorioServicios>();
            builder.Services.AddScoped<IRepositorioHabilidades, RepositorioHabilidades>();
            builder.Services.AddScoped<IRepositorioTurnos, RepositorioTurnos>();
            builder.Services.AddScoped<IRepositorioClientes, RepositorioClientes>();
            builder.Services.AddScoped<IRepositorioSectores, RepositorioSectores>();
            builder.Services.AddScoped<IRepositorioDetalleTurno, RepositorioDetalleTurno>();
            builder.Services.AddScoped<IRepositorioPeriodoLaboral, RepositorioPeriodoLaboral>();

            // Casos de uso
            builder.Services.AddScoped<ICUAltaUsuario, CUAltaUsuario>();
            builder.Services.AddScoped<ICULoginUsuario, CULoginUsuario>();

            builder.Services.AddScoped<ICUAltaCliente, CUAltaCliente>();
            builder.Services.AddScoped<ICUObtenerClientePorTelefono, CUObtenerClientePorTelefono>();
            builder.Services.AddScoped<ICULoginCliente, CULoginCliente>();
            builder.Services.AddScoped<ICUGetClientes, CUGetClientes>();
            builder.Services.AddScoped<ICURegistrarClienteSinCuenta, CURegistrarClienteSinCuenta>();

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
            builder.Services.AddScoped<ICUAsignarSectorAServicio, CUAsignarSectorAServicio>();
            builder.Services.AddScoped<ICUQuitarSectorDeServicio, CUQuitarSectorDeServicio>();
            builder.Services.AddScoped<ICUAsignarHabilidadAServicio, CUAsignarHabilidadAServicio>();
            builder.Services.AddScoped<ICUQuitarHabilidadDeServicio, CUQuitarHabilidadDeServicio>();
            builder.Services.AddScoped<ICUObtenerSectoresServicio, CUObtenerSectoresServicio>();
            builder.Services.AddScoped<ICUObtenerHabilidadesServicio, CUObtenerHabilidadesServicio>();

            builder.Services.AddScoped<ICUAltaHabilidad, CUAltaHabilidad>();
            builder.Services.AddScoped<ICUActualizarHabilidad, CUActualizarHabilidad>();
            builder.Services.AddScoped<ICUEliminarHabilidad, CUEliminarHabilidad>();
            builder.Services.AddScoped<ICUObtenerHabilidadPorId, CUObtenerHabilidadPorId>();
            builder.Services.AddScoped<ICUBuscarHabilidadesPorNombre, CUBuscarHabilidadesPorNombre>();
            builder.Services.AddScoped<ICUObtenerHabilidades, CUObtenerHabilidades>();

            builder.Services.AddScoped<ICUAltaEmpleado, CUAltaEmpleado>();
            builder.Services.AddScoped<ICUObtenerEmpleados, CUObtenerEmpleados>();
            builder.Services.AddScoped<ICUObtenerEmpleadasDisponibles, CUObtenerEmpleadasDisponibles>();
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
            builder.Services.AddScoped<ICUObtenerEmpleadoPorHabilidad, CUObtenerEmpleadoPorHabilidad>();

            builder.Services.AddScoped<ICUActualizarTurno, CUActualizarTurno>();
            builder.Services.AddScoped<ICUEliminarTurno, CUEliminarTurno>();
            builder.Services.AddScoped<ICUAltaTurno, CUAltaTurno>();
            builder.Services.AddScoped<ICUObtenerTurnos, CUObtenerTurnos>();
            builder.Services.AddScoped<ICUObtenerTurnoPorId, CUObtenerTurnoPorId>();
            builder.Services.AddScoped<ICUObtenerTurnosPorEmpleada, CUObtenerTurnosPorEmpleada>();
            builder.Services.AddScoped<ICUObtenerTurnosDelDiaPorEmpleada, CUObtenerTurnosDelDiaPorEmpleada>();
            builder.Services.AddScoped<ICUObtenerHorariosDisponibles, CUObtenerHorariosDisponibles>();

            builder.Services.AddScoped<ICUAltaDetalleTurno, CUAltaDetalleTurno>();
            builder.Services.AddScoped<ICUActualizarDetalleTurno, CUActualizarDetalleTurno>();
            builder.Services.AddScoped<ICUEliminarDetalleTurno, CUEliminarDetalleTurno>();
            builder.Services.AddScoped<ICUObtenerDetalleTurnoPorId, CUObtenerDetalleTurnoPorId>();
            builder.Services.AddScoped<ICUObtenerDetallesTurno, CUObtenerDetallesTurno>();

            builder.Services.AddScoped<ICUObtenerPeriodosLaboralesPorEmpleada, CUObtenerPeriodosLaboralesPorEmpleada>();
            builder.Services.AddScoped<ICUAltaPeriodoLaboral, CUAltaPeriodoLaboral>();
            builder.Services.AddScoped<ICUModificarPeriodoLaboral, CUModificarPeriodoLaboral>();
            builder.Services.AddScoped<ICUEliminarPeriodoLaboral, CUEliminarPeriodoLaboral>();

            builder.Services.AddScoped<ICUAltaSector, CUAltaSector>();
            builder.Services.AddScoped<ICUActualizarSector, CUActualizarSector>();
            builder.Services.AddScoped<ICUEliminarSector, CUEliminarSector>();
            builder.Services.AddScoped<ICUObtenerSectorPorId, CUObtenerSectorPorId>();
            builder.Services.AddScoped<ICUObtenerSectores, CUObtenerSectores>();
            builder.Services.AddScoped<ICUObtenerSectoresPorSucursal, CUObtenerSectoresPorSucursal>();



            // CORS
            var allowedOrigins = new[] {
                "https://calm-tree-09940dd0f.6.azurestaticapps.net",
                "http://localhost:5173"
            };
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontendPolicy", policy =>
                {
                    policy.WithOrigins(allowedOrigins)
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

            builder.Services.AddDbContext<EsteticaContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // ✅ ORDEN CORRECTO DE MIDDLEWARES
            app.UseCors("FrontendPolicy");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "JMBROWS API v1");
                c.RoutePrefix = "swagger";
            });

            app.UsarManejadorErrores();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
