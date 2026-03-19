namespace MVC_Login_auth.ViewModels
{
    public class UsuarioRoleViewModel
    {
        public string UsuarioId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
