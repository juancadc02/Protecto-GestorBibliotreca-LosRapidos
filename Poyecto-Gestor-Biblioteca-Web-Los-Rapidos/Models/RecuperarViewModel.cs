using System.ComponentModel.DataAnnotations;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Models
{
    /// <summary>
    /// ViewModel para la vista de cambio de contraseña después de recuperación.
    /// </summary>
    public class RecuperarViewModel
    {
        /// <summary>
        /// Obtiene o establece la nueva contraseña.
        /// </summary>
        [Required(ErrorMessage = "El campo de la contraseña es obligatorio.")]
        public string Password { get; set; }

        /// <summary>
        /// Obtiene o establece la confirmación de la nueva contraseña.
        /// </summary>
        [Required(ErrorMessage = "La confirmación de la contraseña es obligatoria.")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string Password2{ get; set; }

        /// <summary>
        /// Obtiene o establece el token de recuperación asociado al usuario.
        /// </summary>
        public string Token { get; set; }
    }
}
