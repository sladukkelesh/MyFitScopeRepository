﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFitScope.Data.Migrations
{
    public partial class Progress_Entity_Validations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Progresses",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Progresses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
