using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FILMEX.Migrations
{
    /// <inheritdoc />
    public partial class CommentAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Comment_CommentId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Comment_CommentId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CommentId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Actors_CommentId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Actors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishDate",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Actors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CommentId",
                table: "Reviews",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_CommentId",
                table: "Actors",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Comment_CommentId",
                table: "Actors",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Comment_CommentId",
                table: "Reviews",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "Id");
        }
    }
}
