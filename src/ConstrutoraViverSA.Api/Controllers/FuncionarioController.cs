using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Responses;
using ConstrutoraViverSA.Application.Interfaces;

namespace ConstrutoraViverSA.Api.Controllers
{
    
    [ApiController]
    [Route("funcionario")]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionarioController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [HttpPost]
        public IActionResult CadastrarFuncionario(FuncionarioRequest request)
        {
            try
            {
                request.ValidarCriacao();
                
                _funcionarioService.AdicionarFuncionario(request.RequestParaDto());
                
                return Ok(new ApiResponse(true, null, null));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(false, null, new List<string>(){ e.Message}));
            }
        }
        
        [HttpGet("{id}")]
        public IActionResult BuscarFuncionario(long id)
        {
            try
            {
                // TODO: Levar lógica para service
                var consulta = _funcionarioService.BuscarFuncionarioPorId(id);
                
                // TODO: Mapear para objeto de response

                var result = new ApiResponse(true, new List<object>() { consulta }, null);
                
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(false, null, new List<string>(){ e.Message}));
            }
        }

        [HttpPatch("{id}")]
        public IActionResult EditarFuncionario(FuncionarioRequest request, long id)
        {
            try
            {
                request.ValidarEdicao();
                
                _funcionarioService.AlterarFuncionario(id, request.RequestParaDto());

                return Ok( new ApiResponse(true, null, null));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(false, null, new List<string>(){ e.Message}));
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult ExcluirFuncionario(long id)
        {
            try
            {
                _funcionarioService.ExcluirFuncionario(id);

                return Ok(new ApiResponse(true, null, null));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(false, null, new List<string>(){ e.Message}));
            }
        }
    }
}
