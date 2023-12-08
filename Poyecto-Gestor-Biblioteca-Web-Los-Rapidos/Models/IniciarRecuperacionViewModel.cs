using System.ComponentModel.DataAnnotations;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Models
{
    /// <summary>
    /// ViewModel para la vista de recuperación de contraseña.
    /// </summary>
    public class IniciarRecuperacionViewModel
    {
        /// <summary>
        /// Obtiene o establece la dirección de correo electrónico para recuperar la contraseña.
        /// </summary>
        [Required(ErrorMessage = "El campo correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Por favor, ingresa una dirección de correo electrónico válida.")]
        public string Email { get; set; }
    }
}
