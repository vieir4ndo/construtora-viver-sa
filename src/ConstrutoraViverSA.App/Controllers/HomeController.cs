using ConstrutoraViverSA.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ConstrutoraViverSA.App.Controllers
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

        public IActionResult Estoque()
        {
            return Redirect("Estoque");
        }
        public IActionResult Funcionarios()
        {
            return Redirect("Funcionarios");
        }
        public IActionResult Obras()
        {
            return Redirect("Obras");
        }
        public IActionResult Orcamentos()
        {
            return Redirect("Orcamentos");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
