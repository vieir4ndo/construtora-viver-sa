using AutoMapper;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Infraestrutura;
using ConstrutoraViverSA.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ConstrutoraViverSA.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _database;

        public EstoqueController()
        {
            _database = new ApplicationContext();
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
            Material material = new Material(
                Request.Form["Nome"],
                Request.Form["Descricao"],
                (TipoMaterialEnum)Convert.ToInt16(Request.Form["Tipo"]),
                Convert.ToDouble(Request.Form["Valor"]),
                Convert.ToDateTime(Request.Form["DataValidade"])
                );

            _database.Materiais.Add(material);
            _database.SaveChanges(); 

            return View("AdicionarMaterial");
        }

        public IActionResult BuscarMaterial()
        {
            return View("EditarMaterial");
        }

    }
}
