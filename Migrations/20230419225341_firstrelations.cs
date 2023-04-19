using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieAPI.Migrations
{
    public partial class firstrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Persons");

            migrationBuilder.AddColumn<int>(
                name: "tmbdId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "movieRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movieRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_movieRatings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movieRatings_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personGenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personGenres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_personGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personGenres_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_movieRatings_MovieId",
                table: "movieRatings",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_movieRatings_PersonId",
                table: "movieRatings",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_personGenres_GenreId",
                table: "personGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_personGenres_PersonId",
                table: "personGenres",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movieRatings");

            migrationBuilder.DropTable(
                name: "personGenres");

            migrationBuilder.DropColumn(
                name: "tmbdId",
                table: "Movies");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Persons",
                type: "nvarchar(51)",
                maxLength: 51,
                nullable: false,
                defaultValue: "");
        }
    }
}
