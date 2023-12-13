using DAL.Modelos;
using Microsoft.AspNetCore.Mvc;
using Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Controllers
{
    public class ControladorAdmin : Controller
    {
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

    }
}
