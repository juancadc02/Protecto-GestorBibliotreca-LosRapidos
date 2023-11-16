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

            optionsBuilder.UseNpgsql("Server=localhost;Port=5566;Database=gestorBibliotecaPersonal;User Id=postgres;Password=Alcerreca001139_; SearchPath=public");

        }
        public DbSet<Usuarios> Usuarios { get; set; }

    }
}