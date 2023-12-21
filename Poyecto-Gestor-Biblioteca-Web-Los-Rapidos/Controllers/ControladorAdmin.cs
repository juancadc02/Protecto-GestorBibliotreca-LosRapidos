using DAL;
using DAL.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Controllers
{
    public class ControladorAdmin : Controller
    {
        private readonly GestorBibliotecaDbContext _context;
        ServicioConsultas servicio = new ServicioConsultasImpl();
        public IActionResult irAdmin()
        {
            return View("~/Views/Home/Administracion.cshtml");
        }
        //metodo que muestra la lista de usuarios
        public IActionResult irUsuario()
        {
            //Creamos una lista y llamamos al metodo lista usuarios.
            List<Usuarios> listaUsuario = servicio.listartUsuarios();
            ViewData["listaUsuario"] = listaUsuario;

            return View("~/Views/Home/listaUsuarios.cshtml");
        }
        //Metodo que muestra la lista de prestamos
        public IActionResult irPrestamo()
        {

            List<Prestamo> listaPrestamo = servicio.listarPrestamo();
            ViewData["listaPrestamo"] = listaPrestamo;
            return View("~/Views/Home/listaPrestamos.cshtml");
        }
        public IActionResult EditarUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id); // Ajusta según tu modelo de datos
            return RedirectToAction("MostrarUsuario", new { id = usuario.id_usuario });
        }
        public IActionResult MostrarUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View("~/Views/Home/MostrarUsuario.cshtml", usuario);
        }

        //Metodo que modifica los usuarios.
        [HttpPost]
        public ActionResult ModificarUsuario(Usuarios usuario, IFormFile imagen)
        {
            if (ModelState.IsValid)
            {
                // Utiliza la instancia de _context en lugar de crear una nueva
                var usuarioExistente = _context.Usuarios.Find(usuario.id_usuario);
                Console.WriteLine(usuarioExistente.id_usuario);
                if (usuarioExistente != null)
                {
                    // Actualiza los campos
                    usuarioExistente.nombre_usuario = usuario.nombre_usuario;
                    usuarioExistente.apellidos_usuario = usuario.apellidos_usuario;
                    usuarioExistente.dni_usuario = usuario.dni_usuario;
                    usuarioExistente.tlf_usuario = usuario.tlf_usuario;
                    usuarioExistente.email_usuario = usuario.email_usuario;

                    // Actualiza la imagen si se ha subido una nueva
                    if (imagen != null && imagen.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            imagen.CopyTo(memoryStream);
                            usuarioExistente.imagen = memoryStream.ToArray();
                        }
                    }

                    // Guarda los cambios utilizando la misma instancia de _context
                    _context.SaveChanges();

                    // Redirecciona a una acción válida después de guardar
                    return RedirectToAction("irAdmin");
                }
            }

            // Si hay errores de validación o el usuario no existe, regresa al formulario
            return View("~/Views/Home/Administracion.cshtml");
        }

        //Metodo encargado de eliminar al usuario por su id.
        public IActionResult EliminarUsuario(int id)
        {
            var usuarioExistente = _context.Usuarios.Find(id);

            // Si el usuario no existe, regresa un error o manejo adecuado
            if (usuarioExistente == null)
            {
                return NotFound(); // Retorna un error 404 Not Found
            }

            try
            {
                // Remover el usuario del contexto
                _context.Usuarios.Remove(usuarioExistente);

                _context.SaveChanges();

                return RedirectToAction("irAdmin");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("irAdmin"); // Otra vez a la vista de administración, o muestra un mensaje de error
            }
        }

        public ControladorAdmin(GestorBibliotecaDbContext context)
        {
            _context = context;
        }
    }
}
