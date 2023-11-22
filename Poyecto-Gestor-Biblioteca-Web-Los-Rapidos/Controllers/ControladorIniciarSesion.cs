using DAL.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Controllers
{
    public class ControladorIniciarSesion : Controller
    {
        const string urlApi = "https://localhost:7268/api/ControladorUsuarios";



        /// <summary>
        /// Metodo encargado de abrir la vista del login
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {

            ViewBag.MensajeRegistroExitoso = TempData["MensajeRegistroExitoso"] as string;

            // Lógica de la acción (si es necesario)
            return View("~/Views/Home/Login.cshtml");// Devuelve la vista asociada
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
