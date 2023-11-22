using DAL;
using DAL.Modelos;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios
{
    public class ServicioConsultasImpl : ServicioConsultas
    {
        servicioEncriptarContraseña servicioEncriptar = new servicioEncriptarContraseñaImpl();



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
                    clave_usuario = servicioEncriptar.EncriptarContraseña(nuevoUsuario.clave_usuario),
                    fch_alta_usuario = nuevoUsuario.fch_alta_usuario,

                };
                contexto.Add(nuevoUsuario);
                contexto.SaveChanges();
                Console.WriteLine("\n\n\t Usuario insertado correctamente");
            }
        }
 
      
    }


    }


