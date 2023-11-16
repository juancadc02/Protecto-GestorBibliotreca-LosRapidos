using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Modelos
{
    public class Usuarios
    {
      

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_usuario { get; set; }
        public string dni_usuario { set; get; }
        public string nombre_usuario { set; get; }
        public string apellidos_usuario { set; get; }
        public string tlf_usuario { set; get; }
        public string email_usuario { get; set; }
        public string clave_usuario { set; get; }
        public bool estaBloqueado_usuario { get; set; }
        public DateTime fch_fin_bloqueo_usuario { get; set; }
        public DateTime fch_alta_usuario { get; set; }
        public DateTime fch_baja_usuario { get; set; }



        #region Constructores
        public Usuarios()
        {
        }
        public Usuarios(int id_usuario, string dni_usuario, string nombre_usuario, string apellidos_usuario, string tlf_usuario, string email_usuario, string clave_usuario, bool estaBloqueado_usuario, DateTime fch_fin_bloqueo_usuario, DateTime fch_alta_usuario, DateTime fch_baja_usuario)
        {
            this.id_usuario = id_usuario;
            this.dni_usuario = dni_usuario;
            this.nombre_usuario = nombre_usuario;
            this.apellidos_usuario = apellidos_usuario;
            this.tlf_usuario = tlf_usuario;
            this.email_usuario = email_usuario;
            this.clave_usuario = clave_usuario;
            this.estaBloqueado_usuario = estaBloqueado_usuario;
            this.fch_fin_bloqueo_usuario = fch_fin_bloqueo_usuario;
            this.fch_alta_usuario = fch_alta_usuario;
            this.fch_baja_usuario = fch_baja_usuario;
        }

        #endregion
    }
}
