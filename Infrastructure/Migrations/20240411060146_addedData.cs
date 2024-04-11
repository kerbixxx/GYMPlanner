using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Excersises",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Упражнение 1" });

            migrationBuilder.InsertData(
                table: "Frequencies",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Частота 1" });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[] { 1, "План 1", 1 });

            migrationBuilder.InsertData(
                table: "PlanExcersiseFrequencys",
                columns: new[] { "ExcersiseId", "FrequencyId", "Id", "PlanId", "Description" },
                values: new object[] { 1, 1, 1, 1, "15" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanExcersiseFrequencys",
                keyColumns: new[] { "ExcersiseId", "FrequencyId", "Id", "PlanId" },
                keyValues: new object[] { 1, 1, 1, 1 });

            migrationBuilder.DeleteData(
                table: "Excersises",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Frequencies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
