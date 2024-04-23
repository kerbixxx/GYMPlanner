using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixtypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanExcersiseFrequencys_Excersises_ExcersiseId",
                table: "PlanExcersiseFrequencys");

            migrationBuilder.RenameColumn(
                name: "ExcersiseId",
                table: "PlanExcersiseFrequencys",
                newName: "ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanExcersiseFrequencys_ExcersiseId",
                table: "PlanExcersiseFrequencys",
                newName: "IX_PlanExcersiseFrequencys_ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanExcersiseFrequencys_Excersises_ExerciseId",
                table: "PlanExcersiseFrequencys",
                column: "ExerciseId",
                principalTable: "Excersises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanExcersiseFrequencys_Excersises_ExerciseId",
                table: "PlanExcersiseFrequencys");

            migrationBuilder.RenameColumn(
                name: "ExerciseId",
                table: "PlanExcersiseFrequencys",
                newName: "ExcersiseId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanExcersiseFrequencys_ExerciseId",
                table: "PlanExcersiseFrequencys",
                newName: "IX_PlanExcersiseFrequencys_ExcersiseId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanExcersiseFrequencys_Excersises_ExcersiseId",
                table: "PlanExcersiseFrequencys",
                column: "ExcersiseId",
                principalTable: "Excersises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
