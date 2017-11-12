using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IBookstoreRepository
    {
        IEnumerable<PublisherDTO> GetPublishers();

        PublisherDTO GetPublisher(int publisherId, bool includeBooks = false);
        bool PublisherExists(int publisherId);
        void AddPublisher(PublisherDTO publisher);
        void UpdatePublisher(int id, PublisherUpdateDTO publisher);
        void DeletePublisher(PublisherDTO publisher);

        IEnumerable<BookDTO> GetBooks(int publisherId);
        BookDTO GetBook(int publisherId, int bookId);
        void AddBook(BookDTO book);
        void UpdateBook(int publisherId, int bookId, BookUpdateDTO book);
        void DeleteBook(BookDTO book);
        
        bool Save();


    }
}
