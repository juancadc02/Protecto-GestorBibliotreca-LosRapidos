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
        options.LoginPath = "/ControladorIniciarSesion/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



// Configurar rutas de controlador
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ControladorIniciarSesion}/{action=Login}/{id?}");

app.Run();
