using DAL;
using DAL.Modelos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios
{
    /// <summary>
    /// Clase que implementa la interfaz ServicioConsultas y detalla la lógica de los métodos.
    /// </summary>
    public class ServicioConsultasImpl : ServicioConsultas
    {
        private readonly servicioEncriptar servicioEncriptar = new servicioEncriptarImpl();

        /// <summary>
        /// Registra un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="nuevoUsuario">Objeto Usuarios que representa al nuevo usuario a registrar.</param>
        public void registrarUsuario(Usuarios nuevoUsuario)
        {
            using (var contexto = new GestorBibliotecaDbContext())
            {
                nuevoUsuario = new Usuarios
                {
                    dni_usuario = nuevoUsuario.dni_usuario,
                    nombre_usuario = nuevoUsuario.nombre_usuario,
                    apellidos_usuario = nuevoUsuario.apellidos_usuario,
                    tlf_usuario = nuevoUsuario.tlf_usuario,
                    email_usuario = nuevoUsuario.email_usuario,
                    clave_usuario = servicioEncriptar.Encriptar(nuevoUsuario.clave_usuario),
                    fch_alta_usuario = nuevoUsuario.fch_alta_usuario,
                };

                contexto.Add(nuevoUsuario);
                contexto.SaveChanges();
                Console.WriteLine("\n\n\t Usuario insertado correctamente");
            }
        }
        public List<Usuarios> listartUsuarios()
        {
            using (var contexto = new GestorBibliotecaDbContext())
            {
                List<Usuarios> listaUsuarios = contexto.Usuarios.ToList();

                if (listaUsuarios.Count == 0)
                {
                    Console.WriteLine("\n\n\tNo se ha encontrado ningun usuario.");
                }
                else
                {
                    foreach (var usu in listaUsuarios)
                    {
                        Console.WriteLine("\n\n\t{0}  {1}  {2}  {3}  {4}  {5}", usu.id_usuario, usu.nombre_usuario, usu.apellidos_usuario, usu.tlf_usuario, usu.email_usuario, usu.fch_alta_usuario);
                    }
                }
                return listaUsuarios;
            }
        }
        public List<Prestamo> listarPrestamo()
        {
            using (var contexto = new GestorBibliotecaDbContext())
            {
                List<Prestamo> listaPrestamo = contexto.Prestamos.ToList();

                if (listaPrestamo.Count == 0)
                {
                    Console.WriteLine("\n\n\tNo se ha encontrado ningun prestamo.");
                }
                else
                {
                    foreach (var prestamo in listaPrestamo)
                    {
                        Console.WriteLine("\n\n\t{0}  {1}  {2}  {3}  {4}  {5}", prestamo.id_libro,prestamo.usuario,prestamo.fch_inicio_prestamo,prestamo.fch_fin_prestamo,prestamo.fch_entrega_prestamo,prestamo.estado);
                    }
                }
                return listaPrestamo;
            }
        }


    }

}





