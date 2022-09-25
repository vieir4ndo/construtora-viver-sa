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
            // TODO: Validar request
            // TODO: Usar Automapper

            _orcamentoService.AdicionarOrcamento(request.RequestParaDto());

            return Ok(new ApiResponse(true, null, null));
        }

        [HttpGet("{id}")]
        public IActionResult BuscarOrcamento(long id)
        {
            var consulta = _orcamentoService.BuscarOrcamentoPorId(id);

            return Ok(consulta);
        }

        [HttpPatch("{id}")]
        public IActionResult EditarOrcamento(OrcamentoRequest request, long id)
        {
            _orcamentoService.AlterarOrcamento(id, request.RequestParaDto());

            return Ok(new ApiResponse(true, null, null));
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirOrcamento(long id)
        {
            _orcamentoService.ExcluirOrcamento(id);

            return Ok(new ApiResponse(true, null, null));
        }
    }
}