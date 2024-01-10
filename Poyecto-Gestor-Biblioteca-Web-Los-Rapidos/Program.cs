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

// A�adir la pol�tica de autorizaci�n
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequiereIdAcceso1", policy =>
        policy.Requirements.Add(new RequiereIdAcceso1Requirement()));
});

// Registrar el manejador de autorizaci�n
builder.Services.AddSingleton<IAuthorizationHandler, RequiereIdAcceso1Handler>();

// Agregar sesi�n
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Ejemplo de tiempo de expiraci�n
});

// ... (Resto del c�digo)

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Usar la autenticaci�n y autorizaci�n
app.UseAuthentication();
app.UseAuthorization();

// Usar la sesi�n
app.UseSession();

// Configurar rutas de controlador
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ControladorIniciarSesion}/{action=Login}/{id?}");

app.Run();
