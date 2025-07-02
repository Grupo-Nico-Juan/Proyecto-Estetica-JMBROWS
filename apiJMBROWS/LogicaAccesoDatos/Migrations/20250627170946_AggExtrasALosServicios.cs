using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AggExtrasALosServicios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtrasServicio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DuracionMinutos = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServicioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtrasServicio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtrasServicio_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleTurnoExtra",
                columns: table => new
                {
                    DetalleTurnoId = table.Column<int>(type: "int", nullable: false),
                    ExtrasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleTurnoExtra", x => new { x.DetalleTurnoId, x.ExtrasId });
                    table.ForeignKey(
                        name: "FK_DetalleTurnoExtra_DetallesTurno_DetalleTurnoId",
                        column: x => x.DetalleTurnoId,
                        principalTable: "DetallesTurno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleTurnoExtra_ExtrasServicio_ExtrasId",
                        column: x => x.ExtrasId,
                        principalTable: "ExtrasServicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleTurnoExtra_ExtrasId",
                table: "DetalleTurnoExtra",
                column: "ExtrasId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtrasServicio_ServicioId",
                table: "ExtrasServicio",
                column: "ServicioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleTurnoExtra");

            migrationBuilder.DropTable(
                name: "ExtrasServicio");
        }
    }
}
