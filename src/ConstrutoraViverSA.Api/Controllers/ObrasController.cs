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
    public class ObrasController : ControllerBase
    {
        private readonly ObraService _obraService;
        
        public ObrasController(ObraService obraService)
        {
            _obraService = obraService;
        }
        public IActionResult Index()
        {
            //return View();
            throw new Exception("NotImplemented");
        }
        public IActionResult AdicionarObra()
        {
            //return View();
            throw new Exception("NotImplemented");
        }
        public IActionResult EditarObra()
        {
            //return View();
            throw new Exception("NotImplemented");
        }

        public IActionResult RelatorioObra()
        {
            var Obras = _obraService.BuscarObras();

            var relatorio = new RelatorioModel();
            relatorio.Obras = Obras;

            //return View(relatorio);
            throw new Exception("NotImplemented");
        }

        public IActionResult CadastrarObra(string nome, string endereco, TipoObraEnum tipoObra, string descricao, double valor, DateTime prazoConclusao)
        {
            Obra obra = new Obra(
                 nome,
                 endereco,
                 tipoObra,
                 descricao,
                 valor,
                 prazoConclusao);

            _obraService.AdicionarObra(obra);

            //return View("SucessoView");
            throw new Exception("NotImplemented");
        }
        public IActionResult BuscarObra(long BuscaId)
        {
            var consulta = _obraService.BuscarObraPorId(BuscaId);

            if (consulta == null)
            {
                //return View("ErroView");
            }

            ObraModel obraModel = new ObraModel(
                consulta.Id,
                consulta.Nome,
                consulta.Endereco,
                (TipoObraEnum)consulta.TipoObra,
                consulta.Descricao,
                Convert.ToDouble(consulta.Valor),
                Convert.ToDateTime(consulta.PrazoConclusao)
                );

            //return View("EditarObra", obraModel);
            throw new Exception("NotImplemented");
        }
        public IActionResult AlterarObra(long Id, string nome, string endereco, TipoObraEnum tipoObra, string descricao, double valor, DateTime prazoConclusao)
        {
            var consulta = _obraService.BuscarObraPorId(Id);

            if (consulta == null)
            {
                //return View("ErroView");
            }

            Obra ObraEditado = new Obra(
                 nome,
                 endereco,
                 tipoObra,
                 descricao,
                 valor,
                 prazoConclusao);

            _obraService.AlterarObra(Id, ObraEditado);

            //return View("SucessoView");
            throw new Exception("NotImplemented");
        }
        public IActionResult ExcluirObra(long IdExcluir)
        {
            var consulta = _obraService.BuscarObraPorId(IdExcluir);

            if (consulta == null)
            {
                //return View("ErroView");
            }

            _obraService.ExcluirObra(IdExcluir);

            //return View("SucessoView");
            throw new Exception("NotImplemented");
        }

    }
}
