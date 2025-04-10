using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WhaleSpottingBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddPostedByIdAndSeedDataChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Species",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Species",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Species",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Species",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Species",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Species",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Species",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Species",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Species",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Species",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Species",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.AddColumn<string>(
                name: "PostedById",
                table: "Sighting",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sighting_PostedById",
                table: "Sighting",
                column: "PostedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Sighting_AspNetUsers_PostedById",
                table: "Sighting",
                column: "PostedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sighting_AspNetUsers_PostedById",
                table: "Sighting");

            migrationBuilder.DropIndex(
                name: "IX_Sighting_PostedById",
                table: "Sighting");

            migrationBuilder.DropColumn(
                name: "PostedById",
                table: "Sighting");

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "SpatialCoordinates" },
                values: new object[,]
                {
                    { 1, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (41.9028 -60)") },
                    { 2, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (57.808243 -146.412739)") },
                    { 3, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (34.468535 -130.063914)") },
                    { 4, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (73.149204 -148.475577)") },
                    { 5, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (36.547852 -72.010735)") },
                    { 6, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (52.122217 -50.741203)") },
                    { 7, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (69.169371 -174.813427)") },
                    { 8, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (55.443528 -138.983393)") },
                    { 9, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (44.420668 -56.366203)") },
                    { 10, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (83.249778 -106.815422)") }
                });

            migrationBuilder.InsertData(
                table: "Species",
                columns: new[] { "Id", "SpeciesName" },
                values: new object[,]
                {
                    { 1, "Blue Whale" },
                    { 2, "Humpback Whale" },
                    { 3, "Orca" },
                    { 4, "Sperm Whale" },
                    { 5, "Fin Whale" },
                    { 6, "Minke Whale" },
                    { 7, "Beluga Whale" },
                    { 8, "Gray Whale" },
                    { 9, "Right Whale" },
                    { 10, "Bowhead Whale" },
                    { 11, "Unknown" }
                });

            migrationBuilder.InsertData(
                table: "Sighting",
                columns: new[] { "Id", "Description", "ImageSource", "LocationId", "Quantity", "ReportDate", "SightingDate", "SpeciesId" },
                values: new object[,]
                {
                    { 1, "Details of Sighting 1", "http://localhost:5067/images/blue-whale.jpg", 1, 1, new DateTime(2024, 3, 2, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 1, 13, 21, 33, 0, DateTimeKind.Utc), 1 },
                    { 2, "Details of Sighting 2", "http://localhost:5067/images/orca-whale.jpg", 2, 1, new DateTime(2024, 3, 3, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 2, 13, 21, 33, 0, DateTimeKind.Utc), 2 },
                    { 3, "Details of Sighting 3", "http://localhost:5067/images/blue-whale.jpg", 3, 1, new DateTime(2024, 3, 4, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 3, 13, 21, 33, 0, DateTimeKind.Utc), 3 },
                    { 4, "Details of Sighting 4", "http://localhost:5067/images/orca-whale.jpg", 4, 1, new DateTime(2024, 3, 5, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 4, 13, 21, 33, 0, DateTimeKind.Utc), 4 },
                    { 5, "Details of Sighting 5", "http://localhost:5067/images/blue-whale.jpg", 5, 1, new DateTime(2024, 3, 6, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 5, 13, 21, 33, 0, DateTimeKind.Utc), 5 },
                    { 6, "Details of Sighting 6", "http://localhost:5067/images/orca-whale.jpg", 6, 1, new DateTime(2024, 3, 7, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 6, 13, 21, 33, 0, DateTimeKind.Utc), 6 },
                    { 7, "Details of Sighting 7", "http://localhost:5067/images/blue-whale.jpg", 7, 1, new DateTime(2024, 3, 8, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 7, 13, 21, 33, 0, DateTimeKind.Utc), 7 },
                    { 8, "Details of Sighting 8", "http://localhost:5067/images/orca-whale.jpg", 8, 1, new DateTime(2024, 3, 9, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 8, 13, 21, 33, 0, DateTimeKind.Utc), 8 },
                    { 9, "Details of Sighting 9", "http://localhost:5067/images/blue-whale.jpg", 9, 1, new DateTime(2024, 3, 10, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 9, 13, 21, 33, 0, DateTimeKind.Utc), 9 },
                    { 10, "Details of Sighting 10", "http://localhost:5067/images/orca-whale.jpg", 10, 1, new DateTime(2024, 3, 11, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 10, 13, 21, 33, 0, DateTimeKind.Utc), 10 },
                    { 11, "Details of Sighting 11", "http://localhost:5067/images/blue-whale.jpg", 1, 1, new DateTime(2024, 3, 12, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 11, 13, 21, 33, 0, DateTimeKind.Utc), 1 },
                    { 12, "Details of Sighting 12", "http://localhost:5067/images/orca-whale.jpg", 2, 1, new DateTime(2024, 3, 13, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 12, 13, 21, 33, 0, DateTimeKind.Utc), 2 },
                    { 13, "Details of Sighting 13", "http://localhost:5067/images/blue-whale.jpg", 3, 1, new DateTime(2024, 3, 14, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 13, 13, 21, 33, 0, DateTimeKind.Utc), 3 },
                    { 14, "Details of Sighting 14", "http://localhost:5067/images/orca-whale.jpg", 4, 1, new DateTime(2024, 3, 15, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 14, 13, 21, 33, 0, DateTimeKind.Utc), 4 },
                    { 15, "Details of Sighting 15", "http://localhost:5067/images/blue-whale.jpg", 5, 1, new DateTime(2024, 3, 16, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 15, 13, 21, 33, 0, DateTimeKind.Utc), 5 },
                    { 16, "Details of Sighting 16", "http://localhost:5067/images/orca-whale.jpg", 6, 1, new DateTime(2024, 3, 17, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 16, 13, 21, 33, 0, DateTimeKind.Utc), 6 },
                    { 17, "Details of Sighting 17", "http://localhost:5067/images/blue-whale.jpg", 7, 1, new DateTime(2024, 3, 18, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 17, 13, 21, 33, 0, DateTimeKind.Utc), 7 },
                    { 18, "Details of Sighting 18", "http://localhost:5067/images/orca-whale.jpg", 8, 1, new DateTime(2024, 3, 19, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 18, 13, 21, 33, 0, DateTimeKind.Utc), 8 },
                    { 19, "Details of Sighting 19", "http://localhost:5067/images/blue-whale.jpg", 9, 1, new DateTime(2024, 3, 20, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 19, 13, 21, 33, 0, DateTimeKind.Utc), 9 },
                    { 20, "Details of Sighting 20", "http://localhost:5067/images/orca-whale.jpg", 10, 1, new DateTime(2024, 3, 21, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 20, 13, 21, 33, 0, DateTimeKind.Utc), 10 }
                });
        }
    }
}
