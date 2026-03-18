using Microsoft.AspNetCore.Identity;

namespace MVC_Login_auth.Data
{
    public class DbInitializer
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

            // Crear roles si no existen
            string[] roles = { "Admin", "Editor", "Cliente" };
            foreach (var role in roles)
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));

            // Crear usuario admin por defecto
            const string adminEmail = "admin@miapp.com";
            const string adminPass = "Admin123!";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(admin, adminPass);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }

            //Crear usuario editor
            if (!await roleManager.RoleExistsAsync("Editor"))
            {
                await roleManager.CreateAsync(new IdentityRole("Editor"));
            }
        }
    }
}
