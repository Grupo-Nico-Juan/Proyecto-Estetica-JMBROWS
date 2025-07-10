using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class removeSectorIdForTurnos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Sectores_SectorId",
                table: "Turnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Sucursales_SucursalId",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_SectorId",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "Turnos");

            migrationBuilder.AlterColumn<int>(
                name: "SucursalId",
                table: "Turnos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Sucursales_SucursalId",
                table: "Turnos",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Sucursales_SucursalId",
                table: "Turnos");

            migrationBuilder.AlterColumn<int>(
                name: "SucursalId",
                table: "Turnos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SectorId",
                table: "Turnos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_SectorId",
                table: "Turnos",
                column: "SectorId");

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
        }
    }
}
