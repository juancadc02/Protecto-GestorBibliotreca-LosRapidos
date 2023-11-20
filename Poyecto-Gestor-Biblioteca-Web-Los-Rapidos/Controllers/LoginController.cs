using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Controllers
{
    public class LoginController : Controller
    {

        
        // GET: LoginController
        public IActionResult Login()
        {
            // Lógica de la acción (si es necesario)
            return View("~/Views/Home/Login.cshtml");// Devuelve la vista asociada
        }

        public IActionResult Registro()
        {
            // Lógica de la acción (si es necesario)
            return View("~/Views/Home/Registro.cshtml");
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
