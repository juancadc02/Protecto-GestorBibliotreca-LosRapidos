namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios
{
    /// <summary>
    /// Interfaz que declara el metodo para encriptar contraseña
    /// </summary>
    public interface servicioEncriptarContraseña
    {
        /// <summary>
        /// Interfaz del metodo encargado de encriptar la contraseña
        /// </summary>
        /// <param name="contraseña">Contraseña del usuario sin encriptar</param>
        /// <returns>Contraseña del usuario encriptada</returns>
        public string EncriptarContraseña(string contraseña);
    }
}
