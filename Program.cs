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

            var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("./appsettings.json", optional: true, reloadOnChange: true);
            var configuration = configurationBuilder.Build();
            string? tmdbKey = configuration["key"];

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            CreateMethods(app);



            // Get all Movies
            app.MapGet("/api/movie", async (DataContext context) =>
            {
                var movies = await context.Movies.ToListAsync();
                return Results.Ok(movies);
            });

            // Get all Genres
            app.MapGet("/api/genre/", async (DataContext context) =>
            {
                var genres = await context.Genres.ToListAsync();
                return Results.Ok(genres);
            });

            // Get all Persons
            app.MapGet("/api/person/", async (DataContext context) =>
            {
                var persons = await context.Persons.ToListAsync();
                return Results.Ok(persons);
            });

            // Get Movies by id
            app.MapGet("/api/movie/{id}", async (DataContext context, int id) =>
                await context.Movies.FindAsync(id) is Movie movie ? Results.Ok(movie) : Results.NotFound("Movie not found."));



            // Updating Movies
            app.MapPut("/api/movie/{id}", async (DataContext context, Movie updatedMovie, int id) =>
            {
                var existingMovie = await context.Movies.FindAsync(id);

                if (existingMovie != null)
                {
                    updatedMovie.MovieId = id;
                    context.Entry(existingMovie).CurrentValues.SetValues(updatedMovie);
                    await context.SaveChangesAsync();
                    return Results.Ok(updatedMovie);
                }
                return Results.NotFound("Movie not found");
            });

            // Deleting Movies
            app.MapDelete("/api/movie/{id}", async (DataContext context, int id) =>
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
            app.MapGet("/api/person/movie", async (DataContext context, string name) =>
            {
                var personMovies = await context.MovieRatings
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
            app.MapGet("/api/genre/person", async (DataContext context, string name) =>
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
            app.MapGet("/api/rating/person", async (DataContext context, string name) =>
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
            app.MapPost("/api/rating/person", async (DataContext context, MovieRating mr) =>
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

                return Results.Created($"/api/person/Ratings?personId={mr.PersonId}&movieId={mr.MovieId}", movieRating);
            });

            // Add genre to a person
            app.MapPost("/api/person/genre", async (DataContext context, PersonGenre model) =>
            {
                var person = await context.Persons.FirstOrDefaultAsync(p => p.PersonId == model.PersonId);
                if (person == null) { return Results.BadRequest("Person not found."); }

                var genre = await context.Genres.FirstOrDefaultAsync(g => g.GenreId == model.GenreId);
                if (genre == null) { return Results.BadRequest("Genre not found."); }

                var existingPersonGenre = await context.PersonGenres.FirstOrDefaultAsync(pg => pg.PersonId == person.PersonId && pg.GenreId == genre.GenreId);
                if (existingPersonGenre != null) { return Results.BadRequest("Genre already exists for the person."); }

                var personGenre = new PersonGenre
                {
                    PersonId = person.PersonId,
                    GenreId = genre.GenreId
                };

                await context.PersonGenres.AddAsync(personGenre);
                await context.SaveChangesAsync();

                return Results.Created($"/api/person/Genres?personName={person.Name}&genreName={genre.GenreName}", personGenre);
            });

            // Add movies linked to a person
            app.MapPost("/api/person/movie", async (DataContext context, MovieRating pm) =>
            {
                var person = await context.Persons.FirstOrDefaultAsync(p => p.PersonId == pm.PersonId);
                if (person == null) { return Results.BadRequest("Person not found."); }

                var movie = await context.Movies.FirstOrDefaultAsync(m => m.MovieId == pm.MovieId);
                if (movie == null) { return Results.BadRequest("Movie not found."); }

                var existingPersonMovie = await context.MovieRatings.FirstOrDefaultAsync(pm => pm.MovieId == movie.MovieId && pm.PersonId == person.PersonId);
                if (existingPersonMovie != null) { return Results.BadRequest("Person Movie already exists."); }

                var personMovie = new MovieRating
                {
                    PersonId = person.PersonId,
                    MovieId = movie.MovieId,
                };

                await context.MovieRatings.AddAsync(personMovie);
                await context.SaveChangesAsync();

                return Results.Created($"/api/person/Movie?personId={pm.PersonId}&movieId={pm.MovieId}", personMovie);
            });


            // Add movie links to specific person and genre
            app.MapPost("/api/person/MovieLinks", async (DataContext context, Movie movieModel, int personId, string genreName) =>
            {
                var person = await context.Persons.FirstOrDefaultAsync(p => p.PersonId == personId);
                if (person == null) { return Results.BadRequest("Person not found."); }

                var genre = await context.Genres.FirstOrDefaultAsync(g => g.GenreName == genreName);
                if (genre == null) { return Results.BadRequest("Genre not found."); }

                var existingPersonGenre = await context.PersonGenres.FirstOrDefaultAsync(pg => pg.PersonId == person.PersonId && pg.GenreId == genre.GenreId);

                var personGenre = new PersonGenre
                {
                    PersonId = person.PersonId,
                    GenreId = genre.GenreId
                };

                await context.PersonGenres.AddAsync(personGenre);

                var movie = await context.Movies.FirstOrDefaultAsync(m => m.MovieId == movieModel.MovieId);
                if (movie == null)
                {
                    movie = new Movie
                    {
                        MovieName = movieModel.MovieName,
                        MovieLink = movieModel.MovieLink,
                        tmbdId = movieModel.tmbdId,
                        PersonGenreId = personGenre.Id
                    };
                    movie.PersonGenreId = personGenre.Id;
                    movie.PersonGenre = personGenre;
                    await context.Movies.AddAsync(movie);
                }

                await context.SaveChangesAsync();

                return Results.Ok(movie);
                //return Results.Created($"/api/movie/{movie.MovieId}", movie);
            });

            app.MapGet("/api/movie/discover/", async (DataContext context, string genreName) =>
            {
                var genre = await context.Genres.FirstOrDefaultAsync(g => g.GenreName == genreName);

                var url = $@"https://api.themoviedb.org/3/discover/movie?api_key={tmdbKey}&language=en-US
                                &sort_by=popularity.desc&include_adult=false&include_video=false&page=1
                                &with_genres={genre.tmbdId}&with_watch_monetization_types=free";

                var client = new HttpClient();

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                //This returns the raw dataas json , easier to read
                return Results.Content(content, contentType: "application/json");
            });


            app.Run();
        }

        private static void CreateMethods(WebApplication app)
        {
            // Create Movies
            app.MapPost("/api/movie", async (DataContext context, Movie movie) =>
            {
                context.Movies.Add(movie);
                await context.SaveChangesAsync();
                return Results.Created($"/api/movie/{movie.MovieId}", movie);
            });

            // Create Genres
            app.MapPost("/api/genre", async (DataContext context, Genre genre) =>
            {
                context.Genres.Add(genre);
                await context.SaveChangesAsync();
                return Results.Created($"/api/movie/{genre.GenreId}", genre);
            });

            // Create Persons
            app.MapPost("/api/person", async (DataContext context, Person person) =>
            {
                context.Persons.Add(person);
                await context.SaveChangesAsync();
                return Results.Created($"/api/movie/{person.PersonId}", person);
            });
        }
    }
}