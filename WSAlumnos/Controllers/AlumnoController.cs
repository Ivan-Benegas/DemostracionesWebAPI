using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WSAlumnos.Models;

namespace WSAlumnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private List<Alumno> Listado()
        {
            List<Alumno> alumnos = new List<Alumno>()
            {
                new Alumno() { Id = 1, Apellido = "Perez", Nombre = "Maria"},
                new Alumno() { Id = 2, Apellido = "Rojo", Nombre = "Luis"},
                new Alumno() { Id = 3, Apellido = "Dorado", Nombre = "Marte"}
            };
            return alumnos;
        }

        // GET api/alumno
        [HttpGet]
        public IEnumerable<Alumno> Get()
        {
            return Listado();
        }

        // GET api/alumno/3
        [HttpGet("{id}")]
        public ActionResult<Alumno> GetById(int id)
        {
            var alumno = (from a in Listado()
                          where a.Id == id
                          select a).SingleOrDefault();

            return alumno;
        }


    }
}
