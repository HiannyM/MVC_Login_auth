
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Login_auth.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> TestRoles()
        {
            var user = await _userManager.FindByEmailAsync("usuario@ejemplo.com");

            if (user == null)
                return Content("Usuario no encontrado");

            await _userManager.AddToRoleAsync(user, "Editor");

            bool esAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            await _userManager.RemoveFromRoleAsync(user, "Editor");

            var roles = await _userManager.GetRolesAsync(user);

            return Content("OK");
        }
    }
}
