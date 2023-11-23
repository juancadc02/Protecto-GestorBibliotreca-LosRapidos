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
        /// Metodo encargado de abrir la vista del login
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {

            ViewBag.MensajeRegistroExitoso = TempData["MensajeRegistroExitoso"] as string;

            // Lógica de la acción (si es necesario)
            return View("~/Views/Home/Login.cshtml");// Devuelve la vista asociada
        }



        [HttpPost]
        public IActionResult InicioDeSesion(string nombre_usuario, string clave_usuario)
        {
            ServicioConsultas consultas = new ServicioConsultasImpl();
            // Validar los datos de entrada
            if (string.IsNullOrEmpty(nombre_usuario) || string.IsNullOrEmpty(clave_usuario))
            {
                // Datos de entrada no válidos, podrías redirigir a una página de error
                return RedirectToAction("Error", "Home");
            }

            // Realizar la autenticación en la base de datos
            if (IniciarSesion(nombre_usuario, clave_usuario))
            {
                // Inicio de sesión exitoso, redirigir a la página principal
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Autenticación fallida, podrías redirigir a una página de inicio de sesión con un mensaje de error

                ViewBag.MensajeLoginError = "Usuario o Contraseñ incorrecto !!";
                return View("~/Views/Home/Login.cshtml");// Devuelve la vista asociada
            }
        }
        private bool IniciarSesion(string nombre_usuario, string clave_usuario)
        {
            servicioEncriptarContraseña encriptarContraseña = new servicioEncriptarContraseñaImpl();

            // Utilizar Entity Framework para verificar las credenciales
            var usuario = dbContext.Usuarios
                .FirstOrDefault(u => u.nombre_usuario == nombre_usuario && u.clave_usuario == encriptarContraseña.EncriptarContraseña(clave_usuario));

            // Si el usuario es diferente de null, las credenciales son válidas
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