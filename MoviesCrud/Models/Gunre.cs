using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCrud.Models
{
    public class Gunre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte GunreId  { get; set;}
        [Required, MaxLength(100)]
        public string Name { get; set;}
      public  ICollection<Movie> movies { get; set; }
    }
}
