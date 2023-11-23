using Microsoft.AspNetCore.Mvc;

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
    }
}
