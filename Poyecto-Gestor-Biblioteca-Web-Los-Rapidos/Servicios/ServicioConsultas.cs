using DAL.Modelos;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios
{
    public interface ServicioConsultas
    {
        public void registrarUsuario(Usuarios nuevoUsuario);
        public bool loginUsuario(string username, string contraseña, string urlApi);
    }
}
