using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FILMEX.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Actors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CommentId",
                table: "Reviews",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_CommentId",
                table: "Actors",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_MovieId",
                table: "Comment",
                column: "MovieId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Comment_CommentId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Comment_CommentId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "Comment");

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
                name: "CommentId",
                table: "Actors");
        }
    }
}
