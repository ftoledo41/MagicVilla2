using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AgregarNumeroVillaTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumeroVillas",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    DetalleEspecial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeroVillas", x => x.VillaNo);
                    table.ForeignKey(
                        name: "FK_NumeroVillas_villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 5, 31, 16, 35, 34, 634, DateTimeKind.Local).AddTicks(3797), new DateTime(2023, 5, 31, 16, 35, 34, 634, DateTimeKind.Local).AddTicks(3850) });

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 5, 31, 16, 35, 34, 634, DateTimeKind.Local).AddTicks(3853), new DateTime(2023, 5, 31, 16, 35, 34, 634, DateTimeKind.Local).AddTicks(3854) });

            migrationBuilder.CreateIndex(
                name: "IX_NumeroVillas_VillaId",
                table: "NumeroVillas",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumeroVillas");

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 5, 31, 8, 44, 10, 391, DateTimeKind.Local).AddTicks(3328), new DateTime(2023, 5, 31, 8, 44, 10, 391, DateTimeKind.Local).AddTicks(3386) });

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 5, 31, 8, 44, 10, 391, DateTimeKind.Local).AddTicks(3388), new DateTime(2023, 5, 31, 8, 44, 10, 391, DateTimeKind.Local).AddTicks(3390) });
        }
    }
}
