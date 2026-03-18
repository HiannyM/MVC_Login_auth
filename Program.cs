using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_Login_auth.Data;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// --- BLOQUE 1: CONFIGURACIÓN DE SERVICIOS (Antes del Build) ---

// 1. Conexión a la base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Configuración de Identity 
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddRoles<IdentityRole>() 
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 3. Configuración de Autorización y Políticas
builder.Services.AddAuthorization(options =>
{
    // Política personalizada
    options.AddPolicy("SoloTI", policy => policy.RequireClaim("Departamento", "TI"));


});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // Necesario para Identity

// --- CONSTRUCCIÓN DE LA APP ---
var app = builder.Build();

// ---  CONFIGURACIÓN DEL MIDDLEWARE  ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 4. Autenticación y Autorización 
app.UseAuthentication();
app.UseAuthorization();

// 5. Mapeo de rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// 6. Seeder (Se ejecutara justo antes del Run)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await DbInitializer.SeedRolesAndAdminAsync(services);
    }
    catch (Exception ex)
    {
        // Esto es para ver si el Seeder falla por otra razón
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al sembrar la base de datos.");
    }
}

app.Run();