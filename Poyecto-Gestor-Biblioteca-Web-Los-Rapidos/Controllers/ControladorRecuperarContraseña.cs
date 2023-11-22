using Microsoft.AspNetCore.Mvc;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Controllers
{
    public class ControladorRecuperarContraseña : Controller
    {
        /// <summary>
        /// Metodo encargado de abrir la vista de recuperar contraseña
        /// </summary>
        /// <returns></returns>
        public IActionResult irARecuperarContraseña()
        {
            // Lógica de la acción (si es necesario)
            return View("~/Views/Registro/RecuperarContraseña.cshtml");
        }
    }
}
