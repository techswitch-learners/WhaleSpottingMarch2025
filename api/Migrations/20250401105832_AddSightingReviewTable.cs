using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WhaleSpottingBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddSightingReviewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SightingReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReportID = table.Column<int>(type: "integer", nullable: false),
                    AdminID = table.Column<int>(type: "integer", nullable: false),
                    Approved = table.Column<bool>(type: "boolean", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SightingReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SightingReview_Sighting_ReportID",
                        column: x => x.ReportID,
                        principalTable: "Sighting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SightingReview_ReportID",
                table: "SightingReview",
                column: "ReportID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SightingReview");
        }
    }
}
