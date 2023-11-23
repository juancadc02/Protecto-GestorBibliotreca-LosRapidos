using DAL.Modelos;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios
{
    /// <summary>
    /// Interfaz que declara los métodos que se van a utilizar para realizar consultas
    /// </summary>
    public interface ServicioConsultas
    {
        /// <summary>
        /// Método encargado de registrar nuevos usuarios
        /// </summary>
        /// <param name="nuevoUsuario"> Objeto usuario que se va registrar en la bbdd</param>
        public void registrarUsuario(Usuarios nuevoUsuario);
        /// <summary>
        /// Interfaz del metodo que envia correo electronico
        /// </summary>
        /// <param name="gmailEnvio"></param>
        /// <param name="gmailRecibe"></param>
        /// <param name="subject"></param>
        /// <param name="htmlContent"></param>
        public void enviarCorreoElectronico(string gmailEnvio, string gmailRecibe, string subject, string htmlContent);
    }
}
