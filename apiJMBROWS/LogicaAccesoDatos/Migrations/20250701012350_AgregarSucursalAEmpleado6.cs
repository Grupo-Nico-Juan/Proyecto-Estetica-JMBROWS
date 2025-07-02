using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarSucursalAEmpleado6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Sucursales_SucursalId",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "SucursalId",
                table: "Usuarios",
                newName: "SucursalIdRef");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_SucursalId",
                table: "Usuarios",
                newName: "IX_Usuarios_SucursalIdRef");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Sucursales_SucursalIdRef",
                table: "Usuarios",
                column: "SucursalIdRef",
                principalTable: "Sucursales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Sucursales_SucursalIdRef",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "SucursalIdRef",
                table: "Usuarios",
                newName: "SucursalId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_SucursalIdRef",
                table: "Usuarios",
                newName: "IX_Usuarios_SucursalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Sucursales_SucursalId",
                table: "Usuarios",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id");
        }
    }
}
