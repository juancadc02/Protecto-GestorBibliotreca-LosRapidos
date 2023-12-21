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

            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=Proyecto-GestorBiblioteca-LosRapidos;User Id=postgres;Password=Juanccaaa1992; SearchPath=public");

        }
        public DbSet<Usuarios> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

            Usuarios admin = new Usuarios
            {
                id_usuario = 1,
                dni_usuario = "1",
                clave_usuario = "ac9689e2272427085e35b9d3e3e8bed88cb3434828b43b86fc0596cad4c6e270",
                nombre_usuario = "ADMIN",
                apellidos_usuario = "ADMIN",
                email_usuario = "admin@gmail.com",
                tlf_usuario = "1",
                fch_alta_usuario = DateTime.Now.ToUniversalTime(),
            };
            // Configuración de la relación entre Autores y Libros (muchos a muchos)
            modelBuilder.Entity<Autores>()
                .HasMany(a => a.Libros)
                .WithMany(l => l.Autores)
                .UsingEntity(j => j.ToTable("Rel_Autores_Libros"));

            // Otras configuraciones de tus relaciones aquí, si es necesario

            modelBuilder.Entity<Prestamo>()
               .HasMany(a => a.collectionLibro)
               .WithMany(l => l.Prestamos)
               .UsingEntity(j => j.ToTable("Rel_Libros_Prestamos"));

            modelBuilder.Entity<Usuarios>().HasData(admin);
            base.OnModelCreating(modelBuilder);

        }





        public DbSet<Autores> Autores { get; set; }
        public DbSet<Libros> Libros { get; set; }
        public DbSet<Editoriales> Editoriales { get; set; }
        public DbSet<Generos> Generos { get; set; }
        public DbSet<Colecciones> Colecciones { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Estamos_Prestamo> estamos_Prestamos { get; set; }
        public DbSet<Usuarios> Usuario { get; set; }

    }
}