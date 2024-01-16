using DAL;
using DAL.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.NewFolder1;
using Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios;
using System.Security.Claims;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Controllers
{
    [Authorize]
    public class ControladorAdmin : Controller
    {
        private readonly GestorBibliotecaDbContext _context;
        ServicioConsultas servicio = new ServicioConsultasImpl();
        public IActionResult irAdmin()
        {
          
            return RedirectToAction("Index", "Home");
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
            Console.WriteLine(ModelState.IsValid);
            if (ModelState.IsValid)
            {
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
                    return RedirectToAction("irUsuario");
                }
            }
            return RedirectToAction("irAdmin");
        }

        //Metodo encargado de eliminar al usuario por su id.
        public IActionResult EliminarUsuario(int id)
        {
            var usuarioExistente = _context.Usuarios.Find(id);

            if (usuarioExistente == null)
            {
                return NotFound(); 
            }

            try
            { 
                //Borramos el usuario y guardamos los cambios 
                _context.Usuarios.Remove(usuarioExistente);
                _context.SaveChanges();
                //Mostramos mensaje de exito y redirigimos al listado
                TempData["SuccessMessage"] = "El usuario se ha eliminado correctamente.";
                return RedirectToAction("irUsuario");
            }
            catch (Exception ex)
            {
                //Mostramos error en la consola y redirigimos a la administracion.
                Console.WriteLine(ex.Message);
                return RedirectToAction("irAdmin"); 
            }
        }
        public ControladorAdmin(GestorBibliotecaDbContext context)
        {
            _context = context;
        }
    }
}
