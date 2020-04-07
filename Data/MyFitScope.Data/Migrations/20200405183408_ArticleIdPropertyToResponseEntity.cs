namespace MyFitScope.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ArticleIdPropertyToResponseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArticleId",
                table: "Responses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Responses_ArticleId",
                table: "Responses",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Articles_ArticleId",
                table: "Responses",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Articles_ArticleId",
                table: "Responses");

            migrationBuilder.DropIndex(
                name: "IX_Responses_ArticleId",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Responses");
        }
    }
}
