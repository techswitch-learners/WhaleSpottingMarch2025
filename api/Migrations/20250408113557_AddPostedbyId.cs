using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhaleSpottingBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddPostedbyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostedById",
                table: "Sighting",
                type: "text",
                nullable: true,
                defaultValue:null);

            migrationBuilder.UpdateData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 1,
                column: "PostedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 2,
                column: "PostedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 3,
                column: "PostedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 4,
                column: "PostedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 5,
                column: "PostedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 6,
                column: "PostedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 7,
                column: "PostedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 8,
                column: "PostedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 9,
                column: "PostedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 10,
                column: "PostedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 11,
                column: "PostedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 12,
                column: "PostedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 13,
                column: "PostedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 14,
                column: "PostedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 15,
                column: "PostedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 16,
                column: "PostedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 17,
                column: "PostedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 18,
                column: "PostedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 19,
                column: "PostedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sighting",
                keyColumn: "Id",
                keyValue: 20,
                column: "PostedById",
                value: null);

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
        }
    }
}
