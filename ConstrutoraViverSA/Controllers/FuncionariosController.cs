using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Models;
using ConstrutoraViverSA.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ConstrutoraViverSA.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly FuncionarioService _funcionarioService;

        public FuncionariosController()
        {
            _funcionarioService = new FuncionarioService();
        }
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
            var Funcionario = _funcionarioService.BuscarFuncionarios();

            var relatorio = new RelatorioModel();
            relatorio.Funcionarios = Funcionario;

            return View(relatorio);
        }

        public IActionResult CadastrarFuncionario(string nome, DateTime dataNascimento, int genero, string cpf, string numCtps, string endereco, string email, string telefone, int cargo)
        {
            Funcionario funcionario = new Funcionario(
                nome,
                dataNascimento,
                (GeneroEnum)genero,
                cpf,
                numCtps,
                endereco,
                email,
                telefone,
                (CargoEnum)cargo);

            _funcionarioService.AdicionarFuncionario(funcionario);

            return View("AdicionarFuncionario");
        }

        public IActionResult BuscarFuncionario(long BuscaId)
        {
            var consulta = _funcionarioService.BuscarFuncionarioPorId(BuscaId);

            FuncionarioModel funcionarioModel = new FuncionarioModel(
                consulta.Id,
                consulta.Nome,
                Convert.ToDateTime(consulta.DataNascimento),
                (GeneroEnum)consulta.Genero,
                consulta.Cpf,
                consulta.NumCtps,
                consulta.Endereco,
                consulta.Email,
                consulta.Telefone,
                (CargoEnum)consulta.Cargo
                );

            return View("EditarFuncionario", funcionarioModel);
        }

        public IActionResult AlterarFuncionario(long Id, string nome, DateTime dataNascimento, GeneroEnum genero, string cpf, string numCtps, string endereco, string email, string telefone, CargoEnum cargo)
        {
            Funcionario funcionarioEditado = new Funcionario(
                 nome,
                 dataNascimento,
                 genero,
                 cpf,
                 numCtps,
                 endereco,
                 email,
                 telefone,
                 cargo);

            _funcionarioService.AlterarFuncionario(Id, funcionarioEditado);

            return View("EditarFuncionario");
        }
        public IActionResult ExcluirFuncionario(long IdExcluir)
        {
            _funcionarioService.ExcluirFuncionario(IdExcluir);

            return View("EditarFuncionario");
        }
    }
}
