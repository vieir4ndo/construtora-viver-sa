using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ConstrutoraViverSA.Controllers
{
    public class EstoqueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }        
        
        public IActionResult AdicionarMaterial()
        {
            return View();
        }            
        public IActionResult EditarMaterial()
        {
            return View();
        }    
                
        public IActionResult RelatorioMaterial()
        {
            var material = new List<Material>();

            var materialTeste = new Material();
            materialTeste.Nome = "Madeira";
            materialTeste.Descricao = "Teste";

            material.Add(materialTeste);
            material.Add(materialTeste);

            var relatorio = new RelatorioModel();
            relatorio.Materiais = material;

            return View(relatorio);
        }    
        
        public IActionResult CadastrarMaterial()
        {
            var model = new MaterialModel();
            model.Nome = "Teste";
            return View("AdicionarMaterial", model);
        }

        public IActionResult BuscarMaterial()
        {
            var model = new MaterialModel();
            model.Nome = "Teste";
            return View("EditarMaterial", model);
        }

    }
}
