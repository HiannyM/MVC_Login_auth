//--Prueba de autorización en el controlador TestController--

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Login_auth.Controllers
{

    [Authorize] 
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return Content("Zona protegida");
        }
    }
}
