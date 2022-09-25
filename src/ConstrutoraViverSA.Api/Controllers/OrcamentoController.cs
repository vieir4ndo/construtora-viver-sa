using System.Collections.Generic;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Responses;
using ConstrutoraViverSA.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConstrutoraViverSA.Api.Controllers
{
    [ApiController]
    [Route("orcamento")]
    public class OrcamentoController : ControllerBase
    {
        private readonly IOrcamentoService _orcamentoService;

        public OrcamentoController(IOrcamentoService orcamentoService)
        {
            _orcamentoService = orcamentoService;
        }

        [HttpPost]
        public IActionResult CadastrarOrcamento(OrcamentoRequest request)
        {
            request.ValidarCriacao();

            _orcamentoService.Adicionar(request.RequestParaDto());

            return Ok(new ApiResponse(true, null, null));
        }

        [HttpGet("{id}")]
        public IActionResult BuscarOrcamento(long id)
        {
            var consulta = _orcamentoService.BuscarPorId(id);

            return Ok(new ApiResponse(true, new List<object>() { consulta }, null));
        }

        [HttpPatch("{id}")]
        public IActionResult EditarOrcamento(OrcamentoRequest request, long id)
        {
            request.ValidarEdicao();
            
            _orcamentoService.Editar(id, request.RequestParaDto());

            return Ok(new ApiResponse(true, null, null));
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirOrcamento(long id)
        {
            _orcamentoService.Excluir(id);

            return Ok(new ApiResponse(true, null, null));
        }
    }
}