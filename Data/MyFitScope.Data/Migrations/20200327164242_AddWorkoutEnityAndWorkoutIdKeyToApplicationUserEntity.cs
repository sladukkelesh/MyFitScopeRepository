using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFitScope.Data.Migrations
{
    public partial class AddWorkoutEnityAndWorkoutIdKeyToApplicationUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkoutId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CreatorName = table.Column<string>(nullable: false),
                    IsCustom = table.Column<bool>(nullable: false),
                    Difficulty = table.Column<int>(nullable: false),
                    WorkoutType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WorkoutId",
                table: "AspNetUsers",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_IsDeleted",
                table: "Workouts",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Workouts_WorkoutId",
                table: "AspNetUsers",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Workouts_WorkoutId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WorkoutId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "AspNetUsers");
        }
    }
}
