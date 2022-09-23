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
    public class EstoqueController : ControllerBase
    {
        private readonly EstoqueService _estoqueService;

        public EstoqueController(EstoqueService estoqueService)
        {
            _estoqueService = estoqueService;
        }

        /*
        public void RelatorioMaterial()
        {
            var materiais = _estoqueService.BuscarMateriais();

            var relatorio = new RelatorioModel();
            relatorio.Materiais = materiais;

            //return View(relatorio);
            throw new Exception("NotImplemented");
        }

        public void CadastrarMaterial(string Nome, string Descricao, int Tipo, double Valor,
            DateTime DataValidade)
        {
            Material material = new Material(
                Nome,
                Descricao,
                (TipoMaterialEnum)Tipo,
                Valor,
                DataValidade
            );

            _estoqueService.AdicionarMaterial(material);

            //return View("SucessoView");
            throw new Exception("NotImplemented");
        }

        public void BuscarMaterial(long BuscaId)
        {
            var consulta = _estoqueService.BuscarMaterialPorId(BuscaId);

            if (consulta == null)
            {
                //return View("ErroView");
            }

            MaterialModel materialModel = new MaterialModel(
                consulta.Id,
                consulta.Nome,
                consulta.Descricao,
                (TipoMaterialEnum)consulta.Tipo,
                Convert.ToDouble(consulta.Valor),
                Convert.ToDateTime(consulta.DataValidade)
            );

            //return View("EditarMaterial", materialModel);
            throw new Exception("NotImplemented");
        }

        public void AlterarMaterial(long Id, string Nome, string Descricao, int Tipo, double Valor,
            DateTime DataValidade)
        {
            var consulta = _estoqueService.BuscarMaterialPorId(Id);

            if (consulta == null)
            {
                //return View("ErroView");
            }

            Material materialEditado = new Material(
                Nome,
                Descricao,
                (TipoMaterialEnum)Tipo,
                Valor,
                DataValidade
            );

            _estoqueService.AlterarMaterial(Id, materialEditado);

            //return View("SucessoView");
            throw new Exception("NotImplemented");
        }

        public void ExcluirMaterial(long IdExcluir)
        {
            var consulta = _estoqueService.BuscarMaterialPorId(IdExcluir);

            if (consulta == null)
            {
                //return View("ErroView");
            }

            _estoqueService.ExcluirMaterial(IdExcluir);

            //return View("SucessoView");
            throw new Exception("NotImplemented");
        }*/
    }
}