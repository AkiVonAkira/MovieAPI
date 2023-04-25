using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.Models
{
    public class Movie
    {
        [Key]
        public int? MovieId { get; set; }
        [Required]
        [StringLength(50)]
        public string? MovieName { get; set; }
        [Required]
        [StringLength(250)]
        public string? MovieLink { get; set; }
        public int? tmbdId { get; set; }
        [ForeignKey("PersonGenre")]
        public int? PersonGenreId { get; set; }
        virtual public PersonGenre? PersonGenre { get; set; }
    }
}
