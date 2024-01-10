using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Modelos
{
    public class Accesos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_accesos { get; set; }
        public string codigo_acceso { get; set; }
        public string descripcion_acceso { get; set; }


        public Acceso()
        {
            
        }

        public Acceso(int id_accesos, string codigo_acceso, string descripcion_acceso)
        {
            this.id_accesos = id_accesos;
            this.codigo_acceso = codigo_acceso;
            this.descripcion_acceso = descripcion_acceso;
        }
    }
}
