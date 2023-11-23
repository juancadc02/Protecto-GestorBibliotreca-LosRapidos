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
    /// Clase que implementa la interfaz ServicioConsultas y detalla la lógica de los métodos
    /// </summary>
    public class ServicioConsultasImpl : ServicioConsultas
    {
        servicioEncriptarContraseña servicioEncriptar = new servicioEncriptarContraseñaImpl();

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
                    clave_usuario = servicioEncriptar.EncriptarContraseña(nuevoUsuario.clave_usuario),
                    fch_alta_usuario = nuevoUsuario.fch_alta_usuario,

                };
                contexto.Add(nuevoUsuario);
                contexto.SaveChanges();
                Console.WriteLine("\n\n\t Usuario insertado correctamente");
            }
        }
        public void enviarCorreoElectronico(string emailOrigen, string emailDestino)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            mail.From = new MailAddress(emailOrigen);
            mail.To.Add(new MailAddress(emailDestino));

            // Cuerpo del correo en formato HTML
            string htmlBody = ObtenerContenidoHTMLDesdeVista("RecuperacionContraseñaCorreo.html");
            mail.Body = htmlBody;
            mail.IsBodyHtml = true;



            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential(emailOrigen, "Contreña");
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mail);
                Console.WriteLine("Correo enviado");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se ha producido un error: " + ex);
            }
        }

        private string ObtenerContenidoHTMLDesdeVista(string nombreVista)
        {
            // Aquí debes cargar el contenido HTML desde tu vista.
            // Puedes utilizar algún mecanismo de lectura de archivos, dependiendo de tu aplicación.

            // Ejemplo: leyendo el contenido de un archivo en la misma carpeta que el código
            string rutaVista = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plantilla", nombreVista);

            if (File.Exists(rutaVista))
            {
                return File.ReadAllText(rutaVista);
            }
            else
            {
                Console.WriteLine("La vista no se encontró.");
                return string.Empty;
            }
        }






    }


}


