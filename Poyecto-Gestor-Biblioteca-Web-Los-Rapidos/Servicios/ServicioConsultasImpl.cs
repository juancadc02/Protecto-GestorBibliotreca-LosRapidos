using DAL;
using DAL.Modelos;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios
{
    public class ServicioConsultasImpl : ServicioConsultas
    {
        servicioEncriptarContraseña servicioEncriptar = new servicioEncriptarContraseñaImpl();
        

        #region Metodos Login
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
                    clave_usuario =servicioEncriptar.EncriptarContraseña( nuevoUsuario.clave_usuario),
                    fch_alta_usuario = nuevoUsuario.fch_alta_usuario,
                    
                };
                contexto.Add(nuevoUsuario);
                contexto.SaveChanges();
                Console.WriteLine("\n\n\t Usuario insertado correctamente");
            }
        }

        public bool loginUsuario(string nombre_usuario, string clave_usuario, string urlApi)
        {
             
            using (HttpClient client = new HttpClient())
            {
                
                string apiUrl = urlApi;
                
                string endpoint = $"/auth?username={nombre_usuario}&password={servicioEncriptar.EncriptarContraseña(clave_usuario)}";

                HttpResponseMessage response = client.GetAsync(apiUrl + endpoint).Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    return responseBody.ToLower() == "true";
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion
    }
}
