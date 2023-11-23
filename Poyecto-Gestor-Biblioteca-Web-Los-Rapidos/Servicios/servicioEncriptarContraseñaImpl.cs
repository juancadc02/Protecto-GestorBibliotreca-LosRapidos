using System.Security.Cryptography;
using System.Text;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios
{
    /// <summary>
    /// Clase que implementa y detalla la lógica para encriptar contraseña
    /// </summary>
    public class servicioEncriptarContraseñaImpl : servicioEncriptarContraseña
    {
        public string EncriptarContraseña(string contraseña)
        {
            //Creamos una instancia de la clase SHA256.
            //SHA256 es un algoritmo de hash criptográfico ampliamente utilizado que toma una entrada
            //y produce una cadena hexadecimal de 64 caracteres como resultado.
            using (SHA256 sha256 = SHA256.Create())
            {
                //La contraseña proporcionada se convierte en un arreglo de bytes
                //utilizando la codificación UTF-8, esta codificacion asegura que los caracteres especiales
                //y no ASCII se manejen correctamente.
                byte[] bytes = Encoding.UTF8.GetBytes(contraseña);

                //Toma los bytes de entrada y aplica el algoritmo SHA-256 para producir
                //un conjunto de bytes que representa el hash de la contraseña.
                byte[] hashBytes = sha256.ComputeHash(bytes);

                //Paso 1: BitConverter.ToString(hashBytes) convierte los bytes del hash en una cadena que contiene
                //los caracteres hexadecimales separados por guiones.
                //Paso 2: Replace("-", "") elimina los guiones de la cadena generada, ya que a menudo
                //se prefiere una cadena continua
                //Paso 3: ToLower() convierte la cadena resultante a minúsculas,
                //lo que es común en la representación de hashes.
                string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                //Finalmente, el método devuelve la cadena hash que representa la contraseña encriptada.
                return hash;
            }
        }
    }
}
