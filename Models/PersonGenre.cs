using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.Models
{
    public class PersonGenre
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        virtual public Person Person { get; set; }
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        virtual public Genre Genre { get; set; }
    }
}
