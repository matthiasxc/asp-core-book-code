using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class BookCreateDTO
    {
        [Required(ErrorMessage = "You must enter a name.")]
        [MaxLength(50)]
        public string Title {get; set;}
        public int PublisherID { get; set; }
    }
}
