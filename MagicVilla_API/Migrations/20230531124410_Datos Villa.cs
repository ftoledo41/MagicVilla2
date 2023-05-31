using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class DatosVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "villas",
                columns: new[] { "Id", "Amenidad", "Details", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Name", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detalle de la villa...", new DateTime(2023, 5, 31, 8, 44, 10, 391, DateTimeKind.Local).AddTicks(3328), new DateTime(2023, 5, 31, 8, 44, 10, 391, DateTimeKind.Local).AddTicks(3386), "", 5, "Villa Real", 5, 200.0 },
                    { 2, "", "Detalle de la villa...", new DateTime(2023, 5, 31, 8, 44, 10, 391, DateTimeKind.Local).AddTicks(3388), new DateTime(2023, 5, 31, 8, 44, 10, 391, DateTimeKind.Local).AddTicks(3390), "", 10, "Villa Real Premiun", 10, 500.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
