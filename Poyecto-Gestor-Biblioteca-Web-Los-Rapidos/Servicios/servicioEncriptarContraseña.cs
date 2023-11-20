namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios
{
    /// <summary>
    /// Interfaz que define el metodo de encriptar contraseña
    /// </summary>
    public interface servicioEncriptarContraseña
    {
        /// <summary>
        /// Interfaz del metodo encargado de encriptar la contraseña
        /// </summary>
        /// <param name="contraseña"></param>
        /// <returns></returns>
        public string EncriptarContraseña(string contraseña);
    }
}
