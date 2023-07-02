using MoviesCrud.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCrud.ViewModels
{
    public class MovieVM
    {
        public int Id { get; set; }
        [Required, StringLength(250)]
        public string Title { get; set; }
        public int Year { get; set; }
        [Range(0, 10)]
        public float Rate { get; set; }
        [Required, StringLength(2500)]
        public string StoryLine { get; set; }
        [Display(Name = "Choose Poster")]
        public byte[] Poster { get; set; }
        [Display(Name = "Gunre")]
        public Byte GunreId { get; set; }
        public IEnumerable<Gunre> Gunres { get; set; }
    }
}
