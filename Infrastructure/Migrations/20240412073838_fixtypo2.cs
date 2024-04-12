using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixtypo2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanExcersiseFrequencys_Excersises_ExerciseId",
                table: "PlanExcersiseFrequencys");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanExcersiseFrequencys_Frequencies_FrequencyId",
                table: "PlanExcersiseFrequencys");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanExcersiseFrequencys_Plans_PlanId",
                table: "PlanExcersiseFrequencys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanExcersiseFrequencys",
                table: "PlanExcersiseFrequencys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Excersises",
                table: "Excersises");

            migrationBuilder.RenameTable(
                name: "PlanExcersiseFrequencys",
                newName: "PlanExerciseFrequencies");

            migrationBuilder.RenameTable(
                name: "Excersises",
                newName: "Exercises");

            migrationBuilder.RenameIndex(
                name: "IX_PlanExcersiseFrequencys_FrequencyId",
                table: "PlanExerciseFrequencies",
                newName: "IX_PlanExerciseFrequencies_FrequencyId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanExcersiseFrequencys_ExerciseId",
                table: "PlanExerciseFrequencies",
                newName: "IX_PlanExerciseFrequencies_ExerciseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanExerciseFrequencies",
                table: "PlanExerciseFrequencies",
                columns: new[] { "PlanId", "FrequencyId", "ExerciseId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanExerciseFrequencies_Exercises_ExerciseId",
                table: "PlanExerciseFrequencies",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanExerciseFrequencies_Frequencies_FrequencyId",
                table: "PlanExerciseFrequencies",
                column: "FrequencyId",
                principalTable: "Frequencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanExerciseFrequencies_Plans_PlanId",
                table: "PlanExerciseFrequencies",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanExerciseFrequencies_Exercises_ExerciseId",
                table: "PlanExerciseFrequencies");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanExerciseFrequencies_Frequencies_FrequencyId",
                table: "PlanExerciseFrequencies");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanExerciseFrequencies_Plans_PlanId",
                table: "PlanExerciseFrequencies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanExerciseFrequencies",
                table: "PlanExerciseFrequencies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises");

            migrationBuilder.RenameTable(
                name: "PlanExerciseFrequencies",
                newName: "PlanExcersiseFrequencys");

            migrationBuilder.RenameTable(
                name: "Exercises",
                newName: "Excersises");

            migrationBuilder.RenameIndex(
                name: "IX_PlanExerciseFrequencies_FrequencyId",
                table: "PlanExcersiseFrequencys",
                newName: "IX_PlanExcersiseFrequencys_FrequencyId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanExerciseFrequencies_ExerciseId",
                table: "PlanExcersiseFrequencys",
                newName: "IX_PlanExcersiseFrequencys_ExerciseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanExcersiseFrequencys",
                table: "PlanExcersiseFrequencys",
                columns: new[] { "PlanId", "FrequencyId", "ExerciseId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Excersises",
                table: "Excersises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanExcersiseFrequencys_Excersises_ExerciseId",
                table: "PlanExcersiseFrequencys",
                column: "ExerciseId",
                principalTable: "Excersises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanExcersiseFrequencys_Frequencies_FrequencyId",
                table: "PlanExcersiseFrequencys",
                column: "FrequencyId",
                principalTable: "Frequencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanExcersiseFrequencys_Plans_PlanId",
                table: "PlanExcersiseFrequencys",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
