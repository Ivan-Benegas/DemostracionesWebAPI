using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic; // Agregar
using System.Linq; // Agregar
using WebAppLibros.Data; // Agregar
using WebAppLibros.Models; //Agregar

namespace WebAppLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        // Inyección de dependencia --- inicia

        // Propiedad
        private readonly DBLibrosContext context;

        public AutorController(DBLibrosContext context)
        {
            this.context = context;
        }

        // Inyección de dependencia --- fin

        // GET: api/autor
        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            //return context.Autores.ToList();

            var result = context.Autores.Include(x => x.Libros).ToList();

            return result;

        }

        // GET api/autor/5
        [HttpGet("{id}")]
        public ActionResult<Autor> GetById(int id)
        {
            Autor autor = (from a in context.Autores
                           where a.IdAutor == id
                           select a).SingleOrDefault();

            return autor;
        }

        // INSERT
        // POST api/autor
        [HttpPost]
        public ActionResult Post(Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Autores.Add(autor);
            context.SaveChanges();
            return Ok();
        }

        // UPDATE
        // PUT api/autor/2
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Autor autor)
        {
            if (id != autor.IdAutor)
            {
                return BadRequest();
            }

            context.Entry(autor).State = EntityState.Modified;
            context.SaveChanges();
            return NoContent();
        }

        // DELETE api/autor/1
        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id)
        {
            var autor = (from a in context.Autores
                         where a.IdAutor == id
                         select a).SingleOrDefault();

            if (autor == null)
            {
                return NotFound();
            }

            context.Autores.Remove(autor);
            context.SaveChanges();
            return autor;
        }

        //GET: api/autor/33
        [HttpGet("listado/{edad}")] //Ruta personalizada
        public ActionResult<IEnumerable<Autor>> GetEdad(int edad)
        {
            List<Autor> autores = (from a in context.Autores
                                   where a.Edad == edad
                                   select a).ToList();
            return autores;
        }



    }
}
