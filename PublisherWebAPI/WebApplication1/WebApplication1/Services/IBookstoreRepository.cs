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
        void DeletePublisher(int id);
        bool Save();
    }
}
