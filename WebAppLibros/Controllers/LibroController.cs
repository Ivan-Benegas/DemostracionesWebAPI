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
    public class LibroController : ControllerBase
    {

        // Inyección de dependencia --- inicia

        // Propiedad
        private readonly DBLibrosContext context;

        public LibroController(DBLibrosContext context)
        {
            this.context = context;
        }

        // Inyección de dependencia --- fin

        // GET: api/libro
        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Get()
        {
            // return context.Libros.ToList();

            var result = context.Libros.Include(x => x.Autor).ToList();

            return result;
        }

        // GET api/libro/5
        [HttpGet("{id}")]
        public ActionResult<Libro> GetById(int id)
        {
            /*Libro libro = (from a in context.Libros
                           where a.Id == id
                           select a).SingleOrDefault();*/

            var libro = context.Libros.Include(x => x.Autor).FirstOrDefault(x => x.Id == id);

            return libro;
        }

        // INSERT
        // POST api/libro
        [HttpPost]
        public ActionResult Post(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Libros.Add(libro);
            context.SaveChanges();
            return Ok();
        }

        // UPDATE
        // PUT api/libro/2
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest();
            }

            context.Entry(libro).State = EntityState.Modified;
            context.SaveChanges();
            return NoContent();
        }

        // DELETE api/libro/1
        [HttpDelete("{id}")]
        public ActionResult<Libro> Delete(int id)
        {
            var libro = (from a in context.Libros
                         where a.Id == id
                         select a).SingleOrDefault();

            if (libro == null)
            {
                return NotFound();
            }

            context.Libros.Remove(libro);
            context.SaveChanges();
            return libro;
        }

        //GET: api/libro/listadoporautorid/11
        [HttpGet("listadoporautorid/{id}")] //Ruta personalizada
        public ActionResult<IEnumerable<Libro>> GetAutorId(int id)
        {
            List<Libro> libros = (from a in context.Libros
                                   where a.AutorId == id
                                   select a).ToList();
            return libros;
        }





    }
}
