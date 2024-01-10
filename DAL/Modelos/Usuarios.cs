using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Modelos
{
    /// <summary>
    /// Clase DAO que representa la tabla de usuarios
    /// </summary>
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
        [ForeignKey("Accesos")]
        public int id_accesos { get; set; }
        public string? token_recuperacion { get; set; }
        public DateTime? fecha_vencimiento_token { get; set; }
        public DateTime fch_alta_usuario { get; set; }
        public byte[]? imagen { get; set; }


        public Accesos Accesos { get; set; }


        #region Constructores
        public Usuarios()
        {
        }

        public Usuarios(string dni_usuario, string nombre_usuario, string apellidos_usuario, string tlf_usuario, string email_usuario, string clave_usuario, DateTime fch_alta_usuario)
        {
            this.dni_usuario = dni_usuario;
            this.nombre_usuario = nombre_usuario;
            this.apellidos_usuario = apellidos_usuario;
            this.tlf_usuario = tlf_usuario;
            this.email_usuario = email_usuario;
            this.clave_usuario = clave_usuario;
            this.fch_alta_usuario = fch_alta_usuario;
        }

        public Usuarios(string dni_usuario, string nombre_usuario, string apellidos_usuario, string tlf_usuario, string email_usuario, string clave_usuario, DateTime fch_alta_usuario, string token, DateTime? fecha_vencimiento_token, byte[] imagen)
        {
            this.dni_usuario = dni_usuario;
            this.nombre_usuario = nombre_usuario;
            this.apellidos_usuario = apellidos_usuario;
            this.tlf_usuario = tlf_usuario;
            this.email_usuario = email_usuario;
            this.clave_usuario = clave_usuario;
            this.fch_alta_usuario = fch_alta_usuario;
            this.token_recuperacion = token;
            this.fecha_vencimiento_token = fecha_vencimiento_token;
            this.imagen = imagen;
        }

        public Usuarios(string dni_usuario, string nombre_usuario, string apellidos_usuario, string tlf_usuario, string email_usuario ,byte[] imagen)
        {
            this.dni_usuario = dni_usuario;
            this.nombre_usuario = nombre_usuario;
            this.apellidos_usuario = apellidos_usuario;
            this.tlf_usuario = tlf_usuario;
            this.email_usuario = email_usuario;
            this.imagen = imagen;
        }

        public Usuarios(int id_usuario, string dni_usuario, string nombre_usuario, string apellidos_usuario, string tlf_usuario, string email_usuario, string clave_usuario, int id_accesos, string? token_recuperacion, DateTime? fecha_vencimiento_token, DateTime fch_alta_usuario, byte[]? imagen, Acceso accesos)
        {
            this.dni_usuario = dni_usuario;
            this.nombre_usuario = nombre_usuario;
            this.apellidos_usuario = apellidos_usuario;
            this.tlf_usuario = tlf_usuario;
            this.email_usuario = email_usuario;
            this.clave_usuario = clave_usuario;
            this.id_accesos = id_accesos;
            this.token_recuperacion = token_recuperacion;
            this.fecha_vencimiento_token = fecha_vencimiento_token;
            this.fch_alta_usuario = fch_alta_usuario;
            this.imagen = imagen;
            Accesos = accesos;
        }






        #endregion
    }
}
