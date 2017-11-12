using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class MockBookstoreRepository : IBookstoreRepository
    {

        #region Publisher methods
        public PublisherDTO GetPublisher(int publisherId, bool includeBooks = false)
        {
            var publisher = MockData.Current.Publishers.FirstOrDefault(p => p.Id.Equals(publisherId));

            if (includeBooks && publisher != null)
            {
                publisher.Books = MockData.Current.Books.Where(b => b.PublisherId.Equals(publisherId)).ToList();
            }
            return publisher;
        }

        public IEnumerable<PublisherDTO> GetPublishers()
        {
            return MockData.Current.Publishers;
        }

        public bool PublisherExists(int publisherId)
        {
            return MockData.Current.Publishers.Count(p => p.Id.Equals(publisherId)).Equals(1);
        }

        public void AddPublisher(PublisherDTO publisher)
        {
            var id = GetPublishers().Max(m => m.Id) + 1;
            publisher.Id = id;
            MockData.Current.Publishers.Add(publisher);
        }

        public void UpdatePublisher(int id, PublisherUpdateDTO publisher)
        {
            var publisherToUpdate = GetPublisher(id);
            publisherToUpdate.Name = publisher.Name;
            publisherToUpdate.Established = publisher.Established;
        }

        public void DeletePublisher(PublisherDTO publisher)
        {
            foreach (var book in publisher.Books)
            {
                DeleteBook(book);
            }

            MockData.Current.Publishers.Remove(publisher);
        }

        #endregion

        #region Book Methods

        public IEnumerable<BookDTO> GetBooks(int publisherId)
        {
            return MockData.Current.Books.Where(b => b.PublisherId.Equals(publisherId));
        }

        public BookDTO GetBook(int publisherId, int bookId)
        {
            return MockData.Current.Books.FirstOrDefault(b => b.PublisherId.Equals(publisherId) && b.Id.Equals(bookId));
        }

        public void DeleteBook(BookDTO book)
        {
            MockData.Current.Books.Remove(book);
        }

        public void AddBook(BookDTO book)
        {
            var bookId = MockData.Current.Books.Max(m => m.Id) + 1;
            book.Id = bookId;
            MockData.Current.Books.Add(book);
        }

        public void UpdateBook(int publisherId, int bookId, BookUpdateDTO book)
        {
            var bookToUpdate = GetBook(publisherId, bookId);
            bookToUpdate.Title = book.Title;
        }

        #endregion 


        public bool Save()
        {
            return true;
        }

        
    }

       
}
