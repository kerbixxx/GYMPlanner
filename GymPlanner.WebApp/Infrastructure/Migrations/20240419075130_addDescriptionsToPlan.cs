using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addDescriptionsToPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullDescription",
                table: "Plans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MenuDescription",
                table: "Plans",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FullDescription", "MenuDescription" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullDescription",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "MenuDescription",
                table: "Plans");
        }
    }
}
