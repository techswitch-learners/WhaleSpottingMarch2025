﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WhaleSpottingBackend.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "Latitude", "Longitude" },
                values: new object[,]
                {
                    { 1, 41.902799999999999, -60.0 },
                    { 2, 57.808242999999997, -146.41273899999999 },
                    { 3, 34.468535000000003, -130.06391400000001 },
                    { 4, 73.149203999999997, -148.47557699999999 },
                    { 5, 36.547851999999999, -72.010734999999997 },
                    { 6, 52.122216999999999, -50.741202999999999 },
                    { 7, 69.169370999999998, -174.81342699999999 },
                    { 8, 55.443528000000001, -138.98339300000001 },
                    { 9, 44.420667999999999, -56.366202999999999 },
                    { 10, 83.249778000000006, -106.815422 }
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
                    { 1, "Details of Sighting 1", "https://images.google.com/", 1, 1, new DateTime(2024, 3, 2, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 1, 13, 21, 33, 0, DateTimeKind.Utc), 1 },
                    { 2, "Details of Sighting 2", "https://images.google.com/", 2, 1, new DateTime(2024, 3, 3, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 2, 13, 21, 33, 0, DateTimeKind.Utc), 2 },
                    { 3, "Details of Sighting 3", "https://images.google.com/", 3, 1, new DateTime(2024, 3, 4, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 3, 13, 21, 33, 0, DateTimeKind.Utc), 3 },
                    { 4, "Details of Sighting 4", "https://images.google.com/", 4, 1, new DateTime(2024, 3, 5, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 4, 13, 21, 33, 0, DateTimeKind.Utc), 4 },
                    { 5, "Details of Sighting 5", "https://images.google.com/", 5, 1, new DateTime(2024, 3, 6, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 5, 13, 21, 33, 0, DateTimeKind.Utc), 5 },
                    { 6, "Details of Sighting 6", "https://images.google.com/", 6, 1, new DateTime(2024, 3, 7, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 6, 13, 21, 33, 0, DateTimeKind.Utc), 6 },
                    { 7, "Details of Sighting 7", "https://images.google.com/", 7, 1, new DateTime(2024, 3, 8, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 7, 13, 21, 33, 0, DateTimeKind.Utc), 7 },
                    { 8, "Details of Sighting 8", "https://images.google.com/", 8, 1, new DateTime(2024, 3, 9, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 8, 13, 21, 33, 0, DateTimeKind.Utc), 8 },
                    { 9, "Details of Sighting 9", "https://images.google.com/", 9, 1, new DateTime(2024, 3, 10, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 9, 13, 21, 33, 0, DateTimeKind.Utc), 9 },
                    { 10, "Details of Sighting 10", "https://images.google.com/", 10, 1, new DateTime(2024, 3, 11, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 10, 13, 21, 33, 0, DateTimeKind.Utc), 10 },
                    { 11, "Details of Sighting 11", "https://images.google.com/", 1, 1, new DateTime(2024, 3, 12, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 11, 13, 21, 33, 0, DateTimeKind.Utc), 1 },
                    { 12, "Details of Sighting 12", "https://images.google.com/", 2, 1, new DateTime(2024, 3, 13, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 12, 13, 21, 33, 0, DateTimeKind.Utc), 2 },
                    { 13, "Details of Sighting 13", "https://images.google.com/", 3, 1, new DateTime(2024, 3, 14, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 13, 13, 21, 33, 0, DateTimeKind.Utc), 3 },
                    { 14, "Details of Sighting 14", "https://images.google.com/", 4, 1, new DateTime(2024, 3, 15, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 14, 13, 21, 33, 0, DateTimeKind.Utc), 4 },
                    { 15, "Details of Sighting 15", "https://images.google.com/", 5, 1, new DateTime(2024, 3, 16, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 15, 13, 21, 33, 0, DateTimeKind.Utc), 5 },
                    { 16, "Details of Sighting 16", "https://images.google.com/", 6, 1, new DateTime(2024, 3, 17, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 16, 13, 21, 33, 0, DateTimeKind.Utc), 6 },
                    { 17, "Details of Sighting 17", "https://images.google.com/", 7, 1, new DateTime(2024, 3, 18, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 17, 13, 21, 33, 0, DateTimeKind.Utc), 7 },
                    { 18, "Details of Sighting 18", "https://images.google.com/", 8, 1, new DateTime(2024, 3, 19, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 18, 13, 21, 33, 0, DateTimeKind.Utc), 8 },
                    { 19, "Details of Sighting 19", "https://images.google.com/", 9, 1, new DateTime(2024, 3, 20, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 19, 13, 21, 33, 0, DateTimeKind.Utc), 9 },
                    { 20, "Details of Sighting 20", "https://images.google.com/", 10, 1, new DateTime(2024, 3, 21, 13, 21, 33, 0, DateTimeKind.Utc), new DateTime(2024, 3, 20, 13, 21, 33, 0, DateTimeKind.Utc), 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
