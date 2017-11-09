using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Message
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Text { get; set; }
    }
}