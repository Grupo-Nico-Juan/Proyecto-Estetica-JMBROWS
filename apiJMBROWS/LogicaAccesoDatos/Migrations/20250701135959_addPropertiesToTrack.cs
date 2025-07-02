using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class addPropertiesToTrack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SucursalId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SectorId",
                table: "Turnos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SucursalId",
                table: "Turnos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_SucursalId",
                table: "Usuarios",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_SectorId",
                table: "Turnos",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_SucursalId",
                table: "Turnos",
                column: "SucursalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Sectores_SectorId",
                table: "Turnos",
                column: "SectorId",
                principalTable: "Sectores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Sucursales_SucursalId",
                table: "Turnos",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Sucursales_SucursalId",
                table: "Usuarios",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Sectores_SectorId",
                table: "Turnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Sucursales_SucursalId",
                table: "Turnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Sucursales_SucursalId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_SucursalId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_SectorId",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_SucursalId",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "SucursalId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "SucursalId",
                table: "Turnos");
        }
    }
}
