using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gql.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CommentId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryParentId",
                table: "BlogCategorys",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentId",
                table: "Comments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategorys_CategoryParentId",
                table: "BlogCategorys",
                column: "CategoryParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogCategorys_BlogCategorys_CategoryParentId",
                table: "BlogCategorys",
                column: "CategoryParentId",
                principalTable: "BlogCategorys",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_CommentId",
                table: "Comments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogCategorys_BlogCategorys_CategoryParentId",
                table: "BlogCategorys");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_CommentId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_BlogCategorys_CategoryParentId",
                table: "BlogCategorys");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CategoryParentId",
                table: "BlogCategorys");
        }
    }
}
