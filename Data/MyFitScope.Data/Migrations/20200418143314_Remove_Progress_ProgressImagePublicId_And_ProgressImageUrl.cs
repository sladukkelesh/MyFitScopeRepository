using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFitScope.Data.Migrations
{
    public partial class Remove_Progress_ProgressImagePublicId_And_ProgressImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgressImagePublicId",
                table: "Progresses");

            migrationBuilder.DropColumn(
                name: "ProgressImageUrl",
                table: "Progresses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProgressImagePublicId",
                table: "Progresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgressImageUrl",
                table: "Progresses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
