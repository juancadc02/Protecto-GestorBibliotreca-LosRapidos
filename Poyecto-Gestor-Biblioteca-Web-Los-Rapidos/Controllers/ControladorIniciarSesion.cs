using DAL;
using DAL.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Controllers
{
    public class ControladorIniciarSesion : Controller
    {
        private readonly GestorBibliotecaDbContext dbContext;

        const string urlApi = "https://localhost:7268/api/ControladorUsuarios";



        /// <summary>
        /// Metodo encargado de abrir la vista del login.
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {

            ViewBag.MensajeRegistroExitoso = TempData["MensajeRegistroExitoso"] as string;

            // Lógica de la acción (si es necesario)
            return View("~/Views/Home/Login.cshtml");// Devuelve la vista asociada
        }


        /// <summary>
        /// Metodo encargado de hacer el inicio de sesion con el email y contraseña recogidos del formulario
        /// </summary>
        /// <param name="email_usuario"></param>
        /// <param name="clave_usuario"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InicioDeSesion(string email_usuario, string clave_usuario)
        {
            ServicioConsultas consultas = new ServicioConsultasImpl();

            if (string.IsNullOrEmpty(email_usuario) || string.IsNullOrEmpty(clave_usuario))
            {
                return RedirectToAction("Error", "Home");
            }

            if (IniciarSesion(email_usuario, clave_usuario))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.MensajeLoginError = "Usuario o Contraseñ incorrecto !!";
                return View("~/Views/Home/Login.cshtml");
            }
        }

        /// <summary>
        /// Metodo que comprueba si el email y la clave del usuario estan registrados en la base de datos
        /// </summary>
        /// <param name="email_usuario"></param>
        /// <param name="clave_usuario"></param>
        /// <returns></returns>
        private bool IniciarSesion(string email_usuario, string clave_usuario)
        {
            servicioEncriptarContraseña encriptarContraseña = new servicioEncriptarContraseñaImpl();
            var usuario = dbContext.Usuarios
                .FirstOrDefault(u => u.email_usuario == email_usuario && u.clave_usuario == encriptarContraseña.EncriptarContraseña(clave_usuario));
            return usuario != null;
        }



        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Logout()
        {
            // Lógica para cerrar sesión aquí

            return RedirectToAction("Login");
        }
        public ControladorIniciarSesion(GestorBibliotecaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}