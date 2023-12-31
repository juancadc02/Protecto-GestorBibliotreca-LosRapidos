﻿using DAL.Modelos;

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
        /// Interfaz de metodo encargado de devolver un listado de los usuarios.
        /// </summary>
        /// <returns></returns>
        public List<Usuarios> listartUsuarios();
        public List<Prestamo> listarPrestamo();

        /// <summary>
        /// Interfaz del metodo que comprueba en la base de datos si el email introducido existe o no.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool existeCorreoElectronico(string email);
        /// <summary>
        /// Interfaz del metodo que comprueba en la base de datos si el dni introducido existe o no.
        /// </summary>
        /// <param name="dni"></param>
        /// <returns></returns>
        public bool existeDNI(string dni);


    }
}
