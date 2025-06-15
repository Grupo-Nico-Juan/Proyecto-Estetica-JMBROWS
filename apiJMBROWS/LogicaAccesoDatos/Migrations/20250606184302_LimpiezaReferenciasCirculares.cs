using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class LimpiezaReferenciasCirculares : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmpleadoHabilidad_Usuarios_EmpleadasId",
                table: "EmpleadoHabilidad");

            migrationBuilder.DropForeignKey(
                name: "FK_EmpleadoSector_Usuarios_EmpleadasId",
                table: "EmpleadoSector");

            migrationBuilder.DropForeignKey(
                name: "FK_HabilidadServicio_Servicios_ServiciosId",
                table: "HabilidadServicio");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Sectores");

            migrationBuilder.RenameColumn(
                name: "ServiciosId",
                table: "HabilidadServicio",
                newName: "ServicioId");

            migrationBuilder.RenameIndex(
                name: "IX_HabilidadServicio_ServiciosId",
                table: "HabilidadServicio",
                newName: "IX_HabilidadServicio_ServicioId");

            migrationBuilder.RenameColumn(
                name: "EmpleadasId",
                table: "EmpleadoSector",
                newName: "EmpleadoId");

            migrationBuilder.RenameColumn(
                name: "EmpleadasId",
                table: "EmpleadoHabilidad",
                newName: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmpleadoHabilidad_Usuarios_EmpleadoId",
                table: "EmpleadoHabilidad",
                column: "EmpleadoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmpleadoSector_Usuarios_EmpleadoId",
                table: "EmpleadoSector",
                column: "EmpleadoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HabilidadServicio_Servicios_ServicioId",
                table: "HabilidadServicio",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmpleadoHabilidad_Usuarios_EmpleadoId",
                table: "EmpleadoHabilidad");

            migrationBuilder.DropForeignKey(
                name: "FK_EmpleadoSector_Usuarios_EmpleadoId",
                table: "EmpleadoSector");

            migrationBuilder.DropForeignKey(
                name: "FK_HabilidadServicio_Servicios_ServicioId",
                table: "HabilidadServicio");

            migrationBuilder.RenameColumn(
                name: "ServicioId",
                table: "HabilidadServicio",
                newName: "ServiciosId");

            migrationBuilder.RenameIndex(
                name: "IX_HabilidadServicio_ServicioId",
                table: "HabilidadServicio",
                newName: "IX_HabilidadServicio_ServiciosId");

            migrationBuilder.RenameColumn(
                name: "EmpleadoId",
                table: "EmpleadoSector",
                newName: "EmpleadasId");

            migrationBuilder.RenameColumn(
                name: "EmpleadoId",
                table: "EmpleadoHabilidad",
                newName: "EmpleadasId");

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Sectores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_EmpleadoHabilidad_Usuarios_EmpleadasId",
                table: "EmpleadoHabilidad",
                column: "EmpleadasId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmpleadoSector_Usuarios_EmpleadasId",
                table: "EmpleadoSector",
                column: "EmpleadasId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HabilidadServicio_Servicios_ServiciosId",
                table: "HabilidadServicio",
                column: "ServiciosId",
                principalTable: "Servicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
