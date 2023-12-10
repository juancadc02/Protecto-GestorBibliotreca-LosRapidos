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
            // Genera un token único para el usuario y la recuperación.
            string token = _encriptarServicio.Encriptar(Guid.NewGuid().ToString());

            // Si pasa la validación busca al usuario por su dirección de correo electrónico.
            var user = _contexto.Usuarios.Where(u => u.email_usuario == modelo.Email).FirstOrDefault();

            if (user != null)
            {
                // Asigna el token al usuario y actualiza la base de datos.
                user.token_recuperacion = token;
                _contexto.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _contexto.Usuarios.Update(user);
                _contexto.SaveChanges();

                // Envia un correo electrónico con el enlace de recuperación.
                EnviarEmail(user.email_usuario, token);
                //Mostramos mensaje de que el correo ha sido enviado
                TempData["MensajeExito"] = "Correo recuperacion de contraseña enviado correctamente.";
                //Redirigimos al login
                return RedirectToAction("Login", "ControladorIniciarSesion");

            }
            else
            {
                TempData["MensajeError"] = "Usuario no encontrado. La recuperación de contraseña ha fallado.";
                return RedirectToAction("IniciarRecuperacion", "ControladorRecuperarContraseña");
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

            if (user != null)
            {
                // Si se encuentra dicho usuario se actualiza la contraseña y elimina el token de recuperación para que no se vuelva a usar.
                user.clave_usuario = _encriptarServicio.Encriptar(modelo.Password);
                user.token_recuperacion = null;
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
        private void EnviarEmail(string emailDestino, string token)
        {
            string urlDominio = "https://localhost:7186";

            string EmailOrigen = "@gmail.com";
            string urlDeRecuperacion = String.Format("{0}/ControladorRecuperarContraseña/Recuperar/?token={1}", urlDominio, token);

            string cuerpoCorreo = String.Format(@"<!DOCTYPE html>
                                        <html lang='es'>
                                        <body>
                                            <div style='width:600px;padding:20px;border:1px solid #DBDBDB;border-radius:12px;font-family:Sans-serif'>
                                                <h1 style='color:#C76F61'>Restablecer contraseña</h1>
                                                <p style='margin-bottom:25px'>Estimado/a&nbsp;<b>{0}</b>:</p>
                                                <p style='margin-bottom:25px'>Se solicitó un restablecimiento de contraseña para tu cuenta, haz clic en el botón que aparece a continuación para cambiar tu contraseña.</p>
                                                <a style='padding:12px;border-radius:12px;background-color:#6181C7;color:#fff;text-decoration:none' href='{1}' target='_blank'>Cambiar contraseña</a>
                                                <p style='margin-top:25px'>Gracias.</p>
                                            </div>
                                        </body>
                                        </html>", emailDestino, urlDeRecuperacion);

            MailMessage mensajeDelCorreo = new MailMessage(EmailOrigen, emailDestino, "Recuperación de contraseña", cuerpoCorreo);

            mensajeDelCorreo.IsBodyHtml = true;

            SmtpClient oSmtpCliente = new SmtpClient("smtp.gmail.com");
            oSmtpCliente.EnableSsl = true;
            oSmtpCliente.UseDefaultCredentials = false;
            oSmtpCliente.Port = 587;
            oSmtpCliente.Credentials = new System.Net.NetworkCredential(EmailOrigen, "");

            oSmtpCliente.Send(mensajeDelCorreo);

            oSmtpCliente.Dispose();
        }

        #endregion
    }




}

