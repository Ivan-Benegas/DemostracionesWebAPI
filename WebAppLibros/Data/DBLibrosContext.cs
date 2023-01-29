using Microsoft.EntityFrameworkCore; // Agregar
using WebAppLibros.Models; // Agregar

namespace WebAppLibros.Data
{
    public class DBLibrosContext : DbContext
    {
        // Constructor
        public DBLibrosContext(DbContextOptions<DBLibrosContext> options) : base(options) { }

        // Propiedades

        public DbSet<Autor> Autores { get; set; }

        public DbSet<Libro> Libros { get; set; }

    }
}
