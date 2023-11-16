using DAL;
using DAL.Modelos;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios
{
    public class ServicioConsultasImpl : ServicioConsultas
    {
        

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
                    clave_usuario = nuevoUsuario.clave_usuario,
                    estaBloqueado_usuario = nuevoUsuario.estaBloqueado_usuario,
                    fch_fin_bloqueo_usuario = nuevoUsuario.fch_fin_bloqueo_usuario,
                    fch_alta_usuario = nuevoUsuario.fch_alta_usuario,
                    fch_baja_usuario = nuevoUsuario.fch_baja_usuario
                };
                contexto.Add(nuevoUsuario);
                contexto.SaveChanges();
                Console.WriteLine("\n\n\t Usuario insertado correctamente");
            }
        }

        public bool loginUsuario(string username, string contraseña, string urlApi)
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = urlApi;
                // Construir el endpoint para la solicitud, incluyendo el nombre de usuario y la contraseña proporcionados
                string endpoint = $"/auth?username={username}&password={contraseña}";

                // Realizar una solicitud GET a la API concatenando la URL de la API y el endpoint
                HttpResponseMessage response = client.GetAsync(apiUrl + endpoint).Result;

                // Verificar si la solicitud fue exitosa (código de estado HTTP 200)
                if (response.IsSuccessStatusCode)
                {
                    // Leer el cuerpo de la respuesta como una cadena
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    // Interpretar la respuesta (en este caso, la API devuelve "true" si las credenciales son correctas)
                    return responseBody.ToLower() == "true"; // Ejemplo: la API devuelve "true" si las credenciales son correctas
                }
                else
                {
                    // Si la solicitud no fue exitosa, devolver false (autenticación fallida)
                    return false;
                }
            }
        }
        #endregion
    }
}
