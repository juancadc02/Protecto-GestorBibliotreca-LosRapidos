using DAL.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Controllers
{
    public class LoginController : Controller
    {
        const string urlApi = "https://localhost:7268/api/ControladorUsuarios";


        // GET: LoginController
        public IActionResult Login()
        {

            ViewBag.MensajeRegistroExitoso = TempData["MensajeRegistroExitoso"] as string;

            // Lógica de la acción (si es necesario)
            return View("~/Views/Home/Login.cshtml");// Devuelve la vista asociada
        }

        public IActionResult Registro()
        {
            // Lógica de la acción (si es necesario)
            return View("~/Views/Home/Registro.cshtml");
        }

        [HttpPost]
        public ActionResult Login(string nombre_usuario, string clave_usuario)
        {
            ServicioConsultas servicio = new ServicioConsultasImpl();
            bool loginExitoso = servicio.loginUsuario(nombre_usuario, clave_usuario, urlApi);

            if (loginExitoso)
            {
                // Muestra un mensaje de éxito
                ViewBag.MensajeLoginExitoso = "Inicio de sesión exitoso.";
            }
            else
            {
                // Muestra un mensaje de error si el inicio de sesión falla
                ViewBag.MensajeLoginError = "Credenciales incorrectas. Por favor, inténtalo de nuevo.";
            }

            ViewBag.MensajeRegistroExitoso = TempData["MensajeRegistroExitoso"] as string;

            return View("~/Views/Home/Login.cshtml");


        }

        [HttpPost]
        public ActionResult InsertarDatos(string nombre_usuario, string apellidos_usuario, string dni_usuario, string tlf_usuario, string email_usuario, string clave_usuario)
        {
            ServicioConsultas servicio = new ServicioConsultasImpl();
            // Validaciones y lógica adicional aquí
            DateTime fechaActual = DateTime.Now.ToUniversalTime();
            Usuarios usuariosNuevo = new Usuarios(dni_usuario,nombre_usuario,apellidos_usuario,tlf_usuario,email_usuario,clave_usuario,fechaActual);
            servicio.registrarUsuario(usuariosNuevo);

            // Redirige a otra página o realiza alguna acción después de registrar al usuario
            TempData["MensajeRegistroExitoso"] = "Usuario registrado con éxito.";

            return RedirectToAction("Login");
        }




        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
