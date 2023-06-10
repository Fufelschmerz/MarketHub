using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketHub.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewInitRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Name", "RoleType" },
                values: new object[] { "Пользователь", 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Name", "RoleType" },
                values: new object[] { "Покупатель", 3 });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "RoleType" },
                values: new object[] { 3L, "Продавец", 2 });
        }
    }
}
