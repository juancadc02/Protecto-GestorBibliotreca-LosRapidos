using DAL.Modelos;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class GestorBibliotecaDbContext:DbContext
    {

        public GestorBibliotecaDbContext(DbContextOptions options) : base(options)
        {
        }

        public GestorBibliotecaDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=Proyecto-GestorBiblioteca-LosRapidos;User Id=postgres;Password=; SearchPath=public");

        }
        public DbSet<Usuarios> Usuarios { get; set; }

    }
}