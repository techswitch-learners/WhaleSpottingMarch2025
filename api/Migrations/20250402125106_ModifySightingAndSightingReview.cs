using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhaleSpottingBackend.Migrations
{
    /// <inheritdoc />
    public partial class ModifySightingAndSightingReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SightingReview_Sighting_ReportID",
                table: "SightingReview");

            migrationBuilder.RenameColumn(
                name: "ReportID",
                table: "SightingReview",
                newName: "SightingID");

            migrationBuilder.RenameIndex(
                name: "IX_SightingReview_ReportID",
                table: "SightingReview",
                newName: "IX_SightingReview_SightingID");

            migrationBuilder.CreateIndex(
                name: "IX_SightingReview_AdminID",
                table: "SightingReview",
                column: "AdminID");

            migrationBuilder.AddForeignKey(
                name: "FK_SightingReview_AspNetUsers_AdminID",
                table: "SightingReview",
                column: "AdminID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SightingReview_Sighting_SightingID",
                table: "SightingReview",
                column: "SightingID",
                principalTable: "Sighting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SightingReview_AspNetUsers_AdminID",
                table: "SightingReview");

            migrationBuilder.DropForeignKey(
                name: "FK_SightingReview_Sighting_SightingID",
                table: "SightingReview");

            migrationBuilder.DropIndex(
                name: "IX_SightingReview_AdminID",
                table: "SightingReview");

            migrationBuilder.RenameColumn(
                name: "SightingID",
                table: "SightingReview",
                newName: "ReportID");

            migrationBuilder.RenameIndex(
                name: "IX_SightingReview_SightingID",
                table: "SightingReview",
                newName: "IX_SightingReview_ReportID");

            migrationBuilder.AddForeignKey(
                name: "FK_SightingReview_Sighting_ReportID",
                table: "SightingReview",
                column: "ReportID",
                principalTable: "Sighting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
