using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class RefactorServicioSinSectores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicioSector_Sectores_SectoresId",
                table: "ServicioSector");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Sucursales_SucursalId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_SucursalId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "SucursalId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Eliminada",
                table: "Sucursales");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Servicios");

            migrationBuilder.RenameColumn(
                name: "SectoresId",
                table: "ServicioSector",
                newName: "SectorId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Servicios",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "DetalleTurnos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicioSector_Sectores_SectorId",
                table: "ServicioSector",
                column: "SectorId",
                principalTable: "Sectores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicioSector_Sectores_SectorId",
                table: "ServicioSector");

            migrationBuilder.RenameColumn(
                name: "SectorId",
                table: "ServicioSector",
                newName: "SectoresId");

            migrationBuilder.AddColumn<int>(
                name: "SucursalId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminada",
                table: "Sucursales",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Servicios",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Servicios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "DetalleTurnos",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_SucursalId",
                table: "Usuarios",
                column: "SucursalId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicioSector_Sectores_SectoresId",
                table: "ServicioSector",
                column: "SectoresId",
                principalTable: "Sectores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Sucursales_SucursalId",
                table: "Usuarios",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id");
        }
    }
}
