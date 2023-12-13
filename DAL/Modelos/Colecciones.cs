using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Modelos
{
    public class Colecciones
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_colecciones { get; set; }
        public string nombre_coleccion { get; set; }

        public Colecciones(string nombre_coleccion)
        {
            this.nombre_coleccion = nombre_coleccion;
        }
        public Colecciones()
        {

        }

        public Colecciones(int id_colecciones)
        {
            this.id_colecciones = id_colecciones;
        }
    }
}
