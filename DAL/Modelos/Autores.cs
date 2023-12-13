using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Modelos
{
    public class Autores
    {
        public Autores()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_autor { get; set; }
        public string nombre_autor { get; set; }
        public string apellidos_autor { get; set; }


        // Definición de la colección de libros
        public ICollection<Libros> Libros { get; set; }

        public Autores(string nombre_autor, string apellidos_autor, ICollection<Libros> libros)
        {
            this.nombre_autor = nombre_autor;
            this.apellidos_autor = apellidos_autor;
            Libros = libros;
        }

        public Autores(string nombre_autor, string apellidos_autor)
        {
            this.nombre_autor = nombre_autor;
            this.apellidos_autor = apellidos_autor;
        }
    }
}

