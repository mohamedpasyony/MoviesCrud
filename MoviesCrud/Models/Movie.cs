using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCrud.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        [Required , MaxLength(250)]
        public string Title { get; set; }
        public int Year { get; set; }
        public float Rate { get; set; }
        [Required, MaxLength(2500)]
        public string StoryLine { get; set; }
        public byte[] Poster { get; set; }
        public Byte GunreId { get; set; }
        public Gunre Gunre { get; set; }
    }
}
