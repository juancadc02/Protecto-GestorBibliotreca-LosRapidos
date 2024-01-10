// ... Otros using statements
using DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios;
using Microsoft.AspNetCore.Authorization;
using Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.NewFolder1;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<GestorBibliotecaDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("CadenaConexionPostgreSQL"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// Añadir la política de autorización
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequiereIdAcceso1", policy =>
        policy.Requirements.Add(new RequiereIdAcceso1Requirement()));
});

// Registrar el manejador de autorización
builder.Services.AddSingleton<IAuthorizationHandler, RequiereIdAcceso1Handler>();

// Agregar sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Ejemplo de tiempo de expiración
});

// ... (Resto del código)

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Usar la autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Usar la sesión
app.UseSession();

// Configurar rutas de controlador
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ControladorIniciarSesion}/{action=Login}/{id?}");

app.Run();
