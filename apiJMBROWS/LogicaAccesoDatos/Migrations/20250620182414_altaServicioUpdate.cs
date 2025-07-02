using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class altaServicioUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicioSector_Sectores_SectorId",
                table: "ServicioSector");

            migrationBuilder.RenameColumn(
                name: "SectorId",
                table: "ServicioSector",
                newName: "SectoresId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicioSector_Sectores_SectoresId",
                table: "ServicioSector",
                column: "SectoresId",
                principalTable: "Sectores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicioSector_Sectores_SectoresId",
                table: "ServicioSector");

            migrationBuilder.RenameColumn(
                name: "SectoresId",
                table: "ServicioSector",
                newName: "SectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicioSector_Sectores_SectorId",
                table: "ServicioSector",
                column: "SectorId",
                principalTable: "Sectores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
