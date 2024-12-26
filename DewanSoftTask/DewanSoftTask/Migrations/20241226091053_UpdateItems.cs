using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DewanSoftTask.Migrations
{
    /// <inheritdoc />
    public partial class UpdateItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "AmountSold", "Balance", "Name", "Price" },
                values: new object[] { 6, 0, 0, "Mouse", 50m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
