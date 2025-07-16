using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class nuevaDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsRegistrado = table.Column<bool>(type: "bit", nullable: false),
                    TelefonoVerificado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Habilidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Eliminada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habilidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sucursales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promocion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PorcentajeDescuento = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activa = table.Column<bool>(type: "bit", nullable: false),
                    Eliminada = table.Column<bool>(type: "bit", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promocion_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sectores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SucursalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sectores_Sucursales_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "Sucursales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoUsuario = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SucursalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Sucursales_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "Sucursales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DuracionMinutos = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PromocionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servicios_Promocion_PromocionId",
                        column: x => x.PromocionId,
                        principalTable: "Promocion",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoHabilidad",
                columns: table => new
                {
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    HabilidadesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoHabilidad", x => new { x.EmpleadoId, x.HabilidadesId });
                    table.ForeignKey(
                        name: "FK_EmpleadoHabilidad_Habilidades_HabilidadesId",
                        column: x => x.HabilidadesId,
                        principalTable: "Habilidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpleadoHabilidad_Usuarios_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoSector",
                columns: table => new
                {
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    SectoresAsignadosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoSector", x => new { x.EmpleadoId, x.SectoresAsignadosId });
                    table.ForeignKey(
                        name: "FK_EmpleadoSector_Sectores_SectoresAsignadosId",
                        column: x => x.SectoresAsignadosId,
                        principalTable: "Sectores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpleadoSector_Usuarios_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeriodosLaborales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadaId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    DiaSemana = table.Column<int>(type: "int", nullable: true),
                    HoraInicio = table.Column<TimeSpan>(type: "time", nullable: true),
                    HoraFin = table.Column<TimeSpan>(type: "time", nullable: true),
                    Desde = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Hasta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodosLaborales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeriodosLaborales_Usuarios_EmpleadaId",
                        column: x => x.EmpleadaId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmpleadaId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    SucursalId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    HangfireId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turnos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turnos_Sucursales_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "Sucursales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turnos_Usuarios_EmpleadaId",
                        column: x => x.EmpleadaId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExtrasServicio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DuracionMinutos = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ServicioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtrasServicio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtrasServicio_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HabilidadServicio",
                columns: table => new
                {
                    HabilidadesId = table.Column<int>(type: "int", nullable: false),
                    ServicioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabilidadServicio", x => new { x.HabilidadesId, x.ServicioId });
                    table.ForeignKey(
                        name: "FK_HabilidadServicio_Habilidades_HabilidadesId",
                        column: x => x.HabilidadesId,
                        principalTable: "Habilidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HabilidadServicio_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicioSector",
                columns: table => new
                {
                    SectoresId = table.Column<int>(type: "int", nullable: false),
                    ServiciosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicioSector", x => new { x.SectoresId, x.ServiciosId });
                    table.ForeignKey(
                        name: "FK_ServicioSector_Sectores_SectoresId",
                        column: x => x.SectoresId,
                        principalTable: "Sectores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicioSector_Servicios_ServiciosId",
                        column: x => x.ServiciosId,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallesTurno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurnoId = table.Column<int>(type: "int", nullable: false),
                    ServicioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesTurno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesTurno_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesTurno_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Destinatario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Enviada = table.Column<bool>(type: "bit", nullable: false),
                    TurnoId = table.Column<int>(type: "int", nullable: true),
                    PromocionId = table.Column<int>(type: "int", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificacion_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notificacion_Promocion_PromocionId",
                        column: x => x.PromocionId,
                        principalTable: "Promocion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notificacion_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DetalleTurnoExtra",
                columns: table => new
                {
                    DetalleTurnoId = table.Column<int>(type: "int", nullable: false),
                    ExtrasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleTurnoExtra", x => new { x.DetalleTurnoId, x.ExtrasId });
                    table.ForeignKey(
                        name: "FK_DetalleTurnoExtra_DetallesTurno_DetalleTurnoId",
                        column: x => x.DetalleTurnoId,
                        principalTable: "DetallesTurno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleTurnoExtra_ExtrasServicio_ExtrasId",
                        column: x => x.ExtrasId,
                        principalTable: "ExtrasServicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Telefono",
                table: "Clientes",
                column: "Telefono",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallesTurno_ServicioId",
                table: "DetallesTurno",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesTurno_TurnoId",
                table: "DetallesTurno",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleTurnoExtra_ExtrasId",
                table: "DetalleTurnoExtra",
                column: "ExtrasId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoHabilidad_HabilidadesId",
                table: "EmpleadoHabilidad",
                column: "HabilidadesId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoSector_SectoresAsignadosId",
                table: "EmpleadoSector",
                column: "SectoresAsignadosId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtrasServicio_ServicioId",
                table: "ExtrasServicio",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_HabilidadServicio_ServicioId",
                table: "HabilidadServicio",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacion_ClienteId",
                table: "Notificacion",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacion_PromocionId",
                table: "Notificacion",
                column: "PromocionId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacion_TurnoId",
                table: "Notificacion",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodosLaborales_EmpleadaId",
                table: "PeriodosLaborales",
                column: "EmpleadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Promocion_ClienteId",
                table: "Promocion",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Sectores_SucursalId",
                table: "Sectores",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_PromocionId",
                table: "Servicios",
                column: "PromocionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioSector_ServiciosId",
                table: "ServicioSector",
                column: "ServiciosId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_ClienteId",
                table: "Turnos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_EmpleadaId",
                table: "Turnos",
                column: "EmpleadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_SucursalId",
                table: "Turnos",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_SucursalId",
                table: "Usuarios",
                column: "SucursalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleTurnoExtra");

            migrationBuilder.DropTable(
                name: "EmpleadoHabilidad");

            migrationBuilder.DropTable(
                name: "EmpleadoSector");

            migrationBuilder.DropTable(
                name: "HabilidadServicio");

            migrationBuilder.DropTable(
                name: "Notificacion");

            migrationBuilder.DropTable(
                name: "PeriodosLaborales");

            migrationBuilder.DropTable(
                name: "ServicioSector");

            migrationBuilder.DropTable(
                name: "DetallesTurno");

            migrationBuilder.DropTable(
                name: "ExtrasServicio");

            migrationBuilder.DropTable(
                name: "Habilidades");

            migrationBuilder.DropTable(
                name: "Sectores");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Promocion");

            migrationBuilder.DropTable(
                name: "Sucursales");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
