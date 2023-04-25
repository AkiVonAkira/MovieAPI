using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieAPI.Migrations
{
    public partial class moviePersonGenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonGenreId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_PersonGenreId",
                table: "Movies",
                column: "PersonGenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_PersonGenres_PersonGenreId",
                table: "Movies",
                column: "PersonGenreId",
                principalTable: "PersonGenres",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_PersonGenres_PersonGenreId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_PersonGenreId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "PersonGenreId",
                table: "Movies");
        }
    }
}
