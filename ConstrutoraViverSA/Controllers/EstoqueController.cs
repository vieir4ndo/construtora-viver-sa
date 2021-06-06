using AutoMapper;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Models;
using ConstrutoraViverSA.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ConstrutoraViverSA.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly IMapper _mapper;
        private readonly EstoqueService _estoqueService;

        public EstoqueController()
        {
            _estoqueService = new EstoqueService();
        }
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
            var materiais = _estoqueService.BuscarMateriais();

            var relatorio = new RelatorioModel();
            relatorio.Materiais = materiais;

            return View(relatorio);
        }

        public IActionResult CadastrarMaterial(string Nome, string Descricao, int Tipo, double Valor, DateTime DataValidade)
        {
            Material material = new Material(
                Nome,
                Descricao,
                (TipoMaterialEnum)Tipo,
                Valor,
                DataValidade
                );

            _estoqueService.AdicionarMaterial(material);

            return View("AdicionarMaterial");
        }

        public IActionResult BuscarMaterial(long BuscaId)
        {
            var consulta = _estoqueService.BuscarMaterialPorId(BuscaId);

            MaterialModel materialModel = new MaterialModel(
                consulta.Id,
                consulta.Nome,
                consulta.Descricao,
                (TipoMaterialEnum)consulta.Tipo,
                Convert.ToDouble(consulta.Valor),
                Convert.ToDateTime(consulta.DataValidade)
                );

            return View("EditarMaterial", materialModel);
        }

        public IActionResult AlterarMaterial(long Id, string Nome, string Descricao, int Tipo, double Valor, DateTime DataValidade)
        {
            Material materialEditado = new Material(
             Nome,
             Descricao,
             (TipoMaterialEnum)Tipo,
             Valor,
             DataValidade
             );

            _estoqueService.AlterarMaterial(Id, materialEditado);

            return View("EditarMaterial");
        }
        public IActionResult ExcluirMaterial(long IdExcluir)
        {
            _estoqueService.ExcluirMaterial(IdExcluir);

            return View("EditarMaterial");
        }
    }
}
