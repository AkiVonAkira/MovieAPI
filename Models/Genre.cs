﻿using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Genre
    {
        [Key]
        public int? GenreId { get; set; }
        [Required]
        [StringLength(25)]
        public string? GenreName { get; set; }
        [Required]
        [StringLength(250)]
        public string? GenreDescription { get; set; }
        public int? tmbdId { get; set; }
    }
}
