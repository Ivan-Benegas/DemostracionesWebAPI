using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApiPubs.Models;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        // Inyección de dependencia --- inicia

        // Propiedad
        private readonly pubsContext context;

        public StoreController(pubsContext context)
        {
            this.context = context;
        }

        // Inyección de dependencia --- fin


        // GET: api/store
        [HttpGet]
        public ActionResult<IEnumerable<Store>> Get()
        {
            return context.Stores.ToList();
        }


        // GET api/store/1
        [HttpGet("{id}")]
        public ActionResult<Store> GetById(string id)
        {
            Store store = (from a in context.Stores
                           where a.StorId == id
                           select a).SingleOrDefault();

            return store;
        }


        // INSERT
        // POST api/store
        [HttpPost]
        public ActionResult Post(Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Stores.Add(store);
            context.SaveChanges();
            return Ok();
        }


        // UPDATE
        // PUT api/store/1
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Store store)
        {
            if (id != store.StorId)
            {
                return BadRequest();
            }

            context.Entry(store).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }


        // DELETE api/store/1
        [HttpDelete("{id}")]
        public ActionResult<Store> Delete(string id)
        {
            var store = (from a in context.Stores
                         where a.StorId == id
                         select a).SingleOrDefault();

            if (store == null)
            {
                return NotFound();
            }

            context.Stores.Remove(store);
            context.SaveChanges();
            return store;
        }


        //GET: api/store/name/juan
        [HttpGet("name/{name}")] //Ruta personalizada
        public ActionResult<IEnumerable<Store>> GetByName(string name)
        {
            List<Store> stores = (from a in context.Stores
                                  where a.StorName == name
                                   select a).ToList();
            return stores;
        }


        //GET: api/store/city&state/rosario/sf
        [HttpGet("city&state/{city}/{state}")] //Ruta personalizada
        public ActionResult<IEnumerable<Store>> GetByCityState(string city, string state)
        {
            List<Store> stores = (from a in context.Stores
                                  where a.City == city
                                  && a.State == state
                                  select a).ToList();
            return stores;
        }


        //GET: api/store/zip/200
        [HttpGet("zip/{zip}")] //Ruta personalizada
        public ActionResult<IEnumerable<Store>> GetByZip(string zip)
        {
            List<Store> stores = (from a in context.Stores
                                  where a.Zip == zip
                                  select a).ToList();
            return stores;
        }


    }
}
