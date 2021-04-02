using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFitScope.Data.Migrations
{
    public partial class Add_Properties_Sets_Reps_Weights_TimeInterval_To_WorkoutDaysExercises_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExerciseSettings",
                table: "Exercises");

            migrationBuilder.AddColumn<int>(
                name: "Reps",
                table: "WorkoutDaysExercises",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sets",
                table: "WorkoutDaysExercises",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeInterval",
                table: "WorkoutDaysExercises",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weights",
                table: "WorkoutDaysExercises",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reps",
                table: "WorkoutDaysExercises");

            migrationBuilder.DropColumn(
                name: "Sets",
                table: "WorkoutDaysExercises");

            migrationBuilder.DropColumn(
                name: "TimeInterval",
                table: "WorkoutDaysExercises");

            migrationBuilder.DropColumn(
                name: "Weights",
                table: "WorkoutDaysExercises");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseSettings",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
