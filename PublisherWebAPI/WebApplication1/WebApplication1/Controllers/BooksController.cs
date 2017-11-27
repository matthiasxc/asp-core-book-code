using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    [Route("api/publishers")]
    public class BooksController : Controller
    {
        IBookstoreRepository _bookstore;

        public BooksController(IBookstoreRepository bookstore)
        {
            _bookstore = bookstore;
        }
        
        [HttpGet("{publisherId}/books")]
        public IActionResult Get(int publisherId)
        {
            var books = _bookstore.GetBooks(publisherId);

            return Ok(books);
        }

        [HttpGet("{publisherId}/books/{id}", Name="GetBook")]
        public IActionResult Get(int publisherId, int id)
        {
            var book = _bookstore.GetBook(publisherId, id);

            if (book == null) return NotFound();

            return Ok(book);
        }

        [HttpPost("{publisherId}/books")]
        public IActionResult Post(int publisherId, [FromBody]BookCreateDTO book)
        {
            if (book == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var publisherExists = _bookstore.PublisherExists(publisherId);
            if (!publisherExists) return NotFound();

            var bookToAdd = new BookDTO { PublisherId = publisherId, Title = book.Title };
            _bookstore.AddBook(bookToAdd);
            _bookstore.Save();

            // once the book is saved, return the book as it exists in the data set
            return CreatedAtRoute("GetBook", new { publisherId = publisherId, id = bookToAdd.Id }, bookToAdd);
        }

        [HttpPut("{publisherId}/books/{id}")]
        public IActionResult Put(int publisherId, int id, [FromBody]BookUpdateDTO book)
        {
            if (book == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var publisherExists = _bookstore.PublisherExists(publisherId);
            if (!publisherExists) return NotFound();

            _bookstore.UpdateBook(publisherId, id, book);
            _bookstore.Save();

            return NoContent();
        }

        [HttpPatch("{publisherId}/books/{id}")]
        public IActionResult Patch(int publisherId, int id, [FromBody]JsonPatchDocument<BookUpdateDTO> book)
        {
            if (book == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var bookToUpdate = _bookstore.GetBook(publisherId, id);
            if (bookToUpdate == null) return NotFound();

            var bookPatch = new BookUpdateDTO()
                                {
                                    Title = bookToUpdate.Title,
                                    PublisherId = bookToUpdate.PublisherId
                                };
            book.ApplyTo(bookPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            _bookstore.UpdateBook(publisherId, id, bookPatch);
            _bookstore.Save();

            return NoContent();
        }

        [HttpDelete("{publisherId}/books/{id}")]
        public IActionResult Delete(int publisherId, int id)
        {
            var bookToDelete = _bookstore.GetBook(publisherId, id);
            if (bookToDelete == null) return NotFound();

            _bookstore.DeleteBook(bookToDelete);
            _bookstore.Save();

            return NoContent();
        }
    }
}