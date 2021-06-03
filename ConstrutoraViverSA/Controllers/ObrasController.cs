using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ConstrutoraViverSA.Controllers
{
    public class ObrasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdicionarObra()
        {
            return View();
        }
        public IActionResult EditarObra()
        {
            return View();
        }

        public IActionResult RelatorioObra()
        {
            var relatorio = new RelatorioModel();

            return View(relatorio);
        }

        public IActionResult CadastrarObra()
        {
            var model = new ObraModel();
            model.Nome = "Teste";
            return View("AdicionarObra", model);
        }

        public IActionResult BuscarObra()
        {
            var model = new ObraModel();
            model.Nome = "Teste";
            return View("EditarObra", model);
        }

    }
}
