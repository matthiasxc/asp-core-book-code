using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublisherId { get; set; }
    }
}
