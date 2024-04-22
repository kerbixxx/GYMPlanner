using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class reworkTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.AddColumn<string>(
                name: "TagsDb",
                table: "Plans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: 1,
                column: "TagsDb",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagsDb",
                table: "Plans");

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_PlanId",
                table: "Tags",
                column: "PlanId");
        }
    }
}
