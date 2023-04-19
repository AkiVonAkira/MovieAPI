using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(25)]
        public string LastName { get; set; }
        [StringLength(51)]
        public string FullName { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
    }
}
