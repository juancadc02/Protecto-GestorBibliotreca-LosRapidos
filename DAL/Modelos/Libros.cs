using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Modelos
{
    public class Libros
    {
        public Libros()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_libro { get; set; }
        public string isbn_libro { get; set; }
        public string nombre_libro { get; set; }
        public string edicion_libro { get; set; }
        [ForeignKey("Editoriales")]
        public int id_editorial { get; set; }
        [ForeignKey("Generos")]
        public int id_genero { get; set; }
        [ForeignKey("Colecciones")]
        public int id_coleccion { get; set; }

        public byte[] imagen_libro { get; set; }

        public ICollection<Autores> Autores { get; set; }


        public ICollection<Prestamo> Prestamos { get; set; }
        public Editoriales Editoriales { get; set; }
        public Generos Generos { get; set; }
        public Colecciones Colecciones { get; set; }

        public Libros(int id_libro, string isbn_libro, string nombre_libro, string edicion_libro, int id_editorial, int id_genero, int id_coleccion, ICollection<Autores> autores, Editoriales editoriales, Generos generos, Colecciones colecciones)
        {
            this.id_libro = id_libro;
            this.isbn_libro = isbn_libro;
            this.nombre_libro = nombre_libro;
            this.edicion_libro = edicion_libro;
            this.id_editorial = id_editorial;
            this.id_genero = id_genero;
            this.id_coleccion = id_coleccion;
            Autores = autores;
            Editoriales = editoriales;
            Generos = generos;
            Colecciones = colecciones;
        }

        public Libros(string isbn_libro, string nombre_libro, string edicion_libro, int id_editorial, int id_genero, int id_coleccion, byte[] imagen_libro)
        {
            this.isbn_libro = isbn_libro;
            this.nombre_libro = nombre_libro;
            this.edicion_libro = edicion_libro;
            this.id_editorial = id_editorial;
            this.id_genero = id_genero;
            this.id_coleccion = id_coleccion;
            this.imagen_libro = imagen_libro;
        }

    }
}
