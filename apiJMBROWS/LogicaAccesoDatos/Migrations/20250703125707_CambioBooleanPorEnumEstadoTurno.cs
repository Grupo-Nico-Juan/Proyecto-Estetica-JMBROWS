using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class CambioBooleanPorEnumEstadoTurno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cancelado",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "Realizado",
                table: "Turnos");

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Turnos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Turnos");

            migrationBuilder.AddColumn<bool>(
                name: "Cancelado",
                table: "Turnos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Realizado",
                table: "Turnos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
