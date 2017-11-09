using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class PublisherDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Established { get; set; }

        public ICollection<BookDTO> Books { get; set; } = new List<BookDTO>();
        public int BookCount { get { return Books.Count; } }

    }
}
