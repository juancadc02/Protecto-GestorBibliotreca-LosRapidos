using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.NewFolder1
{
    public class AutorizarSesionAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var usuarioLogueado = context.HttpContext.Session.GetString("UsuarioLogueado");

            if (string.IsNullOrEmpty(usuarioLogueado))
            {
                // Redireccionar al usuario a la página de inicio de sesión si no hay un usuario logueado
                context.Result = new RedirectResult("/ControladorIniciarSesion/Login");
            }
        }


    }
}

