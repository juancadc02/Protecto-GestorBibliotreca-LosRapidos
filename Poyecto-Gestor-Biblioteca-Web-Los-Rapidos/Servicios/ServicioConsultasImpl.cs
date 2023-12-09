using DAL;
using DAL.Modelos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios
{
    /// <summary>
    /// Clase que implementa la interfaz ServicioConsultas y detalla la lógica de los métodos.
    /// </summary>
    public class ServicioConsultasImpl : ServicioConsultas
    {
        private readonly servicioEncriptar servicioEncriptar = new servicioEncriptarImpl();

        /// <summary>
        /// Registra un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="nuevoUsuario">Objeto Usuarios que representa al nuevo usuario a registrar.</param>
        public void registrarUsuario(Usuarios nuevoUsuario)
        {
            using (var contexto = new GestorBibliotecaDbContext())
            {
                nuevoUsuario = new Usuarios
                {
                    dni_usuario = nuevoUsuario.dni_usuario,
                    nombre_usuario = nuevoUsuario.nombre_usuario,
                    apellidos_usuario = nuevoUsuario.apellidos_usuario,
                    tlf_usuario = nuevoUsuario.tlf_usuario,
                    email_usuario = nuevoUsuario.email_usuario,
                    clave_usuario = servicioEncriptar.Encriptar(nuevoUsuario.clave_usuario),
                    fch_alta_usuario = nuevoUsuario.fch_alta_usuario,
                };

                contexto.Add(nuevoUsuario);
                contexto.SaveChanges();
                Console.WriteLine("\n\n\t Usuario insertado correctamente");
            }
        }

        /// <summary>
        /// Envía un correo electrónico desde una dirección de origen a una dirección de destino.
        /// </summary>
        /// <param name="emailOrigen">Dirección de correo electrónico de origen.</param>
        /// <param name="emailDestino">Dirección de correo electrónico de destino.</param>
        public void enviarCorreoElectronico(string gmailEnvio, string gmailRecibe, string asunto, string htmlContent)
        {
            //string htmlContent = System.IO.File.ReadAllText("/Pantilla/RecuperacionContraseñaCorreo.html");
            using (var mail = new MailMessage())
            {
                mail.From = new MailAddress(gmailEnvio);
                mail.To.Add(new MailAddress(gmailRecibe));
                mail.Subject = asunto;

                // Cuerpo del correo en formato HTML
                mail.Body = htmlContent;
                mail.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    // Configuración del servidor SMTP (en este caso, para Gmail)
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new NetworkCredential(gmailEnvio, ""); // Reemplaza "TuContraseña" con tu contraseña real
                    smtp.EnableSsl = true;

                    try
                    {
                        smtp.Send(mail);
                        Console.WriteLine("Correo enviado");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Se ha producido un error: " + ex.Message);
                    }
                }
            }
        }
    }

}





