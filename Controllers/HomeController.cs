using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Login_auth.Models;
using MVC_Login_auth.ViewModels;
using System.Diagnostics;

namespace MVC_Login_auth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //-------------Accion de vista Edit-----------------
        // GET: /Home/Edit/5
        [HttpGet]
        [Authorize(Roles = "Editor,Admin")]
        public IActionResult Edit(int id)
        {
            // Objeto de prueba para que la vista no esté vacía
            var modeloPrueba = new ContenidoViewModel
            {
                Id = id,
                Titulo = "Aviso de la Tienda",
                Texto = "Contenido editable por el personal autorizado."
            };
            return View(modeloPrueba);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Editor,Admin")]
        public IActionResult Edit(ContenidoViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Para simular guardado
                TempData["Mensaje"] = "¡Contenido actualizado correctamente!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}
