using Microsoft.AspNetCore.Mvc;
using System;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Responses;
using ConstrutoraViverSA.Service;

namespace ConstrutoraViverSA.Api.Controllers
{
    [ApiController]
    [Route("orcamento")]
    public class OrcamentoController : ControllerBase
    {
        private readonly OrcamentoService _orcamentoService;

        public OrcamentoController(OrcamentoService orcamentoService)
        {
            _orcamentoService = orcamentoService;
        }

        [HttpPost]
        public IActionResult CadastrarOrcamento(CriarOrcamentoRequest request)
        {
            try
            {
                // TODO: Validar request
                // TODO: Usar Automapper
                
                _orcamentoService.AdicionarOrcamento(request.RequestParaDto());
                
                return Ok(ApiResponseFactory.Success());
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponseFactory.Error(e.Message));
            }
        }
        
        [HttpGet("{id}")]
        public IActionResult BuscarOrcamento(long id)
        {
            try
            {
                // TODO: Levar lógica para service
                var consulta = _orcamentoService.BuscarOrcamentoPorId(id);

                if (consulta == null)
                {
                    return NotFound(ApiResponseFactory.Error("Orcamento não encontrado"));
                }
                
                // TODO: Mapear para objeto de response

                return Ok(consulta);
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponseFactory.Error(e.Message));
            }
        }
        
        [HttpPatch("{id}")]
        public IActionResult EditarOrcamento(EditarOrcamentoRequest request, long id)
        {
            try
            {
                // TODO: Levar lógica para service
                var consulta = _orcamentoService.BuscarOrcamentoPorId(id);

                if (consulta == null)
                {
                    return NotFound(ApiResponseFactory.Error("Orcamento não encontrado"));
                }

                _orcamentoService.AlterarOrcamento(id, request.RequestParaDto());

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponseFactory.Error(e.Message));
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult ExcluirOrcamento(long id)
        {
            try
            {
                // TODO: Levar lógica para service
                var consulta = _orcamentoService.BuscarOrcamentoPorId(id);

                if (consulta == null)
                {
                    return NotFound(ApiResponseFactory.Error("Orcamento não encontrado"));
                }

                _orcamentoService.ExcluirOrcamento(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponseFactory.Error(e.Message));
            }
        }
    }
}