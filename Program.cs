using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Models;

namespace MovieAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            //app.UseAuthorization();

            // Get all Movies
            app.MapGet("/api/movie", async (DataContext context) =>
            {
                var movies = await context.Movies.ToListAsync();
                return Results.Ok(movies);
            });

            // Get all Genres
            app.MapGet("/api/Genre/GetAllGenre/", async (DataContext context) =>
            {
                var genres = await context.Genres.ToListAsync();
                return Results.Ok(genres);
            });

            // Get all Persons
            app.MapGet("/api/Persons/GetPersons/", async (DataContext context) =>
            {
                var persons = await context.Persons.ToListAsync();
                return Results.Ok(persons);
            });

            // Get Movies by id
            app.MapGet("/api/movie/{id}", async (DataContext context, int id) =>
                await context.Movies.FindAsync(id) is Movie movie ? Results.Ok(movie) : Results.NotFound("Movie not found."));

            //Create Movies
            app.MapPost("/api/movie", async (DataContext context, Movie movie) =>
            {
                context.Movies.Add(movie);
                await context.SaveChangesAsync();
                return Results.Created($"/api/Movie/{movie.MovieId}", movie);
            });

            // Updating Movies
            app.MapPut("/api/movie/{id}", async (DataContext context, Movie updatedMovie, int id) =>
            {
                var existingMovie = await context.Movies.FindAsync(id);

                if (existingMovie != null)
                {
                    context.Entry(existingMovie).CurrentValues.SetValues(updatedMovie);
                    await context.SaveChangesAsync();
                    return Results.Ok(updatedMovie);
                }
                return Results.NotFound("Movie not found");
            });

            // Deleting Movies
            app.MapDelete("/api/Movie/{id}", async (DataContext context, int id) =>
            {
                var movieItemFromDb = await context.Movies.FindAsync(id);

                if (movieItemFromDb != null)
                {
                    context.Remove(movieItemFromDb);
                    await context.SaveChangesAsync();
                    return Results.Ok();
                }
                return Results.NotFound("Movie not found");
            });

            // Get movies linked to a person
            app.MapGet("/api/Person/GetMovies", async (DataContext context, string name) =>
            {
                var personMovies = await context.PersonMovies
                    .Include(pm => pm.Movie)
                    .Where(pm => pm.Person.Name == name)
                    .Select(pm => new
                    {
                        pm.Movie.MovieName,
                        pm.Movie.MovieLink,
                        pm.Movie.MovieId
                    })
                    .ToListAsync();

                return Results.Ok(personMovies);
            });

            // Get Genres linked to a person
            app.MapGet("/api/PersonGenre/", async (DataContext context, string name) =>
            {
                var personGenres = await context.PersonGenres
                    .Include(x => x.Genre)
                    .Include(x => x.Person)
                    .Where(x => x.Person.Name == name)
                    .Select(x => x.Genre.GenreName)
                    .ToListAsync();

                return Results.Ok(personGenres);
            });

            // Get ratings on movies linked to a person
            app.MapGet("/api/Person/Ratings/", async (DataContext context, string name) =>
            {
                var personMovieRatings = await context.MovieRatings
                    .Include(mr => mr.Movie)
                    .Include(mr => mr.Person)
                    .Where(mr => mr.Person.Name == name)
                    .Select(mr => new
                    {
                        MovieName = mr.Movie.MovieName,
                        Rating = mr.Rating
                    })
                    .ToListAsync();

                return Results.Ok(personMovieRatings);
            });

            // Add ratings to movies linked to a person
            app.MapPost("/api/Person/Ratings", async (DataContext context, MovieRating mr) =>
            {
                var person = await context.Persons.FirstOrDefaultAsync(p => p.PersonId == mr.PersonId);

                if (person == null) { return Results.BadRequest("Person not found."); }

                var movie = await context.Movies.FirstOrDefaultAsync(m => m.MovieId == mr.MovieId);

                if (movie == null) { return Results.BadRequest("Movie not found."); }

                var existingRating = await context.MovieRatings.FirstOrDefaultAsync(mr => mr.MovieId == movie.MovieId && mr.PersonId == person.PersonId);

                if (existingRating != null) { return Results.BadRequest("Rating already exists."); }

                var movieRating = new MovieRating
                {
                    MovieId = movie.MovieId,
                    PersonId = person.PersonId,
                    Rating = mr.Rating
                };

                await context.MovieRatings.AddAsync(movieRating);
                await context.SaveChangesAsync();

                return Results.Created($"/api/Person/Ratings?personId={mr.PersonId}&movieId={mr.MovieId}", movieRating);
            });

            // Add genre to a person
            app.MapPost("/api/Person/Genres", async (DataContext context, PersonGenre model) =>
            {
                var person = await context.Persons
                    .FirstOrDefaultAsync(p => p.PersonId == model.PersonId);

                if (person == null)
                {
                    return Results.BadRequest("Person not found.");
                }

                var genre = await context.Genres
                    .FirstOrDefaultAsync(g => g.GenreId == model.GenreId);

                if (genre == null)
                {
                    return Results.BadRequest("Genre not found.");
                }

                var existingPersonGenre = await context.PersonGenres
                    .FirstOrDefaultAsync(pg => pg.PersonId == person.PersonId && pg.GenreId == genre.GenreId);

                if (existingPersonGenre != null)
                {
                    return Results.BadRequest("Genre already exists for the person.");
                }

                var personGenre = new PersonGenre
                {
                    PersonId = person.PersonId,
                    GenreId = genre.GenreId
                };

                await context.PersonGenres.AddAsync(personGenre);
                await context.SaveChangesAsync();

                return Results.Created($"/api/Person/Genres?personName={person.Name}&genreName={genre.GenreName}", personGenre);
            });

            // Add movie links to specific person and genre
            app.MapPost("/api/Person/MovieLinks", async (DataContext context, PersonMovieGenre model) =>
            {
                var person = await context.Persons
                    .FirstOrDefaultAsync(p => p.PersonId == model.PersonId);

                if (person == null)
                {
                    return Results.BadRequest("Person not found.");
                }

                var movie = await context.Movies
                    .FirstOrDefaultAsync(m => m.MovieId == model.MovieId);

                if (movie == null)
                {
                    return Results.BadRequest("Movie not found.");
                }

                var genre = await context.Genres
                    .FirstOrDefaultAsync(g => g.GenreId == model.GenreId);

                if (genre == null)
                {
                    return Results.BadRequest("Genre not found.");
                }

                var personMovie = new PersonMovie
                {
                    PersonId = person.PersonId,
                    MovieId = movie.MovieId
                };

                var personGenre = new PersonGenre
                {
                    PersonId = person.PersonId,
                    GenreId = genre.GenreId
                };

                await context.PersonMovies.AddAsync(personMovie);
                await context.PersonGenres.AddAsync(personGenre);
                await context.SaveChangesAsync();

                return Results.Created($"/api/Person/MovieLinks?personId={model.PersonId}&movieId={model.MovieId}&genreId={model.GenreId}",
                    new { PersonMovieId = personMovie.Id, PersonGenreId = personGenre.Id });
            });
        }
    }
}