using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MVC_Login_auth.Models
{
    public class ApplicationUser: IdentityUser
    {
        // Campos adicionales que se guardarán en AspNetUsers
        public string? NombreCompleto { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
    }
}

