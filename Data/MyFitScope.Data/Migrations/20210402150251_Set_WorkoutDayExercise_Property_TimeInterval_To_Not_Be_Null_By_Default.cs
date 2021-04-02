using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFitScope.Data.Migrations
{
    public partial class Set_WorkoutDayExercise_Property_TimeInterval_To_Not_Be_Null_By_Default : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "TimeInterval",
                table: "WorkoutDaysExercises",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "TimeInterval",
                table: "WorkoutDaysExercises",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan));
        }
    }
}
