using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieAPI.Migrations
{
    public partial class moreLinkModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_movieRatings_Movies_MovieId",
                table: "movieRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_movieRatings_Persons_PersonId",
                table: "movieRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_personGenres_Genres_GenreId",
                table: "personGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_personGenres_Persons_PersonId",
                table: "personGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_personGenres",
                table: "personGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_movieRatings",
                table: "movieRatings");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Persons");

            migrationBuilder.RenameTable(
                name: "personGenres",
                newName: "PersonGenres");

            migrationBuilder.RenameTable(
                name: "movieRatings",
                newName: "MovieRatings");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Persons",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_personGenres_PersonId",
                table: "PersonGenres",
                newName: "IX_PersonGenres_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_personGenres_GenreId",
                table: "PersonGenres",
                newName: "IX_PersonGenres_GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_movieRatings_PersonId",
                table: "MovieRatings",
                newName: "IX_MovieRatings_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_movieRatings_MovieId",
                table: "MovieRatings",
                newName: "IX_MovieRatings_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonGenres",
                table: "PersonGenres",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieRatings",
                table: "MovieRatings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PersonMovies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonMovies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonMovies_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonMovies_MovieId",
                table: "PersonMovies",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonMovies_PersonId",
                table: "PersonMovies",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieRatings_Movies_MovieId",
                table: "MovieRatings",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieRatings_Persons_PersonId",
                table: "MovieRatings",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonGenres_Genres_GenreId",
                table: "PersonGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonGenres_Persons_PersonId",
                table: "PersonGenres",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieRatings_Movies_MovieId",
                table: "MovieRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieRatings_Persons_PersonId",
                table: "MovieRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonGenres_Genres_GenreId",
                table: "PersonGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonGenres_Persons_PersonId",
                table: "PersonGenres");

            migrationBuilder.DropTable(
                name: "PersonMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonGenres",
                table: "PersonGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieRatings",
                table: "MovieRatings");

            migrationBuilder.RenameTable(
                name: "PersonGenres",
                newName: "personGenres");

            migrationBuilder.RenameTable(
                name: "MovieRatings",
                newName: "movieRatings");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Persons",
                newName: "LastName");

            migrationBuilder.RenameIndex(
                name: "IX_PersonGenres_PersonId",
                table: "personGenres",
                newName: "IX_personGenres_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonGenres_GenreId",
                table: "personGenres",
                newName: "IX_personGenres_GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieRatings_PersonId",
                table: "movieRatings",
                newName: "IX_movieRatings_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieRatings_MovieId",
                table: "movieRatings",
                newName: "IX_movieRatings_MovieId");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Persons",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_personGenres",
                table: "personGenres",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_movieRatings",
                table: "movieRatings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_movieRatings_Movies_MovieId",
                table: "movieRatings",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_movieRatings_Persons_PersonId",
                table: "movieRatings",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_personGenres_Genres_GenreId",
                table: "personGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_personGenres_Persons_PersonId",
                table: "personGenres",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
