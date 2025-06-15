using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class DetalleTurnoYRelaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleTurnos_Servicios_ServicioId",
                table: "DetalleTurnos");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleTurnos_Turnos_TurnoId",
                table: "DetalleTurnos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetalleTurnos",
                table: "DetalleTurnos");

            migrationBuilder.RenameTable(
                name: "DetalleTurnos",
                newName: "DetallesTurno");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleTurnos_TurnoId",
                table: "DetallesTurno",
                newName: "IX_DetallesTurno_TurnoId");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleTurnos_ServicioId",
                table: "DetallesTurno",
                newName: "IX_DetallesTurno_ServicioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesTurno",
                table: "DetallesTurno",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesTurno_Servicios_ServicioId",
                table: "DetallesTurno",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesTurno_Turnos_TurnoId",
                table: "DetallesTurno",
                column: "TurnoId",
                principalTable: "Turnos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesTurno_Servicios_ServicioId",
                table: "DetallesTurno");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesTurno_Turnos_TurnoId",
                table: "DetallesTurno");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesTurno",
                table: "DetallesTurno");

            migrationBuilder.RenameTable(
                name: "DetallesTurno",
                newName: "DetalleTurnos");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesTurno_TurnoId",
                table: "DetalleTurnos",
                newName: "IX_DetalleTurnos_TurnoId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesTurno_ServicioId",
                table: "DetalleTurnos",
                newName: "IX_DetalleTurnos_ServicioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetalleTurnos",
                table: "DetalleTurnos",
                column: "Id");

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
        }
    }
}
