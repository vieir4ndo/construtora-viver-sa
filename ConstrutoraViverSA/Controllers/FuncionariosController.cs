using ConstrutoraViverSA.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Controllers
{
    public class FuncionariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdicionarFuncionario()
        {
            return View();
        }
        public IActionResult EditarFuncionario()
        {
            return View();
        }

        public IActionResult RelatorioFuncionario()
        {
            var Funcionario = new List<Funcionario>();

            var FuncionarioTeste = new Funcionario();
            FuncionarioTeste.Nome = "Madeira";
            FuncionarioTeste.Genero = GeneroEnum.Feminino;
            FuncionarioTeste.NumCtps = "Teste";

            Funcionario.Add(FuncionarioTeste);
            Funcionario.Add(FuncionarioTeste);

            var relatorio = new RelatorioModel();
            relatorio.Funcionarios = Funcionario;

            return View(relatorio);
        }

        public IActionResult CadastrarFuncionario()
        {
            var model = new FuncionarioModel();
            model.Nome = "Teste";
            return View("AdicionarFuncionario", model);
        }

        public IActionResult BuscarFuncionario()
        {
            var model = new FuncionarioModel();
            model.Nome = "Teste";
            return View("EditarFuncionario", model);
        }

    }
}
