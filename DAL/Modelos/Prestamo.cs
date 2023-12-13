using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Modelos
{
    public class Prestamo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_prestamos { get; set; }
        [ForeignKey("libro")]
        public int id_libro { get; set; }
        [ForeignKey("usuario")]
        public int id_usuario { get; set; }
        public DateTime fch_inicio_prestamo { get; set; }
        public DateTime fch_fin_prestamo { get; set; }
        public DateTime fch_entrega_prestamo { get; set; }
        [ForeignKey("estado")]
        public int id_estado_prestamo { get; set; }

        //Propiedades tablas 

        public ICollection<Libros> collectionLibro { get; set; }

        public Usuarios usuario { get; set; }

        public Estamos_Prestamo estado { get; set; }

        public Prestamo()
        {
        }

        public Prestamo(int id_libro, int id_usuario, DateTime fch_inicio_prestamo, DateTime fch_fin_prestamo, DateTime fch_entrega_prestamo, int id_estado_prestamo, ICollection<Libros> collectionLibro)
        {
            this.id_libro = id_libro;
            this.id_usuario = id_usuario;
            this.fch_inicio_prestamo = fch_inicio_prestamo;
            this.fch_fin_prestamo = fch_fin_prestamo;
            this.fch_entrega_prestamo = fch_entrega_prestamo;
            this.id_estado_prestamo = id_estado_prestamo;
            this.collectionLibro = collectionLibro;
        }
    }
}

