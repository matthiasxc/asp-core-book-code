﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class MockData
    {
        public static MockData Current { get; } = new MockData();

        public List<PublisherDTO> Publishers { get; set; }
        public List<BookDTO> Books { get; set; }

        public MockData()
        {
            Publishers = new List<PublisherDTO>
            {
                new PublisherDTO { Id = 1, Established = 1921, Name = "Publishing House 1" },
                new PublisherDTO { Id = 2, Established = 1888, Name = "Publishing House 2" }
            };

            Books = new List<BookDTO>
            {
                new BookDTO { Id=1, PublisherId=2, Title = "Book 1"},
                new BookDTO { Id=2, PublisherId=1, Title = "Book 2"},
                new BookDTO { Id=3, PublisherId=2, Title = "Book 3"},
                new BookDTO { Id=4, PublisherId=1, Title = "Book 4"},

            };
        }
     }
}
