using Microsoft.AspNetCore.Mvc;
using Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Models;
using Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios;
using System.Diagnostics;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Controllers
{
    /// <summary>
    /// Controlador principal que gestiona las acciones relacionadas con la página principal y otras funcionalidades del sitio web.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ServicioConsultasImpl servicio;

        /// <summary>
        /// Constructor de la clase HomeController.
        /// </summary>
        /// <param name="logger">Instancia del registrador de eventos para la clase HomeController.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Acción que devuelve la vista de la página principal del sitio web.
        /// </summary>
        /// <returns>Vista de la página principal.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Acción que devuelve la vista de la página de privacidad del sitio web.
        /// </summary>
        /// <returns>Vista de la página de privacidad.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Acción que maneja los errores y devuelve la vista de error con detalles adicionales.
        /// </summary>
        /// <returns>Vista de error con detalles adicionales.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}