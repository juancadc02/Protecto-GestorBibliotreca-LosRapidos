﻿namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios
{
    /// <summary>
    /// Interfaz que declara el metodo para encriptar cadenas de texto
    /// </summary>
    public interface servicioEncriptar
    {
        /// <summary>
        /// Interfaz del metodo encargado de encriptar 
        /// </summary>
        /// <param name="texto">String el cual se quiere encriptar</param>
        /// <returns>String encriptado</returns>
        public string Encriptar(string texto);
    }
}
