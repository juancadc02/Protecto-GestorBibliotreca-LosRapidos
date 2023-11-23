using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Controllers
{
    public class ControladorRecuperarContraseña : Controller
    {
        /// <summary>
        /// Metodo encargado de abrir la vista de recuperar contraseña
        /// </summary>
        /// <returns>ActionResult que redirige a la página de recuperar contraseña del usuario</returns>
        public IActionResult irARecuperarContraseña()
        {
            return View("~/Views/Registro/RecuperarContraseña.cshtml");
        }

        [HttpPost]
        public IActionResult recuperarContraseña(string email_usuario)
        {
            string emailEnvio = "correoDelQueEsEnviado";

            //Implementacion del servicioConsultas
            ServicioConsultas consultas = new ServicioConsultasImpl();
           
            //Hacemos que el texto del email sea un archivo html.
            string directorioProyecto = System.IO.Directory.GetCurrentDirectory();
            string rutaArchivo = System.IO.Path.Combine(directorioProyecto, "Plantilla/RecuperacionContraseñaCorreo.html");
            string htmlContent = System.IO.File.ReadAllText(rutaArchivo);

            //Comprobamos si existe el correo electronico al que enviar el mensaje en la base de datos.
            bool existeCorreo = VerificarCorreoExistente(email_usuario);
            
            //Si existe el usuario en la base de datos, se manda el correo y aparece el mensaje de correo enviado
            if(existeCorreo)
            {
                consultas.enviarCorreoElectronico(emailEnvio, email_usuario, "Hola", htmlContent);
                TempData["MensajeRecuperarExitoso"] = "Se ha enviado el correo electronico";
                return View("~/Views/Registro/RecuperarContraseña.cshtml");
            }
            //Si no existe muestra mensaje de error.
            else
            {
                TempData["MensajeRecuperarContraseñaError"] = "El correo electronico no existe.";
                return View("~/Views/Registro/RecuperarContraseña.cshtml");

            }



        }

        //Metodo para verificar si existe el correo en la base de datos.
        public bool VerificarCorreoExistente(string email_usuario)
        {
            // Verificar si existe el correo en la base de datos
            return _dbContext.Usuarios.Any(u => u.email_usuario == email_usuario);
        }

        private readonly GestorBibliotecaDbContext _dbContext;

        public ControladorRecuperarContraseña(GestorBibliotecaDbContext dbContext)
        {
            _dbContext =dbContext ;
        }

    }
}
