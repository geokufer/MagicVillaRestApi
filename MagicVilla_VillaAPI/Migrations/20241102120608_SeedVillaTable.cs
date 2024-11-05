using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Description", "ImageUrl", "Name", "Occupancy", "Rate", "SquareMeters", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Villa 1 Amenity", new DateTime(2024, 11, 2, 13, 6, 7, 801, DateTimeKind.Local).AddTicks(5226), "Villa 1 Description", "https://via.placeholder.com/150", "Villa 1", 4, 100.0, 200, new DateTime(2024, 11, 2, 13, 6, 7, 801, DateTimeKind.Local).AddTicks(5282) },
                    { 2, "Villa 2 Amenity", new DateTime(2024, 11, 2, 13, 6, 7, 801, DateTimeKind.Local).AddTicks(5286), "Villa 2 Description", "https://via.placeholder.com/150", "Villa 2", 6, 200.0, 300, new DateTime(2024, 11, 2, 13, 6, 7, 801, DateTimeKind.Local).AddTicks(5287) },
                    { 3, "Villa 3 Amenity", new DateTime(2024, 11, 2, 13, 6, 7, 801, DateTimeKind.Local).AddTicks(5290), "Villa 3 Description", "https://via.placeholder.com/150", "Villa 3", 8, 300.0, 400, new DateTime(2024, 11, 2, 13, 6, 7, 801, DateTimeKind.Local).AddTicks(5291) },
                    { 4, "Villa 4 Amenity", new DateTime(2024, 11, 2, 13, 6, 7, 801, DateTimeKind.Local).AddTicks(5294), "Villa 4 Description", "https://via.placeholder.com/150", "Villa 4", 10, 400.0, 500, new DateTime(2024, 11, 2, 13, 6, 7, 801, DateTimeKind.Local).AddTicks(5295) },
                    { 5, "Villa 5 Amenity", new DateTime(2024, 11, 2, 13, 6, 7, 801, DateTimeKind.Local).AddTicks(5298), "Villa 5 Description", "https://via.placeholder.com/150", "Villa 5", 12, 500.0, 600, new DateTime(2024, 11, 2, 13, 6, 7, 801, DateTimeKind.Local).AddTicks(5299) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
