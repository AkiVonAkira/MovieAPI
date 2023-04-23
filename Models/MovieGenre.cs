using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.Models
{
    public class MovieGenre
    {
        [Key]
        public int? Id { get; set; }
        [ForeignKey("Movie")]
        public int? MovieId { get; set; }
        virtual public Movie? Movie { get; set; }
        [ForeignKey("Genre")]
        public int? GenreId { get; set; }
        virtual public Genre? Genre { get; set; }
    }
}
