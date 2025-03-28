using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WhaleSpottingBackend.Migrations
{
    /// <inheritdoc />
    public partial class SpeciesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Species",
                table: "Sighting");

            migrationBuilder.AddColumn<int>(
                name: "SpeciesId",
                table: "Sighting",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SpeciesName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sighting_SpeciesId",
                table: "Sighting",
                column: "SpeciesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sighting_Species_SpeciesId",
                table: "Sighting",
                column: "SpeciesId",
                principalTable: "Species",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sighting_Species_SpeciesId",
                table: "Sighting");

            migrationBuilder.DropTable(
                name: "Species");

            migrationBuilder.DropIndex(
                name: "IX_Sighting_SpeciesId",
                table: "Sighting");

            migrationBuilder.DropColumn(
                name: "SpeciesId",
                table: "Sighting");

            migrationBuilder.AddColumn<string>(
                name: "Species",
                table: "Sighting",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
