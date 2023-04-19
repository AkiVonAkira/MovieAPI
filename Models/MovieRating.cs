using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.Models
{
    public class MovieRating
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        virtual public Movie Movie { get; set; }
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        virtual public Person Person { get; set; }
        [Required]
        public int Rating { get; set; }
    }
}
