using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Models;
using Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios;
using System.Net;
using System.Net.Mail;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Controllers
{
    /// <summary>
    /// Controlador para manejar la recuperación de contraseñas.
    /// </summary>
    public class ControladorRecuperarContraseña : Controller
    {
        private readonly GestorBibliotecaDbContext _contexto;
        private readonly servicioEncriptar _encriptarServicio;

        /// <summary>
        /// Constructor del controlador, con inyeccion de dependencias.
        /// </summary>
        /// <param name="dbContext">Contexto de la base de datos.</param>
        /// <param name="encriptarServicio">Servicio para encriptar.</param>
        public ControladorRecuperarContraseña
        (
            GestorBibliotecaDbContext dbContext,
            servicioEncriptar encriptarServicio
        )
        {
            _contexto = dbContext;
            _encriptarServicio = encriptarServicio;
        }

        /// <summary>
        /// Acción HTTP GET para mostrar la vista de inicio de recuperación.
        /// </summary>
        /// <returns>Vista de inicio de recuperación.</returns>
        [HttpGet]
        public IActionResult IniciarRecuperacion()
        {
            IniciarRecuperacionViewModel modelo = new IniciarRecuperacionViewModel();
            return View("~/Views/Registro/IniciarRecuperacion.cshtml");
        }

        /// <summary>
        /// Acción HTTP POST para procesar la solicitud de inicio de recuperación.
        /// </summary>
        /// <param name="modelo">Modelo de la vista de inicio de recuperación.</param>
        /// <returns>Vista correspondiente después del procesamiento.</returns>
        [HttpPost]
        public IActionResult IniciarRecuperacion(IniciarRecuperacionViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                // Si el el email de recuperacion no pasa la validación, vuelve a mostrar la vista con los errores.
                return View("~/Views/Registro/IniciarRecuperacion.cshtml", modelo);
            }

            // Si pasa la validación busca al usuario por su dirección de correo electrónico.
            var user = _contexto.Usuarios.Where(u => u.email_usuario == modelo.Email).FirstOrDefault();

            if (user != null) //Entra en el IF solo si el usuario existe con el email introducido.
            {
                // Genera un token único para el usuario y la recuperación.
                string token = _encriptarServicio.Encriptar(Guid.NewGuid().ToString());
                // Asigna el token al usuario y actualiza la base de datos.
                user.token_recuperacion = token;
                _contexto.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _contexto.Usuarios.Update(user);
                _contexto.SaveChanges();

                // Envia un correo electrónico con el enlace de recuperación.
                EnviarEmail(user.email_usuario, user.apellidos_usuario + ", " + user.nombre_usuario, token);
                //Mostramos mensaje de que el correo ha sido enviado
                TempData["MensajeExito"] = "Correo recuperacion de contraseña enviado correctamente.";
                //Redirigimos al login
                return View("~/Views/Home/Login.cshtml");

            }
            else
            {
                TempData["MensajeError"] = "La recuperación de contraseña ha fallado. Verifique la información ingresada.";
                return View("~/Views/Registro/IniciarRecuperacion.cshtml");
            }

        }

        /// <summary>
        /// Acción HTTP GET para mostrar la vista de recuperación.
        /// </summary>
        /// <param name="token">Token asociado a la recuperación.</param>
        /// <returns>Vista de recuperación.</returns>
        [HttpGet]
        public IActionResult Recuperar(string token)
        {
            // Crea un modelo de vista y asigna el token.
            RecuperarViewModel model = new RecuperarViewModel();
            model.Token = token;


            if (model.Token == null || model.Token == String.Empty)
            {
                // Si el token es nulo o vacío, muestra la vista de recuperación.
                ViewBag.TokenNoValido = "El token no es válido";
                return View("~/Views/Registro/Recuperar.cshtml");
            }

            // Busca al usuario por el token en la base de datos.
            var user = _contexto.Usuarios.Where(u => u.token_recuperacion == model.Token).FirstOrDefault();

            if (user == null)
            {
                // Si el usuario no es válido, muestra un mensaje y redirige al login.
                ViewBag.TokenNoValido = "El token ha expirado";
                return View("~/Views/Home/Login.cshtml");
            }

            return View("~/Views/Registro/Recuperar.cshtml");
        }

        /// <summary>
        /// Acción HTTP POST para procesar la solicitud de recuperación.
        /// </summary>
        /// <param name="modelo">Modelo de la vista de recuperación.</param>
        /// <returns>Vista correspondiente después del procesamiento.</returns>
        [HttpPost]
        public IActionResult Recuperar(RecuperarViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                // Si la contraseña de recuperacion no pasa la validación, vuelve a mostrar la vista con los errores.
                return View("~/Views/Registro/Recuperar.cshtml", modelo);
            }

            // Si pasa la validación busca al usuario por el token en la base de datos y por el token que llega de la URL.
            var user = _contexto.Usuarios.Where(u => u.token_recuperacion == modelo.Token).FirstOrDefault();

            if (user != null) //Entra en el IF solo si el token del usuario se corresponde con el que recibió en el email de recuperación.
            {
                // Si se encuentra dicho usuario se actualiza la contraseña y elimina el token de recuperación para que no se vuelva a usar.
                user.clave_usuario = _encriptarServicio.Encriptar(modelo.Password);
                user.token_recuperacion = null; //Se establece a null para que el token reclamado ya no se pueda usar nunca más
                _contexto.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _contexto.Usuarios.Update(user);
                _contexto.SaveChanges();
            }

            ViewBag.ContraseñaModificada = "Contraseña modificada correctamente";

            return View("~/Views/Home/Login.cshtml");
        }

        #region METODOS EMAIL

        /// <summary>
        /// Envía un correo electrónico de recuperación de contraseña.
        /// </summary>
        /// <param name="emailDestino">Dirección de correo electrónico del usuario destinatario.</param>
        /// <param name="token">Token asociado a la recuperación.</param>
        private void EnviarEmail(string emailDestino, string nombreUser, string token)
        {
            string urlDominio = "https://localhost:7186";

            string EmailOrigen = "";
            //Se crea la URL de recuperación con el token que se enviará al mail del user.
            string urlDeRecuperacion = String.Format("{0}/ControladorRecuperarContraseña/Recuperar/?token={1}", urlDominio, token);

            //Hacemos que el texto del email sea un archivo html que se encuentra en la carpeta Plantilla.
            string directorioProyecto = System.IO.Directory.GetCurrentDirectory();
            string rutaArchivo = System.IO.Path.Combine(directorioProyecto, "Plantilla/RecuperacionContraseñaCorreo.html");
            string htmlContent = System.IO.File.ReadAllText(rutaArchivo);
            //Asignamos el nombre de usuario que tendrá el cuerpo del mail y el URL de recuperación con el token al HTML.
            htmlContent = String.Format(htmlContent, nombreUser, urlDeRecuperacion);

            MailMessage mensajeDelCorreo = new MailMessage(EmailOrigen, emailDestino, "RESTABLECER CONTRASEÑA", htmlContent);

            mensajeDelCorreo.IsBodyHtml = true;

            SmtpClient smtpCliente = new SmtpClient("smtp.gmail.com");
            smtpCliente.EnableSsl = true;
            smtpCliente.UseDefaultCredentials = false;
            smtpCliente.Port = 587;
            smtpCliente.Credentials = new System.Net.NetworkCredential(EmailOrigen, "");

            smtpCliente.Send(mensajeDelCorreo);

            smtpCliente.Dispose();
        }
        #endregion
    }




}

