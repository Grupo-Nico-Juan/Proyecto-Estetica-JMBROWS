using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class seActualizoElPeriodolaboralYValidaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsLicencia",
                table: "PeriodosLaborales");

            migrationBuilder.DropColumn(
                name: "DuracionMinutos",
                table: "DetallesTurno");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "DetallesTurno");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Hasta",
                table: "PeriodosLaborales",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Desde",
                table: "PeriodosLaborales",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "DiaSemana",
                table: "PeriodosLaborales",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraFin",
                table: "PeriodosLaborales",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraInicio",
                table: "PeriodosLaborales",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "PeriodosLaborales",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaSemana",
                table: "PeriodosLaborales");

            migrationBuilder.DropColumn(
                name: "HoraFin",
                table: "PeriodosLaborales");

            migrationBuilder.DropColumn(
                name: "HoraInicio",
                table: "PeriodosLaborales");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "PeriodosLaborales");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Hasta",
                table: "PeriodosLaborales",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Desde",
                table: "PeriodosLaborales",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EsLicencia",
                table: "PeriodosLaborales",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DuracionMinutos",
                table: "DetallesTurno",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "DetallesTurno",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
