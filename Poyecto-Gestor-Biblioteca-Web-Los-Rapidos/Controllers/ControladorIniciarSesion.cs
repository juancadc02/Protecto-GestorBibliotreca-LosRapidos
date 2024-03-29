﻿using DAL;
using DAL.Modelos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Controllers
{
    public class ControladorIniciarSesion : Controller
    {
        private readonly GestorBibliotecaDbContext dbContext;

        const string urlApi = "https://localhost:7268/api/ControladorUsuarios";
        /// <summary>
        /// Metodo encargado de abrir la vista del login.
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            ViewBag.MensajeRegistroExitoso = TempData["MensajeRegistroExitoso"] as string;
            // Lógica de la acción (si es necesario)
            return View("~/Views/Home/Login.cshtml");// Devuelve la vista asociada
        }
        /// <summary>
        /// Metodo encargado de hacer el inicio de sesion con el email y contraseña recogidos del formulario
        /// </summary>
        /// <param name="email_usuario"></param>
        /// <param name="clave_usuario"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InicioDeSesion(string email_usuario, string clave_usuario)
        {
            ServicioConsultas consultas = new ServicioConsultasImpl();

            if (string.IsNullOrEmpty(email_usuario) || string.IsNullOrEmpty(clave_usuario))
            {
                return RedirectToAction("Error", "Home");
            }

            if (IniciarSesion(email_usuario, clave_usuario))
            {
                // Autenticar al usuario
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, email_usuario),
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true

                };

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.MensajeLoginError = "Usuario o Contraseña incorrecto !!";
                return View("~/Views/Home/Login.cshtml");
            }
        }


        /// <summary>
        /// Metodo que comprueba si el email y la clave del usuario estan registrados en la base de datos
        /// </summary>
        /// <param name="email_usuario"></param>
        /// <param name="clave_usuario"></param>
        /// <returns></returns>
        private bool IniciarSesion(string email_usuario, string clave_usuario)
        {
            servicioEncriptar encriptarContraseña = new servicioEncriptarImpl();
            var usuario = dbContext.Usuarios
                .FirstOrDefault(u => u.email_usuario == email_usuario && u.clave_usuario == encriptarContraseña.Encriptar(clave_usuario));
            return usuario != null;
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        public ControladorIniciarSesion(GestorBibliotecaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        private string GenerateJwtToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("tu_clave_secreta_aqui"); // Asegúrate de mantener esto seguro y no exponerlo en el código

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, email) // Aquí puedes agregar más claims según lo necesites
        }),
                Expires = DateTime.UtcNow.AddHours(1), // Tiempo de expiración del token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}