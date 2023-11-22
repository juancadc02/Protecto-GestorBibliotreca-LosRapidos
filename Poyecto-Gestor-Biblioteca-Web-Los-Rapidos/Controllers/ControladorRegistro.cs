using DAL.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Controllers
{
    public class ControladorRegistro : Controller
    {
        /// <summary>
        /// Metodo encargado de abrir la vista de registrar usuarios
        /// </summary>
        /// <returns></returns>
        public IActionResult Registro()
        {
            // Lógica de la acción (si es necesario)
            return View("~/Views/Registro/Registro.cshtml");
        }

        /// <summary>
        /// Metodo que inserta en base de datos un usuario con los parametro introducidos en el formulario.
        /// </summary>
        /// <param name="nombre_usuario"></param>
        /// <param name="apellidos_usuario"></param>
        /// <param name="dni_usuario"></param>
        /// <param name="tlf_usuario"></param>
        /// <param name="email_usuario"></param>
        /// <param name="clave_usuario"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult RegistrarUsuarios(string nombre_usuario, string apellidos_usuario, string dni_usuario, string tlf_usuario, string email_usuario, string clave_usuario)
        {
            ServicioConsultas servicio = new ServicioConsultasImpl();
            // Validaciones y lógica adicional aquí
            DateTime fechaActual = DateTime.Now.ToUniversalTime();
            Usuarios usuariosNuevo = new Usuarios(dni_usuario, nombre_usuario, apellidos_usuario, tlf_usuario, email_usuario, clave_usuario, fechaActual);
            servicio.registrarUsuario(usuariosNuevo);

            // Redirige a otra página o realiza alguna acción después de registrar al usuario
            TempData["MensajeRegistroExitoso"] = "Usuario registrado con éxito.";

            return RedirectToAction("Login", "ControladorIniciarSesion");
        }
    }
}
