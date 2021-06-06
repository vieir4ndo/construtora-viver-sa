using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Models;
using ConstrutoraViverSA.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ConstrutoraViverSA.Controllers
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

            return View("AdicionarObra");
        }
        public IActionResult BuscarObra(long BuscaId)
        {
            var consulta = _obraService.BuscarObraPorId(BuscaId);

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
            Obra ObraEditado = new Obra(
                 nome,
                 endereco,
                 tipoObra,
                 descricao,
                 valor,
                 prazoConclusao);

            _obraService.AlterarObra(Id, ObraEditado);

            return View("EditarObra");
        }
        public IActionResult ExcluirObra(long IdExcluir)
        {
            _obraService.ExcluirObra(IdExcluir);

            return View("EditarObra");
        }

    }
}
