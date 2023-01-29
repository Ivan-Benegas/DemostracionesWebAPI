using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using WebApiPubs.Models;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        // Inyección de dependencia --- inicia

        // Propiedad
        private readonly pubsContext context;

        public PublisherController(pubsContext context)
        {
            this.context = context;
        }

        // Inyección de dependencia --- fin


        // GET: api/publisher
        [HttpGet]
        public ActionResult<IEnumerable<Models.Publisher>> Get()
        {
            return context.Publishers.ToList();
        }


        // GET api/publisher/1
        [HttpGet("{id}")]
        public ActionResult<Models.Publisher> GetById(string id)
        {
            /*Models.Publisher publisher = (from a in context.Publishers
                                   where a.PubId == id
                           select a).SingleOrDefault();*/

            var publisher = context.Publishers.Include(x => x.Titles).FirstOrDefault(x => x.PubId == id);

            return publisher;
        }


        // INSERT
        // POST api/publisher
        [HttpPost]
        public ActionResult Post(Models.Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Publishers.Add(publisher);
            context.SaveChanges();
            return Ok();
        }


        // UPDATE
        // PUT api/publisher/1
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Models.Publisher publisher)
        {
            if (id != publisher.PubId)
            {
                return BadRequest();
            }

            context.Entry(publisher).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }


        // DELETE api/publisher/1
        [HttpDelete("{id}")]
        public ActionResult<Models.Publisher> Delete(string id)
        {
            var publisher = (from a in context.Publishers
                         where a.PubId == id
                         select a).SingleOrDefault();

            if (publisher == null)
            {
                return NotFound();
            }

            context.Publishers.Remove(publisher);
            context.SaveChanges();
            return publisher;
        }


    }
}
