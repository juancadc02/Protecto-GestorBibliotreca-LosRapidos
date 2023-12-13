using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Modelos
{
    public class Estamos_Prestamo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_estado_prestamo { get; set; }
        public int codigo_estado_prestamo { set; get; }
        public string descripcion_estado_prestamo { set; get; }

        public Estamos_Prestamo()
        {
        }

        public Estamos_Prestamo(int codigo_estado_prestamo, string descripcion_estado_prestamo)
        {
            this.codigo_estado_prestamo = codigo_estado_prestamo;
            this.descripcion_estado_prestamo = descripcion_estado_prestamo;
        }
    }
}
