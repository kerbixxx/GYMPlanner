using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDialog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherUserName",
                table: "Dialogs");

            migrationBuilder.CreateIndex(
                name: "IX_Dialogs_OtherUserId",
                table: "Dialogs",
                column: "OtherUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Dialogs_UserId",
                table: "Dialogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogs_Users_OtherUserId",
                table: "Dialogs",
                column: "OtherUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogs_Users_UserId",
                table: "Dialogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dialogs_Users_OtherUserId",
                table: "Dialogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Dialogs_Users_UserId",
                table: "Dialogs");

            migrationBuilder.DropIndex(
                name: "IX_Dialogs_OtherUserId",
                table: "Dialogs");

            migrationBuilder.DropIndex(
                name: "IX_Dialogs_UserId",
                table: "Dialogs");

            migrationBuilder.AddColumn<string>(
                name: "OtherUserName",
                table: "Dialogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
