
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MVC_Login_auth.ViewModels;

namespace MVC_Login_auth.Controllers
{
    namespace MVC_Login_auth.Controllers
    {
        [Authorize(Roles = "Admin")]
        public class AdminController : Controller
        {
            private readonly UserManager<IdentityUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager; // <-- 1. Declarar

            // 2. Pedir ambos en el constructor
            public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager; // <-- 3. Asignar
            }

            // Listar Usuarios
            public async Task<IActionResult> Index()
            {
                var usuarios = await _userManager.Users.ToListAsync();
                var modelo = new List<UsuarioRoleViewModel>();

                foreach (var user in usuarios)
                {
                    modelo.Add(new UsuarioRoleViewModel
                    {
                        UsuarioId = user.Id,
                        Email = user.Email!,
                        Roles = await _userManager.GetRolesAsync(user)
                    });
                }
                return View(modelo);
            }

            [HttpPost]
            [Route("Admin/AsignarEditor/{id}")]
            public async Task<IActionResult> AsignarEditor(string id)
            {
                if (string.IsNullOrEmpty(id)) return NotFound();

                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    // Ahora _roleManager ya funcionará porque está inyectado arriba
                    if (!await _roleManager.RoleExistsAsync("Editor"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Editor"));
                    }

                    await _userManager.AddToRoleAsync(user, "Editor");
                }
                return RedirectToAction("Index"); // Nombre de la acción como string es más seguro aquí
            }

            // Método para Quitar el rol de Editor
            [HttpPost]
            [Route("Admin/QuitarEditor/{id}")]
            public async Task<IActionResult> QuitarEditor(string id)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    await _userManager.RemoveFromRoleAsync(user, "Editor");
                }
                return RedirectToAction("Index");
            }

            // Método para Quitar el rol de Cliente
            // Nota: Úsalo con cuidado, si el mandato dice que todos son clientes por defecto
            [HttpPost]
            [Route("Admin/QuitarCliente/{id}")]
            public async Task<IActionResult> QuitarCliente(string id)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    await _userManager.RemoveFromRoleAsync(user, "Cliente");
                }
                return RedirectToAction("Index");
            }

            // Método para Asignar el rol de Cliente (por si alguien no lo tiene)
            [HttpPost]
            [Route("Admin/AsignarCliente/{id}")]
            public async Task<IActionResult> AsignarCliente(string id)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    if (!await _roleManager.RoleExistsAsync("Cliente"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Cliente"));
                    }
                    await _userManager.AddToRoleAsync(user, "Cliente");
                }
                return RedirectToAction("Index");
            }

            // --- Roles multiples ---

            [Authorize(Roles = "Admin,Editor")]
            public IActionResult EditarContenido() => View();

            public async Task<IActionResult> TestRoles()
            {
                var user = await _userManager.FindByEmailAsync("usuario@ejemplo.com");
                if (user == null) return Content("Usuario no encontrado");

                await _userManager.AddToRoleAsync(user, "Editor");
                bool esAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                await _userManager.RemoveFromRoleAsync(user, "Editor");

                return Content("OK");
            }
        }
    }
}