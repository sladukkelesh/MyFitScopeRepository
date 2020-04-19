namespace MyFitScope.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Progress_ProgressImagePublicId_And_ProgressImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProgressImagePublicId",
                table: "Progresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgressImageUrl",
                table: "Progresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Progresses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_UserId",
                table: "Progresses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progresses_AspNetUsers_UserId",
                table: "Progresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progresses_AspNetUsers_UserId",
                table: "Progresses");

            migrationBuilder.DropIndex(
                name: "IX_Progresses_UserId",
                table: "Progresses");

            migrationBuilder.DropColumn(
                name: "ProgressImagePublicId",
                table: "Progresses");

            migrationBuilder.DropColumn(
                name: "ProgressImageUrl",
                table: "Progresses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Progresses");
        }
    }
}
