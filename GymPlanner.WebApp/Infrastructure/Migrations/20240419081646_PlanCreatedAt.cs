using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PlanCreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Plans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Plans");
        }
    }
}
