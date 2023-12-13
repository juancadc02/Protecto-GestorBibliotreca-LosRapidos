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
        public IActionResult irUsuario()
        {
            //Creamos una lista y llamamos al metodo lista usuarios.
            List<Usuarios> listaUsuario=servicio.listartUsuarios();
            ViewData["listaUsuario"] = listaUsuario;

            return View("~/Views/Home/listaUsuarios.cshtml");
        }
        public IActionResult irPrestamo()
        {
           
            List<Prestamo> listaPrestamo =servicio.listarPrestamo();
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
        public ControladorAdmin(GestorBibliotecaDbContext context)
        {
            _context = context;
        }
    }
}
