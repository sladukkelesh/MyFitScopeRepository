using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFitScope.Data.Migrations
{
    public partial class WorkoutIdPropertyOfWorkoutDayFromIntTypetoStringtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutDays_Workouts_WorkoutId1",
                table: "WorkoutDays");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutDays_WorkoutId1",
                table: "WorkoutDays");

            migrationBuilder.DropColumn(
                name: "WorkoutId1",
                table: "WorkoutDays");

            migrationBuilder.AlterColumn<string>(
                name: "WorkoutId",
                table: "WorkoutDays",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutDays_WorkoutId",
                table: "WorkoutDays",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutDays_Workouts_WorkoutId",
                table: "WorkoutDays",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutDays_Workouts_WorkoutId",
                table: "WorkoutDays");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutDays_WorkoutId",
                table: "WorkoutDays");

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutId",
                table: "WorkoutDays",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "WorkoutId1",
                table: "WorkoutDays",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutDays_WorkoutId1",
                table: "WorkoutDays",
                column: "WorkoutId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutDays_Workouts_WorkoutId1",
                table: "WorkoutDays",
                column: "WorkoutId1",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
