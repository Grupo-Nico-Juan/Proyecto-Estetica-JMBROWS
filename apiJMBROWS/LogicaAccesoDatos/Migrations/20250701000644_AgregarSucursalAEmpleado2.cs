using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarSucursalAEmpleado2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Sucursales_SucursalId",
                table: "Usuarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Sucursales_SucursalId",
                table: "Usuarios",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Sucursales_SucursalId",
                table: "Usuarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Sucursales_SucursalId",
                table: "Usuarios",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
