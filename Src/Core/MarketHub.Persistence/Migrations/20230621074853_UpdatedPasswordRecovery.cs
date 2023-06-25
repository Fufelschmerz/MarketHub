using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketHub.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPasswordRecovery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PasswordRecoveries_Accounts_AccountId",
                table: "PasswordRecoveries");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "PasswordRecoveries",
                newName: "UpdatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "PasswordRecoveries",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PasswordRecoveries_AccountId",
                table: "PasswordRecoveries",
                newName: "IX_PasswordRecoveries_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordRecoveries_Users_UserId",
                table: "PasswordRecoveries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PasswordRecoveries_Users_UserId",
                table: "PasswordRecoveries");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PasswordRecoveries",
                newName: "AccountId");

            migrationBuilder.RenameColumn(
                name: "UpdatedAtUtc",
                table: "PasswordRecoveries",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameIndex(
                name: "IX_PasswordRecoveries_UserId",
                table: "PasswordRecoveries",
                newName: "IX_PasswordRecoveries_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordRecoveries_Accounts_AccountId",
                table: "PasswordRecoveries",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
