using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFitScope.Data.Migrations
{
    public partial class Change_ProgressImage_PublicId_Property_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublidId",
                table: "ProgressImages");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "ProgressImages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "ProgressImages");

            migrationBuilder.AddColumn<string>(
                name: "PublidId",
                table: "ProgressImages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
