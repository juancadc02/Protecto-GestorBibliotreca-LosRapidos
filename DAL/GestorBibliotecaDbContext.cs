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

            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=Proyecto-GestorBiblioteca-LosRapidos;User Id=postgres;Password=1234; SearchPath=public");
            optionsBuilder.EnableSensitiveDataLogging();

        }
        public DbSet<Usuarios> Usuarios { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

           

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
        public DbSet<Acceso> Accesos { get; set; }
    }
}