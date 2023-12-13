using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Modelos
{
    public class Editoriales
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_editoriales { get; set; }
        public string nombre_editorial { get; set; }

        // Propiedad de navegación para la relación con Libros
        public ICollection<Libros> Libros { get; set; }

        public Editoriales(string nombre_editorial)
        {
            this.nombre_editorial = nombre_editorial;
        }
        public Editoriales()
        {

        }

        public Editoriales(int id_editoriales)
        {
            this.id_editoriales = id_editoriales;
        }
    }
}
