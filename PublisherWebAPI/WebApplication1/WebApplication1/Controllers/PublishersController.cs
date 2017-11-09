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
    public class PublishersController : Controller
    {
        IBookstoreRepository _bookstore;

        public PublishersController(IBookstoreRepository bookstore)
        {
            _bookstore = bookstore;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookstore.GetPublishers());
        }

        [HttpGet("{id}", Name = "GetPublisher")]
        public IActionResult Get(int id, bool includeBooks = false)
        {
            var publisher = _bookstore.GetPublisher(id, includeBooks);

            // If there is no publisher with that ID, throw a 404
            if (publisher == null) return NotFound();

            return Ok(publisher);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PublisherCreateDTO publisher)
        {
            if (publisher == null) return BadRequest();

            // Define possible error states
            if (publisher.Established < 1534)
                ModelState.AddModelError("Established", "The first publishing house was founded in 1543.");
            // If there is an error state, kick out a bad request
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var publisherToAdd =
                new PublisherDTO
                {
                    Established = publisher.Established,
                    Name = publisher.Name
                };

            _bookstore.AddPublisher(publisherToAdd);
            _bookstore.Save();

            return CreatedAtRoute("GetPublisher", new { id = publisherToAdd.Id }, publisherToAdd);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]PublisherUpdateDTO publisher)
        {
            if (publisher == null) return BadRequest();

            // Define possible error states
            if (publisher.Established < 1534)
                ModelState.AddModelError("Established", "The first publishing house was founded in 1543.");
            // If there is an error state, kick out a bad request
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var publisherExists = _bookstore.PublisherExists(id);
            if (!publisherExists) return NotFound();

            _bookstore.UpdatePublisher(id, publisher);
            _bookstore.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]JsonPatchDocument<PublisherUpdateDTO> publisher)
        {
            if (publisher == null) return BadRequest();

            var publisherToUpdate = _bookstore.GetPublisher(id);
            var publisherPatch = new PublisherUpdateDTO()
                                        {
                                            Name = publisherToUpdate.Name,
                                            Established = publisherToUpdate.Established
                                        };
            publisher.ApplyTo(publisherPatch, ModelState);
            if (publisherPatch.Established < 1534)
                ModelState.AddModelError("Established", "The first publishing house was founded in 1543.");
            // If there is an error state, kick out a bad request
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _bookstore.UpdatePublisher(id, publisherPatch);
            _bookstore.Save();
            return NoContent();
        }


        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {

        }

        private IActionResult VerifyPublisher(int id, PublisherCreateDTO publisher)
        {
            if (publisher == null) return BadRequest();

            // Define possible error states
            if (publisher.Established < 1534)
                ModelState.AddModelError("Established", "The first publishing house was founded in 1543.");
            // If there is an error state, kick out a bad request
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var publisherExists = _bookstore.PublisherExists(id);
            if (!publisherExists) return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Code to remove the resource
            return NoContent();
        }
    }
}