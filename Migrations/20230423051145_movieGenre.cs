using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieAPI.Migrations
{
    public partial class movieGenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_PersonMovies_Movies_MovieId",
                table: "PersonMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonMovies_Persons_PersonId",
                table: "PersonMovies");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "PersonMovies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "PersonMovies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "PersonGenres",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "PersonGenres",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "tmbdId",
                table: "Movies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "MovieRatings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "MovieRatings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: true),
                    GenreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId");
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_GenreId",
                table: "MovieGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_MovieId",
                table: "MovieGenres",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieRatings_Movies_MovieId",
                table: "MovieRatings",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieRatings_Persons_PersonId",
                table: "MovieRatings",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonGenres_Genres_GenreId",
                table: "PersonGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonGenres_Persons_PersonId",
                table: "PersonGenres",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonMovies_Movies_MovieId",
                table: "PersonMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonMovies_Persons_PersonId",
                table: "PersonMovies",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId");
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

            migrationBuilder.DropForeignKey(
                name: "FK_PersonMovies_Movies_MovieId",
                table: "PersonMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonMovies_Persons_PersonId",
                table: "PersonMovies");

            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "PersonMovies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "PersonMovies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "PersonGenres",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "PersonGenres",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "tmbdId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "MovieRatings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "MovieRatings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_PersonMovies_Movies_MovieId",
                table: "PersonMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonMovies_Persons_PersonId",
                table: "PersonMovies",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
