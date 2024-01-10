using DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.Servicios;

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

builder.Services.AddAuthorization(options =>
{
    // Puedes agregar políticas de autorización si es necesario
});

// Agregar sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Ejemplo de tiempo de expiración
});

// Realizar migraciones
/*using (var scope = builder.Services.CreateScope())
{
    var appDBContext = scope.ServiceProvider.GetRequiredService<GestorBibliotecaDbContext>();
    appDBContext.Database.Migrate();
}*/

builder.Services.AddScoped<servicioEncriptar, servicioEncriptarImpl>();

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
