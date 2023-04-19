using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        [Required]
        [StringLength(50)]
        public string MovieName { get; set; }
        [Required]
        [StringLength(250)]
        public string MovieLink { get; set; }
        public int tmbdId { get; set; }
    }
}
