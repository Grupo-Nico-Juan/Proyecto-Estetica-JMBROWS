using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class NombreDeLaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleTurno_Servicios_ServicioId",
                table: "DetalleTurno");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleTurno_Turnos_TurnoId",
                table: "DetalleTurno");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetalleTurno",
                table: "DetalleTurno");

            migrationBuilder.RenameTable(
                name: "DetalleTurno",
                newName: "DetalleTurnos");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleTurno_TurnoId",
                table: "DetalleTurnos",
                newName: "IX_DetalleTurnos_TurnoId");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleTurno_ServicioId",
                table: "DetalleTurnos",
                newName: "IX_DetalleTurnos_ServicioId");

            migrationBuilder.AddColumn<int>(
                name: "PromocionId",
                table: "Servicios",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetalleTurnos",
                table: "DetalleTurnos",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_PromocionId",
                table: "Servicios",
                column: "PromocionId");

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
                name: "IX_Promocion_ClienteId",
                table: "Promocion",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleTurnos_Servicios_ServicioId",
                table: "DetalleTurnos",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleTurnos_Turnos_TurnoId",
                table: "DetalleTurnos",
                column: "TurnoId",
                principalTable: "Turnos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Promocion_PromocionId",
                table: "Servicios",
                column: "PromocionId",
                principalTable: "Promocion",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleTurnos_Servicios_ServicioId",
                table: "DetalleTurnos");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleTurnos_Turnos_TurnoId",
                table: "DetalleTurnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Promocion_PromocionId",
                table: "Servicios");

            migrationBuilder.DropTable(
                name: "Notificacion");

            migrationBuilder.DropTable(
                name: "Promocion");

            migrationBuilder.DropIndex(
                name: "IX_Servicios_PromocionId",
                table: "Servicios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetalleTurnos",
                table: "DetalleTurnos");

            migrationBuilder.DropColumn(
                name: "PromocionId",
                table: "Servicios");

            migrationBuilder.RenameTable(
                name: "DetalleTurnos",
                newName: "DetalleTurno");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleTurnos_TurnoId",
                table: "DetalleTurno",
                newName: "IX_DetalleTurno_TurnoId");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleTurnos_ServicioId",
                table: "DetalleTurno",
                newName: "IX_DetalleTurno_ServicioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetalleTurno",
                table: "DetalleTurno",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleTurno_Servicios_ServicioId",
                table: "DetalleTurno",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleTurno_Turnos_TurnoId",
                table: "DetalleTurno",
                column: "TurnoId",
                principalTable: "Turnos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
