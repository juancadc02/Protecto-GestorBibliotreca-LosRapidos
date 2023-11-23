using DAL.Modelos;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios
{
    public interface ServicioConsultas
    {
        /// <summary>
        /// Interfaz del metodo encargador de registrar usuarios
        /// </summary>
        /// <param name="nuevoUsuario"></param>
        public void registrarUsuario(Usuarios nuevoUsuario);

        /// <summary>
        /// Interfaz del metodo que inicia sesion.
        /// </summary>
        /// <param name="nombre_usuario"></param>
        /// <param name="clave_usuario"></param>
        /// <returns></returns>
    }
}
