using ConstrutoraViverSA.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConstrutoraViverSA.Controllers
{
    public class OrcamentosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdicionarOrcamento()
        {
            return View();
        }
        public IActionResult EditarOrcamento()
        {
            return View();
        }

        public IActionResult RelatorioOrcamento()
        {
            var relatorio = new RelatorioModel();

            return View(relatorio);
        }

        public IActionResult CadastrarOrcamento()
        {
            var model = new OrcamentoModel();
            model.Descricao = "Teste";
            return View("AdicionarOrcamento", model);
        }

        public IActionResult BuscarOrcamento()
        {
            var model = new OrcamentoModel();
            model.Descricao = "Teste";
            return View("EditarOrcamento", model);
        }

    }
}
