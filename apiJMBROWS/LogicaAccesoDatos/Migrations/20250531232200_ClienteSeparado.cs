using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class ClienteSeparado : Migration
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
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "Servicios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DuracionMinutos = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sucursales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Eliminada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HabilidadServicio",
                columns: table => new
                {
                    HabilidadesId = table.Column<int>(type: "int", nullable: false),
                    ServiciosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabilidadServicio", x => new { x.HabilidadesId, x.ServiciosId });
                    table.ForeignKey(
                        name: "FK_HabilidadServicio_Habilidades_HabilidadesId",
                        column: x => x.HabilidadesId,
                        principalTable: "Habilidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HabilidadServicio_Servicios_ServiciosId",
                        column: x => x.ServiciosId,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sectores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SucursalId = table.Column<int>(type: "int", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
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
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoUsuario = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "EmpleadoHabilidad",
                columns: table => new
                {
                    EmpleadasId = table.Column<int>(type: "int", nullable: false),
                    HabilidadesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoHabilidad", x => new { x.EmpleadasId, x.HabilidadesId });
                    table.ForeignKey(
                        name: "FK_EmpleadoHabilidad_Habilidades_HabilidadesId",
                        column: x => x.HabilidadesId,
                        principalTable: "Habilidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpleadoHabilidad_Usuarios_EmpleadasId",
                        column: x => x.EmpleadasId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoSector",
                columns: table => new
                {
                    EmpleadasId = table.Column<int>(type: "int", nullable: false),
                    SectoresAsignadosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoSector", x => new { x.EmpleadasId, x.SectoresAsignadosId });
                    table.ForeignKey(
                        name: "FK_EmpleadoSector_Sectores_SectoresAsignadosId",
                        column: x => x.SectoresAsignadosId,
                        principalTable: "Sectores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpleadoSector_Usuarios_EmpleadasId",
                        column: x => x.EmpleadasId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeriodoLaboral",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadaId = table.Column<int>(type: "int", nullable: false),
                    Desde = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hasta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EsLicencia = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodoLaboral", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeriodoLaboral_Usuarios_EmpleadaId",
                        column: x => x.EmpleadaId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Realizado = table.Column<bool>(type: "bit", nullable: false),
                    Cancelado = table.Column<bool>(type: "bit", nullable: false)
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
                        name: "FK_Turnos_Usuarios_EmpleadaId",
                        column: x => x.EmpleadaId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalleTurno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurnoId = table.Column<int>(type: "int", nullable: false),
                    ServicioId = table.Column<int>(type: "int", nullable: false),
                    DuracionMinutos = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleTurno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleTurno_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleTurno_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleTurno_ServicioId",
                table: "DetalleTurno",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleTurno_TurnoId",
                table: "DetalleTurno",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoHabilidad_HabilidadesId",
                table: "EmpleadoHabilidad",
                column: "HabilidadesId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoSector_SectoresAsignadosId",
                table: "EmpleadoSector",
                column: "SectoresAsignadosId");

            migrationBuilder.CreateIndex(
                name: "IX_HabilidadServicio_ServiciosId",
                table: "HabilidadServicio",
                column: "ServiciosId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodoLaboral_EmpleadaId",
                table: "PeriodoLaboral",
                column: "EmpleadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Sectores_SucursalId",
                table: "Sectores",
                column: "SucursalId");

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
                name: "IX_Usuarios_SucursalId",
                table: "Usuarios",
                column: "SucursalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleTurno");

            migrationBuilder.DropTable(
                name: "EmpleadoHabilidad");

            migrationBuilder.DropTable(
                name: "EmpleadoSector");

            migrationBuilder.DropTable(
                name: "HabilidadServicio");

            migrationBuilder.DropTable(
                name: "PeriodoLaboral");

            migrationBuilder.DropTable(
                name: "ServicioSector");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Habilidades");

            migrationBuilder.DropTable(
                name: "Sectores");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Sucursales");
        }
    }
}
