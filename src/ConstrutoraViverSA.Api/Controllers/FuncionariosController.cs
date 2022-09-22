using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using ConstrutoraViverSA.Service;

namespace ConstrutoraViverSA.Api.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class FuncionariosController : ControllerBase
    {
        private readonly FuncionarioService _funcionarioService;

        public FuncionariosController(FuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }
        public IActionResult Index()
        {
            //return View();
            throw new Exception("NotImplemented");
        }
        public IActionResult AdicionarFuncionario()
        {
            //return View();
            throw new Exception("NotImplemented");
        }
        public IActionResult EditarFuncionario()
        {
            //return View();
            throw new Exception("NotImplemented");
        }

        public IActionResult RelatorioFuncionario()
        {
            var Funcionario = _funcionarioService.BuscarFuncionarios();

            var relatorio = new RelatorioModel();
            relatorio.Funcionarios = Funcionario;

            //return View(relatorio);
            throw new Exception("NotImplemented");
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

            //return View("SucessoView");
            throw new Exception("NotImplemented");
        }

        public IActionResult BuscarFuncionario(long BuscaId)
        {
            var consulta = _funcionarioService.BuscarFuncionarioPorId(BuscaId);

            if (consulta == null)
            {
                //return View("ErroView");
            }

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

            //return View("EditarFuncionario", funcionarioModel);
            throw new Exception("NotImplemented");
        }

        public IActionResult AlterarFuncionario(long Id, string nome, DateTime dataNascimento, GeneroEnum genero, string cpf, string numCtps, string endereco, string email, string telefone, CargoEnum cargo)
        {
            var consulta = _funcionarioService.BuscarFuncionarioPorId(Id);

            if (consulta == null)
            {
                //return View("ErroView");
            }

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

            //return View("SucessoView");
            throw new Exception("NotImplemented");
        }
        public IActionResult ExcluirFuncionario(long IdExcluir)
        {
            var consulta = _funcionarioService.BuscarFuncionarioPorId(IdExcluir);

            if (consulta == null)
            {
                //return View("ErroView");
            }
            _funcionarioService.ExcluirFuncionario(IdExcluir);

            //return View("SucessoView");
            throw new Exception("NotImplemented");
        }
    }
}
