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

            //Get all Movies
            app.MapGet("/api/movie", async (DataContext context) =>
            {
                await context.Movies.ToListAsync();
                return context.Movies;
            }).WithName("GetMovies");

            //Get Movies by id
            app.MapGet("/api/movie/{id}", async (DataContext context, int id) =>
                await context.Movies.FindAsync(id) is Movie movie ? Results.Ok(movie) : Results.NotFound("Movie not found.")
            ).WithName("GetMovieByID");

            //Create Movies
            app.MapPost("/api/movie", async (DataContext context, Movie movie) =>
            {
                context.Movies.Add(movie);
                await context.SaveChangesAsync();
                return Results.Created($"/api/Movie/{movie.MovieId}", movie);
            }).WithName("CreateMovie");

            //Updating Movies
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
            }).WithName("UpdateMovie");


            //Deleting Movies
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
            }).WithName("DeleteMovie");

            app.Run();
        }
    }
}