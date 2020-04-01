using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFitScope.Data.Migrations
{
    public partial class ChangeDayOfWeekEnumtoWeekDayEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "WorkoutDays");

            migrationBuilder.AddColumn<int>(
                name: "WeekDay",
                table: "WorkoutDays",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeekDay",
                table: "WorkoutDays");

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "WorkoutDays",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
