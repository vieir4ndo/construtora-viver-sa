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
    public class OrcamentosController : ControllerBase
    {
        private readonly OrcamentoService _orcamentoService;
        
        public OrcamentosController(OrcamentoService orcamentoService)
        {
            _orcamentoService = orcamentoService;
        }
        public IActionResult Index()
        {
            //return View();
            throw new Exception("NotImplemented");
        }
        public IActionResult AdicionarOrcamento()
        {
            //return View();
            throw new Exception("NotImplemented");
        }
        public IActionResult EditarOrcamento()
        {
            //return View();
            throw new Exception("NotImplemented");
        }

        public IActionResult RelatorioOrcamento()
        {
            var orcamentos = _orcamentoService.BuscarOrcamentos();

            var relatorio = new RelatorioModel();
            relatorio.Orcamentos = orcamentos;

            //return View(relatorio);
            throw new Exception("NotImplemented");
        }

        public IActionResult CadastrarOrcamento(string Descricao, string Endereco, int TipoObra, DateTime DataEmissao, DateTime DataValidade, double Valor)
        {
            Orcamento orcamento = new Orcamento(
                Descricao,
                Endereco,
                (TipoObraEnum)TipoObra,
                Convert.ToDateTime(DataEmissao),
                Convert.ToDateTime(DataValidade),
                Convert.ToDouble(Valor)
             );

            _orcamentoService.AdicionarOrcamento(orcamento);

            //return View("SucessoView");
            throw new Exception("NotImplemented");
        }

        public IActionResult BuscarOrcamento(long BuscaId)
        {
            var consulta = _orcamentoService.BuscarOrcamentoPorId(BuscaId);

            if (consulta == null)
            {
                //return View("ErroView");
            }

            OrcamentoModel OrcamentoModel = new OrcamentoModel(
                consulta.Id,
                consulta.Descricao,
                consulta.Endereco,
                (TipoObraEnum)consulta.TipoObra,
                Convert.ToDateTime(consulta.DataEmissao),
                Convert.ToDateTime(consulta.DataValidade),
                Convert.ToDouble(consulta.Valor)
                );

            //return View("EditarOrcamento", OrcamentoModel);
            throw new Exception("NotImplemented");
        }

        public IActionResult AlterarOrcamento(long Id, string Descricao, string Endereco, int TipoObra, DateTime DataEmissao, DateTime DataValidade, double Valor)
        {
            var consulta = _orcamentoService.BuscarOrcamentoPorId(Id);

            if (consulta == null)
            {
                //return View("ErroView");
            }

            Orcamento OrcamentoEditado = new Orcamento(
                Descricao,
                Endereco,
                (TipoObraEnum)TipoObra,
                Convert.ToDateTime(DataEmissao),
                Convert.ToDateTime(DataValidade),
                Convert.ToDouble(Valor)
             );

            _orcamentoService.AlterarOrcamento(Id, OrcamentoEditado);

            //return View("SucessoView");
            throw new Exception("NotImplemented");
        }
        public IActionResult ExcluirOrcamento(long IdExcluir)
        {
            var consulta = _orcamentoService.BuscarOrcamentoPorId(IdExcluir);

            if (consulta == null)
            {
                //return View("ErroView");
            }

            _orcamentoService.ExcluirOrcamento(IdExcluir);

            //return View("SucessoView");
            throw new Exception("NotImplemented");
        }
    }
}
