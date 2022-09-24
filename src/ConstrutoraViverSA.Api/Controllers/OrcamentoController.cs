using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Responses;
using ConstrutoraViverSA.Application.Interfaces;

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
            try
            {
                // TODO: Validar request
                // TODO: Usar Automapper
                
                _orcamentoService.AdicionarOrcamento(request.RequestParaDto());
                
                return Ok(new ApiResponse(true, null, null));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(false, null, new List<string>(){ e.Message}));
            }
        }
        
        [HttpGet("{id}")]
        public IActionResult BuscarOrcamento(long id)
        {
            try
            {
                var consulta = _orcamentoService.BuscarOrcamentoPorId(id);

                return Ok(consulta);
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(false, null, new List<string>(){ e.Message}));
            }
        }
        
        [HttpPatch("{id}")]
        public IActionResult EditarOrcamento(OrcamentoRequest request, long id)
        {
            try
            {
                _orcamentoService.AlterarOrcamento(id, request.RequestParaDto());

                return Ok(new ApiResponse(true, null, null));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(false, null, new List<string>(){ e.Message}));
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult ExcluirOrcamento(long id)
        {
            try
            {
                _orcamentoService.ExcluirOrcamento(id);

                return Ok(new ApiResponse(true, null, null));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(false, null, new List<string>(){ e.Message}));
            }
        }
    }
}