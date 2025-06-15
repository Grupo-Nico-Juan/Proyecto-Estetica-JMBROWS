using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class PeriodoLaboralCRUD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeriodoLaboral_Usuarios_EmpleadaId",
                table: "PeriodoLaboral");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PeriodoLaboral",
                table: "PeriodoLaboral");

            migrationBuilder.RenameTable(
                name: "PeriodoLaboral",
                newName: "PeriodosLaborales");

            migrationBuilder.RenameIndex(
                name: "IX_PeriodoLaboral_EmpleadaId",
                table: "PeriodosLaborales",
                newName: "IX_PeriodosLaborales_EmpleadaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeriodosLaborales",
                table: "PeriodosLaborales",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodosLaborales_Usuarios_EmpleadaId",
                table: "PeriodosLaborales",
                column: "EmpleadaId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeriodosLaborales_Usuarios_EmpleadaId",
                table: "PeriodosLaborales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PeriodosLaborales",
                table: "PeriodosLaborales");

            migrationBuilder.RenameTable(
                name: "PeriodosLaborales",
                newName: "PeriodoLaboral");

            migrationBuilder.RenameIndex(
                name: "IX_PeriodosLaborales_EmpleadaId",
                table: "PeriodoLaboral",
                newName: "IX_PeriodoLaboral_EmpleadaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeriodoLaboral",
                table: "PeriodoLaboral",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodoLaboral_Usuarios_EmpleadaId",
                table: "PeriodoLaboral",
                column: "EmpleadaId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
