using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFitScope.Data.Migrations
{
    public partial class ChangeResponseCommentIdType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Comments_CommentId1",
                table: "Responses");

            migrationBuilder.DropIndex(
                name: "IX_Responses_CommentId1",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "CommentId1",
                table: "Responses");

            migrationBuilder.AlterColumn<string>(
                name: "CommentId",
                table: "Responses",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_CommentId",
                table: "Responses",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Comments_CommentId",
                table: "Responses",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Comments_CommentId",
                table: "Responses");

            migrationBuilder.DropIndex(
                name: "IX_Responses_CommentId",
                table: "Responses");

            migrationBuilder.AlterColumn<int>(
                name: "CommentId",
                table: "Responses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommentId1",
                table: "Responses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Responses_CommentId1",
                table: "Responses",
                column: "CommentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Comments_CommentId1",
                table: "Responses",
                column: "CommentId1",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
