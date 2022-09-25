using System.Collections.Generic;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Responses;
using ConstrutoraViverSA.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConstrutoraViverSA.Api.Controllers
{
    [ApiController]
    [Route("material")]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpPost]
        public IActionResult CadastrarMaterial(MaterialRequest request)
        {
            request.ValidarCriacao();
            
            _materialService.AdicionarMaterial(request.RequestParaDto());

            return Ok(new ApiResponse(true, null, null));
        }

        [HttpGet("{id}")]
        public IActionResult BuscarMaterial(long id)
        {
            var consulta = _materialService.BuscarMaterialPorId(id);

            return Ok(new ApiResponse(true, new List<object> { consulta }, null));
        }

        [HttpPatch("{id}")]
        public IActionResult EditarMaterial(MaterialRequest request, long id)
        {
            request.ValidarEdicao();
            
            _materialService.AlterarMaterial(id, request.RequestParaDto());

            return Ok(new ApiResponse(true, null, null));
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirMaterial(long id)
        {
            _materialService.ExcluirMaterial(id);

            return Ok(new ApiResponse(true, null, null));
        }

        [HttpPut("{id}/estoque")]
        public IActionResult GerenciarEstoque(EstoqueRequest request, long id)
        {
            request.Validar();

            _materialService.MovimentarEstoque(id, request.RequestParaDto());
            
            return Ok(new ApiResponse(true, null, null));
        }
    }
}