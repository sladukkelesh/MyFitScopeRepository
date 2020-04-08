using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFitScope.Data.Migrations
{
    public partial class ProgressEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Progresses",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Weight = table.Column<double>(nullable: false),
                    Biceps = table.Column<double>(nullable: true),
                    Chest = table.Column<double>(nullable: true),
                    Stomach = table.Column<double>(nullable: true),
                    Hips = table.Column<double>(nullable: true),
                    Thigh = table.Column<double>(nullable: true),
                    Calf = table.Column<double>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progresses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_IsDeleted",
                table: "Progresses",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Progresses");
        }
    }
}
