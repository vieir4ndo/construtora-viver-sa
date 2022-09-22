using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.App.Models;
using ConstrutoraViverSA.App.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ConstrutoraViverSA.App.Controllers
{
    public class ObrasController : Controller
    {
        private readonly ObraService _obraService;
        public ObrasController()
        {
            _obraService = new ObraService();
        }
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
            var Obras = _obraService.BuscarObras();

            var relatorio = new RelatorioModel();
            relatorio.Obras = Obras;

            return View(relatorio);
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

            return View("SucessoView");
        }
        public IActionResult BuscarObra(long BuscaId)
        {
            var consulta = _obraService.BuscarObraPorId(BuscaId);

            if (consulta == null)
            {
                return View("ErroView");
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

            return View("EditarObra", obraModel);
        }
        public IActionResult AlterarObra(long Id, string nome, string endereco, TipoObraEnum tipoObra, string descricao, double valor, DateTime prazoConclusao)
        {
            var consulta = _obraService.BuscarObraPorId(Id);

            if (consulta == null)
            {
                return View("ErroView");
            }

            Obra ObraEditado = new Obra(
                 nome,
                 endereco,
                 tipoObra,
                 descricao,
                 valor,
                 prazoConclusao);

            _obraService.AlterarObra(Id, ObraEditado);

            return View("SucessoView");
        }
        public IActionResult ExcluirObra(long IdExcluir)
        {
            var consulta = _obraService.BuscarObraPorId(IdExcluir);

            if (consulta == null)
            {
                return View("ErroView");
            }

            _obraService.ExcluirObra(IdExcluir);

            return View("SucessoView");
        }

    }
}
