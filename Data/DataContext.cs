using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;

namespace MovieAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }
        public DbSet<PersonGenre> PersonGenres { get; set; }
    }
}
