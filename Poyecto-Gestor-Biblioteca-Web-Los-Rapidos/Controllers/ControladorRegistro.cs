using DAL.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Controllers
{
    /// <summary>
    /// Controlador encargado de registrar nuevos usuarios
    /// </summary>
    public class ControladorRegistro : Controller
    {
        /// <summary>
        /// Metodo encargado de abrir la vista de registrar usuarios
        /// </summary>
        /// <returns>La vista de registro para nuevo usuario</returns>
        public IActionResult Registro()
        {
            // Lógica de la acción (si es necesario)
            return View("~/Views/Registro/Registro.cshtml");
        }
        /// <summary>
        /// Metodo que inserta en base de datos un usuario con los parametro introducidos en el formulario.
        /// </summary>
        /// <param name="nombre_usuario"> nombre del nuevo usuario</param>
        /// <param name="apellidos_usuario">apellidos del nuevo usuario</param>
        /// <param name="dni_usuario">dni del usuario a registrar</param>
        /// <param name="tlf_usuario">telefono del usuario a registrar</param>
        /// <param name="email_usuario">email del usuario a registrar</param>
        /// <param name="clave_usuario">contraseña del usuario a registrar</param>
        /// <returns>ActionResult que redirige a la página de inicio de sesión después de registrar al usuario</returns>
        [HttpPost]
        public ActionResult RegistrarUsuarios(string nombre_usuario, string apellidos_usuario, string dni_usuario, string tlf_usuario, string email_usuario, string clave_usuario)
        {
            ServicioConsultas servicio = new ServicioConsultasImpl();

            // Verifica si el correo electrónico o el DNI ya existen en la base de datos
            if (servicio.existeCorreoElectronico(email_usuario) || servicio.existeDNI(dni_usuario))
            {
                TempData["ErrorRegistro"] = "El correo electrónico o el DNI ya están registrados.";
                return RedirectToAction("Registro", "ControladorRegistro"); // Puedes redirigir a una vista de error o a la misma página de registro
            }

            // Si el correo electrónico y el DNI no existen, procede con el registro
            DateTime fechaActual = DateTime.Now.ToUniversalTime();
          
            Usuarios usuariosNuevo = new Usuarios(dni_usuario, nombre_usuario, apellidos_usuario, tlf_usuario, email_usuario, clave_usuario, fechaActual);
            usuariosNuevo.id_acceso = 1;
            servicio.registrarUsuario(usuariosNuevo);
            TempData["MensajeRegistroExitoso"] = "Usuario registrado con éxito.";
            return RedirectToAction("Login", "ControladorIniciarSesion");
        }
    }
}
