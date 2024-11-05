using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarshoppingCartApi.Migrations
{
    /// <inheritdoc />
    public partial class Role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5dbdd449-0e93-4bc5-95e8-d3b8528de888", "69649ea0-863e-4088-bd2d-4dcd10c7b328", "Admin", "admin" },
                    { "c19cca37-3a73-4037-a672-29d9f4a728a9", "450b7ab4-c404-42ed-98d5-2d9405ea6634", "User", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dbdd449-0e93-4bc5-95e8-d3b8528de888");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c19cca37-3a73-4037-a672-29d9f4a728a9");
        }
    }
}
